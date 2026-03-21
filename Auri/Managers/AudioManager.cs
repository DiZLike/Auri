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

        private volatile float[] _fileProgress;
        private int _totalFiles;
        private int _completedFilesCount;
        private bool _allCompleted;
        private bool _aborted;

        private ConcurrentBag<IEncoder> _encoders;

        // Объекты синхронизации
        private readonly object _progressLock = new object();
        private readonly object _stateLock = new object();
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

            _aborted = false;
            _allCompleted = false;
            _encoders = new ConcurrentBag<IEncoder>();

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
        }

        public void Convert(int threads, MetaService _metaService)
        {
            Task.Factory.StartNew(() =>
            {
                // Сброс состояния под блокировкой
                lock (_stateLock)
                {
                    _completedFilesCount = 0;
                    _allCompleted = false;
                    _aborted = false;
                }

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
                    int fileIndex = index;

                    try
                    {
                        // Проверка флага отмены под блокировкой
                        lock (_stateLock)
                        {
                            if (_aborted)
                                return;
                        }

                        var file = _audioFiles[fileIndex];
                        string fileName = Path.GetFileNameWithoutExtension(file.FilePath);
                        string outputAudio = Path.Combine(_outputPath, fileName);

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

                            lock (_progressLock)
                            {
                                _fileProgress[fileIndex] = 100f;
                            }

                            lock (_stateLock)
                            {
                                _completedFilesCount++;
                            }

                            ExceptionManager.RaiseError(Error.FILE_ALREADY_EXISTS, checkFile);
                            OnFileExists?.Invoke(fileIndex);

                            CheckAllCompleted();
                            return;
                        }

                        encoder.OnProgress += (idx, progress) =>
                        {
                            if (idx != fileIndex)
                                return;

                            OnProgress?.Invoke(fileIndex, progress);

                            lock (_progressLock)
                            {
                                _fileProgress[fileIndex] = progress;

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
                            if (idx != fileIndex)
                                return;

                            bool shouldTriggerAllComplete = false;

                            lock (_stateLock)
                            {
                                if (_allCompleted)
                                    return;

                                OnComplete?.Invoke(fileIndex, status);

                                lock (_progressLock)
                                {
                                    _fileProgress[fileIndex] = 100;
                                }

                                _completedFilesCount++;

                                if (_completedFilesCount == _totalFiles && !_allCompleted)
                                {
                                    _allCompleted = true;
                                    shouldTriggerAllComplete = true;
                                }
                            }

                            if (shouldTriggerAllComplete)
                            {
                                OnAllComplete?.Invoke(true);
                            }

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
                        lock (_stateLock)
                        {
                            _aborted = true;
                        }
                    }
                });
            });
        }

        private void CheckAllCompleted()
        {
            bool shouldTrigger = false;

            lock (_stateLock)
            {
                if (_completedFilesCount == _totalFiles && !_allCompleted)
                {
                    _allCompleted = true;
                    shouldTrigger = true;
                }
            }

            if (shouldTrigger)
            {
                OnAllComplete?.Invoke(true);
            }
        }

        public void AbortAll()
        {
            lock (_stateLock)
            {
                _aborted = true;
            }

            OnAbort?.Invoke();

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