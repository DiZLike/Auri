using Engine.Audio;
using Engine.Audio.Encoder;
using Engine.Services;
using System.Collections.Concurrent;

namespace Engine.Managers
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
        private float _totalProgress;
        private int _totalFiles;
        private int _completedFilesCount;
        private bool _allCompleted;
        private bool _aborted;

        private ConcurrentBag<IEncoder> _encoders;

        // Объекты синхронизации
        private readonly object _progressLock = new object();
        private readonly object _stateLock = new object();

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
            _preset = preset ?? new EncoderPreset();

            _aborted = false;
            _allCompleted = false;
            _encoders = new ConcurrentBag<IEncoder>();

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
        }

        public void Convert(int threads, MetaService metaService)
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
                    _totalProgress = 0;
                }

                _encoders = new ConcurrentBag<IEncoder>();
                Parallel.For(0, _audioFiles.Length, new ParallelOptions
                {
                    MaxDegreeOfParallelism = threads
                }, index =>
                {
                    ProcessFile(index, metaService);
                });
            });
        }

        private void ProcessFile(int fileIndex, MetaService metaService)
        {
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

                if (_pattern != string.Empty)
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
                        float oldProgress = _fileProgress[fileIndex];
                        _fileProgress[fileIndex] = 100f;
                        _totalProgress += (100f - oldProgress);
                    }

                    lock (_stateLock)
                        _completedFilesCount++;

                    ExceptionManager.RaiseError(Error.FILE_ALREADY_EXISTS, checkFile);
                    OnFileExists?.Invoke(fileIndex);

                    TryTriggerAllComplete();
                    return;
                }

                encoder.OnProgress += (idx, progress) =>
                {
                    if (idx != fileIndex)
                        return;

                    OnProgress?.Invoke(fileIndex, progress);

                    lock (_progressLock)
                    {
                        float oldProgress = _fileProgress[fileIndex];
                        _fileProgress[fileIndex] = progress;
                        _totalProgress += (progress - oldProgress);
                        OnOverallProgress?.Invoke(_totalProgress / _totalFiles);
                    }
                };

                encoder.OnComplete += (idx, status) =>
                {
                    if (idx != fileIndex)
                        return;

                    lock (_stateLock)
                    {
                        if (_allCompleted)
                            return;
                        OnComplete?.Invoke(fileIndex, status);
                    }

                    lock (_progressLock)
                    {
                        float oldProgress = _fileProgress[fileIndex];
                        _fileProgress[fileIndex] = 100;
                        _totalProgress += (100f - oldProgress);
                    }

                    int completedCount;
                    lock (_stateLock)
                        completedCount = ++_completedFilesCount;

                    if (completedCount == _totalFiles)
                        TryTriggerAllComplete();

                    lock (_progressLock)
                        OnOverallProgress?.Invoke(_totalProgress / _totalFiles);
                };

                bool isOk = encoder.Encode(outputAudio, _preset, 1, 1);
                if (isOk)
                    metaService.CopyMetadata(file.FilePath, outputAudio);
                else
                    ExceptionManager.RaiseError(Error.ENCODE_FAILED, checkFile);
            }
            catch (Exception ex)
            {
                ExceptionManager.RaiseError(Error.ENCODE_FAILED, ex.Message);
                lock (_stateLock)
                    _aborted = true;
                OnComplete?.Invoke(fileIndex, false);
            }
        }

        private void TryTriggerAllComplete()
        {
            lock (_stateLock)
            {
                if (_completedFilesCount == _totalFiles && !_allCompleted)
                {
                    _allCompleted = true;
                    OnAllComplete?.Invoke(true);
                }
            }
        }

        public void AbortAll()
        {
            lock (_stateLock)
                _aborted = true;

            OnAbort?.Invoke();
            foreach (var encoder in _encoders)
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