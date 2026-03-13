using Auri.Audio;
using Auri.Audio.Encoder;
using Auri.Services;
using System;
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
    public class ConverterManager
    {
        public event Action<string> OnError;
        public event Action<int, float> OnProgress;
        public event Action<float> OnOverallProgress;
        public event Action<int, bool> OnComplete;

        private readonly BassAudioService _bass;
        private readonly AudioFile[] _audioFiles;
        private readonly string _outputPath;
        private readonly string _format;
        private readonly EncoderSettings _settings;

        private float[] _fileProgress;
        private int _totalFiles;

        private int _currentAudio;
        private List<IEncoder> _encoders;

        public ConverterManager(BassAudioService bass, AudioFile[] audioFiles, string outputPath, string format, EncoderSettings settings)
        {
            _bass = bass;
            _audioFiles = audioFiles;
            _totalFiles = audioFiles.Length;
            _fileProgress = new float[_totalFiles];
            _outputPath = outputPath;
            _format = format.ToLower();
            _settings = settings;

            if (!Directory.Exists(outputPath))
                Directory.CreateDirectory(outputPath);
        }
        public void Convert()
        {
            Task.Factory.StartNew(() =>
            {
                // Используем потокобезопасную сумму или блокировку
                var lockObject = new object();
                float overallProgress = 0;

                Parallel.For(0, _audioFiles.Length, new ParallelOptions
                {
                    MaxDegreeOfParallelism = Environment.ProcessorCount
                }, index =>
                {
                    var file = _audioFiles[index];
                    string fileName = Path.GetFileName(file.FilePath);
                    string outputAudio = Path.Combine(_outputPath, Path.ChangeExtension(fileName, $".{_format}"));
                    IEncoder encoder = EncoderFactory.Create(_format, _bass, file);

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

                        // Вызываем событие вне блокировки
                        OnOverallProgress?.Invoke((int)overallProgress);
                    };

                    encoder.OnComplete += (fileIndex, status) =>
                    {
                        OnComplete?.Invoke(fileIndex, status);

                        // При завершении файла можно установить 100% прогресс для него
                        lock (lockObject)
                        {
                            _fileProgress[fileIndex] = 100;
                            bool allCompleted = _fileProgress.All(progress => progress == 100);

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
                    encoder.Encode(outputAudio, _settings);
                });
            });
        }

    }
}