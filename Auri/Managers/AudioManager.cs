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
        private volatile bool _allCompleted;
        private bool _aborted;
        private ConcurrentBag<IEncoder> _encoders;

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
            _encoders = new ConcurrentBag<IEncoder>();

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
        }
        public void Convert(int threads, MetaService _metaService)
        {
            Task.Factory.StartNew(() =>
            {
                _completedFilesCount = 0;
                _allCompleted = false;
                _aborted = false;
                _fileProgress = new float[_totalFiles]; // Сбрасываем прогресс
                _encoders = new ConcurrentBag<IEncoder>();

                // Используем потокобезопасную сумму или блокировку
                var lockObject = new object();
                float overallProgress = 0;

                Parallel.For(0, _audioFiles.Length, new ParallelOptions
                {
                    MaxDegreeOfParallelism = threads
                }, index =>
                {
                    try
                    {
                        if (_aborted)
                            return;
                        var file = _audioFiles[index];
                        string fileName = Path.GetFileNameWithoutExtension(file.FilePath);
                        string outputAudio = Path.Combine(_outputPath, fileName);

                        if (_pattern != String.Empty)
                        {
                            var generator = new PathPatternService(_outputPath, outputAudio, _pattern);
                            outputAudio = generator.GeneratePath(file.FilePath);
                        }
                        IEncoder encoder = EncoderFactory.Create(_format, _bass, file);
                        _encoders.Add(encoder);
                        var checkFile = outputAudio + encoder.Extension;
                        if (!_config.Settings.ConverterSettings.RewriteAudio && File.Exists(checkFile))
                        {
                            file.Working = false;
                            file.Completed = true;
                            _fileProgress[file.Index] = 100f;
                            _completedFilesCount++;
                            ExceptionManager.RaiseError(Error.FILE_ALREADY_EXISTS, checkFile);
                            OnFileExists?.Invoke(file.Index);
                            return;
                        }

                        encoder.OnProgress += (fileIndex, progress) =>
                        {
                            OnProgress?.Invoke(fileIndex, progress);

                            // Потокобезопасное обновление прогресса файла
                            lock (lockObject)
                            {
                                _fileProgress[fileIndex] = progress;

                                // Пересчитываем общий прогресс
                                float total = 0;
                                for (int i = 0; i < _fileProgress.Length; i++)
                                {
                                    total += _fileProgress[i];
                                }
                                overallProgress = total / _totalFiles;
                            }

                            OnOverallProgress?.Invoke((int)overallProgress);
                        };
                        encoder.OnComplete += (fileIndex, status) =>
                        {
                            if (_allCompleted) return;
                            OnComplete?.Invoke(fileIndex, status);

                            // При завершении файла можно установить 100% прогресс для него
                            lock (lockObject)
                            {
                                _fileProgress[fileIndex] = 100;
                                _completedFilesCount++;

                                // Пересчитываем общий прогресс
                                float total = 0;
                                for (int i = 0; i < _fileProgress.Length; i++)
                                {
                                    total += _fileProgress[i];
                                }
                                overallProgress = total / _totalFiles;
                                if (_completedFilesCount == _totalFiles)
                                {
                                    _allCompleted = true;
                                    OnAllComplete?.Invoke(true);
                                }
                            }

                            OnOverallProgress?.Invoke((int)overallProgress);
                        };

                        bool isOk = encoder.Encode(outputAudio, _preset, 1, 1);
                        if (isOk)
                        {
                            string fullOutputPath = outputAudio + encoder.Extension;
                            _metaService.CopyMetadata(file.FilePath, fullOutputPath);
                        }
                        else
                            ExceptionManager.RaiseError(Error.ENCODE_FAILED, checkFile);
                    }
                    catch (Exception ex)
                    {
                        ExceptionManager.RaiseError(Error.ENCODE_FAILED, ex.Message);
                        _aborted = true;
                    }
                });
            });
        }
        public void AbortAll()
        {
            _aborted = true;
            OnAbort?.Invoke();
            foreach (var encoder in _encoders)
                encoder.AbortEncode();
        }

    }
}