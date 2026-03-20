using Auri.Audio;
using Auri.Audio.Encoder;
using Auri.Config;
using Auri.Services;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Auri.Managers
{
    public class AudioManager
    {
        public event Action OnAbort;
        public event Action<int> OnFileExists;
        public event Action<int, float> OnProgress;
        public event Action<float> OnOverallProgress;
        public event Action<int, bool> OnComplete;
        public event Action<bool> OnAllComplete;

        private readonly ConfigManager _config;
        private readonly AudioEngineService _bass;
        private readonly AudioFile[] _audioFiles;
        private readonly string _outputPath;
        private readonly string _pattern;
        private readonly string _format;
        private readonly EncoderPreset _preset;

        private float[] _fileProgress;
        private int _totalFiles;
        private int _completedFilesCount;
        private int _allCompleted; // Используем int для Interlocked
        private volatile int _aborted; // Используем int для атомарных операций

        private ConcurrentBag<IEncoder> _encoders;

        // Объекты синхронизации для разных целей
        private readonly object _progressLock = new object();
        private readonly object _completeEventLock = new object();

        public AudioManager(ConfigManager config, AudioEngineService bass, AudioFile[] audioFiles, string outputPath, string pattern, string format, EncoderPreset preset)
        {
            _config = config;
            _bass = bass;
            _audioFiles = audioFiles;
            _totalFiles = audioFiles.Length;
            _fileProgress = new float[_totalFiles];
            _outputPath = outputPath;
            _pattern = pattern;
            _format = format.ToLower();
            if (preset != null)
                _preset = preset;
            else
                _preset = new EncoderPreset();

            _aborted = 0;
            _allCompleted = 0;
            _encoders = new ConcurrentBag<IEncoder>();

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
        }

        public void Convert(int threads, MetaService _metaService)
        {
            Task.Factory.StartNew(() =>
            {
                // Атомарный сброс счетчиков
                Interlocked.Exchange(ref _completedFilesCount, 0);
                Interlocked.Exchange(ref _allCompleted, 0);
                Interlocked.Exchange(ref _aborted, 0);

                // Потокобезопасный сброс массива прогресса
                lock (_progressLock)
                {
                    _fileProgress = new float[_totalFiles];
                }

                _encoders = new ConcurrentBag<IEncoder>();

                Parallel.For(0, _audioFiles.Length, new ParallelOptions
                {
                    MaxDegreeOfParallelism = threads
                }, index =>
                {
                    // Сохраняем индекс для использования в замыканиях
                    int fileIndex = index;

                    try
                    {
                        // Атомарная проверка флага отмены
                        if (Interlocked.CompareExchange(ref _aborted, 0, 0) == 1)
                            return;

                        var file = _audioFiles[fileIndex];
                        string fileName = Path.GetFileNameWithoutExtension(file.FilePath);
                        string outputAudio = Path.Combine(_outputPath, fileName);

                        // Создаем энкодер и сразу добавляем в коллекцию
                        IEncoder encoder = EncoderFactory.Create(_format, _bass, file);
                        _encoders.Add(encoder);

                        if (_pattern != String.Empty)
                        {
                            var generator = new PathPatternService(_outputPath, outputAudio, _pattern);
                            outputAudio = generator.GeneratePath(file.FilePath, encoder.Extension);
                        }

                        var checkFile = outputAudio;
                        if (!_config.Settings.ConverterSettings.RewriteAudio && File.Exists(checkFile))
                        {
                            file.Working = false;
                            file.Completed = true;

                            // Потокобезопасное обновление прогресса
                            lock (_progressLock)
                            {
                                _fileProgress[fileIndex] = 100f;
                            }

                            // Атомарный инкремент счетчика завершенных файлов
                            Interlocked.Increment(ref _completedFilesCount);

                            ExceptionManager.RaiseError(Error.FILE_ALREADY_EXISTS, checkFile);
                            OnFileExists?.Invoke(fileIndex);

                            // Проверяем, не завершены ли все файлы
                            CheckAllCompleted();
                            return;
                        }

                        // Исправленные замыкания - используем локальную переменную fileIndex
                        encoder.OnProgress += (idx, progress) =>
                        {
                            // Проверяем, что событие относится к текущему файлу
                            if (idx != fileIndex)
                                return;

                            OnProgress?.Invoke(fileIndex, progress);

                            lock (_progressLock)
                            {
                                _fileProgress[fileIndex] = progress;

                                // Оптимизированный подсчет общего прогресса
                                float total = 0;
                                for (int i = 0; i < _fileProgress.Length; i++)
                                {
                                    total += _fileProgress[i];
                                }
                                float overallProgress = total / _totalFiles;
                                OnOverallProgress?.Invoke(overallProgress);
                            }
                        };

                        encoder.OnComplete += (idx, status) =>
                        {
                            // Проверяем, что событие относится к текущему файлу
                            if (idx != fileIndex)
                                return;

                            // Проверяем, не было ли уже глобальное завершение
                            if (Interlocked.CompareExchange(ref _allCompleted, 0, 0) == 1)
                                return;

                            // Вызываем событие завершения файла
                            OnComplete?.Invoke(fileIndex, status);

                            lock (_progressLock)
                            {
                                _fileProgress[fileIndex] = 100;
                            }

                            // Атомарный инкремент и проверка завершения всех файлов
                            int completed = Interlocked.Increment(ref _completedFilesCount);

                            if (completed == _totalFiles)
                            {
                                // Пытаемся атомарно установить флаг глобального завершения
                                if (Interlocked.CompareExchange(ref _allCompleted, 1, 0) == 0)
                                {
                                    // Успешно установили флаг, вызываем событие
                                    OnAllComplete?.Invoke(true);
                                }
                            }

                            // Обновляем общий прогресс
                            lock (_progressLock)
                            {
                                float total = 0;
                                for (int i = 0; i < _fileProgress.Length; i++)
                                {
                                    total += _fileProgress[i];
                                }
                                float overallProgress = total / _totalFiles;
                                OnOverallProgress?.Invoke(overallProgress);
                            }
                        };

                        bool isOk = encoder.Encode(outputAudio, _preset, 1, 1);
                        if (isOk)
                        {
                            _metaService.CopyMetadata(file.FilePath, outputAudio);
                        }
                        else
                        {
                            ExceptionManager.RaiseError(Error.ENCODE_FAILED, checkFile);
                        }
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.RaiseError(Error.ENCODE_FAILED, ex.Message);
                        // Атомарная установка флага отмены
                        Interlocked.Exchange(ref _aborted, 1);
                    }
                });
            });
        }

        private void CheckAllCompleted()
        {
            // Проверяем, завершены ли все файлы
            int completed = Interlocked.CompareExchange(ref _completedFilesCount, 0, 0);
            if (completed == _totalFiles)
            {
                // Пытаемся атомарно установить флаг глобального завершения
                if (Interlocked.CompareExchange(ref _allCompleted, 1, 0) == 0)
                {
                    OnAllComplete?.Invoke(true);
                }
            }
        }

        public void AbortAll()
        {
            // Атомарная установка флага отмены
            Interlocked.Exchange(ref _aborted, 1);
            OnAbort?.Invoke();

            // Потокобезопасное копирование энкодеров для остановки
            var encodersToAbort = _encoders?.ToList() ?? new List<IEncoder>();
            foreach (var encoder in encodersToAbort)
            {
                try
                {
                    encoder.AbortEncode();
                }
                catch
                {
                    // Игнорируем ошибки при остановке
                }
            }
        }
    }
}