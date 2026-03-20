using Auri.Managers;
using Auri.Services;
using System;
using System.IO;

namespace Auri.Audio.Encoder.Codec
{
    public class FlacEncoder : EncoderBase
    {
        private const string LossyWavSubPath = "lossywav";
        private const string LossyWavExecutable = "lossyWAV.exe";
        private const string FlacSubPath = "flac";
        private const string FlacExecutable = "flac.exe";
        private const string StdinLossyWavFile = "stdin.lossy.wav";
        private const int DefaultCompressionLevel = 8;
        private const string DefaultLossyWavQuality = "S";

        public override string Extension { get; } = "flac";

        protected override string EncoderSubPath { get; set; } = FlacSubPath;
        protected override string EncoderFileName { get; set; } = FlacExecutable;

        private bool _lossyWavCompleted;
        private string _tempLossyWavFolder;

        public FlacEncoder(AudioEngineService bass, AudioFile inputAudio)
            : base(bass, inputAudio)
        {
        }

        protected override string BuildArguments(EncoderPreset settings, string outputAudio)
        {
            var (compressionLevel, useLossyWav, lossyWavQuality) = ExtractEncoderSettings(settings);
            var outputFileName = Path.GetFileNameWithoutExtension(outputAudio);
            var outputFolder = Path.Combine(Path.GetDirectoryName(outputAudio) ?? string.Empty, outputFileName);
            _tempLossyWavFolder = outputFolder;

            if (useLossyWav && !_lossyWavCompleted)
            {
                return BuildLossyWavArguments(lossyWavQuality, outputFolder);
            }

            if (useLossyWav && _lossyWavCompleted)
            {
                return BuildFlacFromLossyWavArguments(compressionLevel, outputFolder, outputAudio);
            }

            return BuildStandardFlacArguments(compressionLevel, outputAudio);
        }

        private (int compressionLevel, bool useLossyWav, string lossyWavQuality) ExtractEncoderSettings(EncoderPreset settings)
        {
            var compressionLevel = DefaultCompressionLevel;
            if (settings.CustomParams?.TryGetValue("Compress", out var compressObj) == true && compressObj is int compressInt)
                compressionLevel = compressInt;

            var useLossyWav = false;
            if (settings.CustomParams?.TryGetValue("UseLossyWav", out var useLossyObj) == true && useLossyObj is bool useLossyBool)
                useLossyWav = useLossyBool;

            var lossyWavQuality = DefaultLossyWavQuality;
            if (settings.CustomParams?.TryGetValue("LossyWavQuality", out var qualityObj) == true && qualityObj is string qualityString)
                lossyWavQuality = qualityString;

            return (compressionLevel, useLossyWav, lossyWavQuality);
        }

        private string BuildLossyWavArguments(string quality, string outputFolder)
        {
            EncoderSubPath = LossyWavSubPath;
            EncoderFileName = LossyWavExecutable;

            if (!Directory.Exists(outputFolder))
                Directory.CreateDirectory(outputFolder);

            return $"- -q {quality} -f -o \"{outputFolder}\"";
        }

        private string BuildFlacFromLossyWavArguments(int compressionLevel, string outputFolder, string outputAudio)
        {
            EncoderSubPath = FlacSubPath;
            EncoderFileName = FlacExecutable;

            var lossyWavFilePath = Path.Combine(outputFolder, StdinLossyWavFile);
            return $"-{compressionLevel} -f \"{lossyWavFilePath}\" --output-name=\"{outputAudio}\"";
        }

        private string BuildStandardFlacArguments(int compressionLevel, string outputAudio)
        {
            return $"-{compressionLevel} -f - --output-name=\"{outputAudio}\"";
        }

        private void CleanupTempFiles()
        {
            if (string.IsNullOrEmpty(_tempLossyWavFolder) || !Directory.Exists(_tempLossyWavFolder))
                return;

            try
            {
                // временная папка уникальна для каждого трека (т.е. потока)
                var lossyWavFile = Path.Combine(_tempLossyWavFolder, StdinLossyWavFile);
                if (File.Exists(lossyWavFile))
                    File.Delete(lossyWavFile);

                if (Directory.GetFiles(_tempLossyWavFolder).Length == 0)
                    Directory.Delete(_tempLossyWavFolder);
            }
            catch
            {
                ExceptionManager.RaiseError(Error.FILE_TEMP_DELETE);
            }
        }

        public override bool Encode(string outputAudio, EncoderPreset settings, int pass, int totalPass)
        {
            var (_, useLossyWav, _) = ExtractEncoderSettings(settings);

            try
            {
                if (!useLossyWav)
                    return base.Encode(outputAudio, settings, pass, totalPass);

                // Сбрасываем флаг при начале кодирования
                _lossyWavCompleted = false;

                // Первый проход
                bool firstPassSuccess = base.Encode(outputAudio, settings, 1, 2);

                // Проверяем, не была ли операция прервана
                if (!firstPassSuccess || _encoderService.IsAborted)
                {
                    return false;
                }

                _lossyWavCompleted = true;

                // Второй проход
                return base.Encode(outputAudio, settings, 2, 2);
            }
            finally
            {
                // Очищаем временные файлы только если оба прохода завершены или был abort
                if (useLossyWav && (_lossyWavCompleted || _encoderService.IsAborted))
                {
                    CleanupTempFiles();
                }
            }
        }
    }
}