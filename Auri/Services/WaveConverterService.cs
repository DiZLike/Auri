using System;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.Misc;

namespace Auri.Services
{
    public class WaveConverterService
    {
        public event Action<string> OnError;
        public event Action<float> OnProgress;
        public event Action<bool> OnComplete;

        private bool _isUserStopped;
        private readonly BassAudioService _bassAudioService;

        public WaveConverterService(BassAudioService bassAudioService)
        {
            _bassAudioService = bassAudioService;
        }

        public void ConvertToWav(string inputFile, string outputFile, int sampleRate, int channels, int bitsPerSample)
        {
            if (!File.Exists(inputFile))
            {
                OnError?.Invoke($"Входной файл не найден: {inputFile}");
                return;
            }

            // Проверка битности
            if (!IsValidBitDepth(bitsPerSample))
            {
                OnError?.Invoke($"Неподдерживаемая битность: {bitsPerSample}. Используйте 8, 16, 24 или 32");
                return;
            }

            _isUserStopped = false;

            try
            {
                int sourceStream = CreateSourceStream(inputFile);
                if (sourceStream == 0)
                    return;

                try
                {
                    BASS_CHANNELINFO info = Bass.BASS_ChannelGetInfo(sourceStream);

                    int targetSampleRate = sampleRate > 0 ? sampleRate : info.freq;
                    int targetChannels = channels > 0 ? channels : info.chans;

                    int mixerStream = CreateMixerStream(sourceStream, targetSampleRate, targetChannels);
                    if (mixerStream == 0)
                        return;

                    try
                    {
                        WriteWaveFile(mixerStream, outputFile, targetSampleRate, targetChannels, bitsPerSample);
                    }
                    finally
                    {
                        if (mixerStream != sourceStream)
                            Bass.BASS_StreamFree(mixerStream);
                    }
                }
                finally
                {
                    Bass.BASS_StreamFree(sourceStream);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Ошибка при конвертации: {ex.Message}");
            }
        }

        private int CreateSourceStream(string inputFile)
        {
            int stream = Bass.BASS_StreamCreateFile(inputFile, 0, 0,
                BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);

            if (stream == 0)
            {
                BASSError error = Bass.BASS_ErrorGetCode();
                OnError?.Invoke($"Ошибка создания потока ({error}) для файла {inputFile}");
                return 0;
            }

            return stream;
        }

        private int CreateMixerStream(int sourceStream, int targetSampleRate, int targetChannels)
        {
            BASS_CHANNELINFO info = Bass.BASS_ChannelGetInfo(sourceStream);

            if (info.freq == targetSampleRate && info.chans == targetChannels)
                return sourceStream;

            int mixer = BassMix.BASS_Mixer_StreamCreate(
                targetSampleRate,
                targetChannels,
                BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);

            if (mixer == 0)
            {
                OnError?.Invoke($"Ошибка создания микшера: {Bass.BASS_ErrorGetCode()}");
                return 0;
            }

            if (!BassMix.BASS_Mixer_StreamAddChannel(mixer, sourceStream,
                BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT))
            {
                OnError?.Invoke($"Ошибка добавления потока в микшер: {Bass.BASS_ErrorGetCode()}");
                Bass.BASS_StreamFree(mixer);
                return 0;
            }

            return mixer;
        }

        private void WriteWaveFile(int stream, string outputFile, int sampleRate, int channels, int bitsPerSample)
        {
            WaveWriter waveWriter = null;

            try
            {
                waveWriter = CreateWaveWriter(outputFile, channels, sampleRate, bitsPerSample);

                // Получаем длину в байтах правильно
                long totalBytes = Bass.BASS_ChannelGetLength(stream);
                if (totalBytes < 0)
                {
                    OnError?.Invoke("Не удалось получить длину файла");
                    return;
                }

                byte[] buffer = new byte[65536];
                int bytesRead;
                long totalBytesRead = 0;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                const int progressUpdateInterval = 100;

                while (!_isUserStopped)
                {
                    bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);

                    if (bytesRead <= 0)
                    {
                        // Проверяем, не было ли ошибки
                        if (bytesRead < 0)
                        {
                            OnError?.Invoke($"Ошибка чтения данных: {Bass.BASS_ErrorGetCode()}");
                        }
                        break;
                    }

                    waveWriter.Write(buffer, bytesRead);
                    totalBytesRead += bytesRead;

                    if (stopwatch.ElapsedMilliseconds >= progressUpdateInterval)
                    {
                        float progress = (totalBytesRead * 100f) / totalBytes;
                        OnProgress?.Invoke((float)Math.Round(progress, 2));
                        stopwatch.Restart();
                    }
                }

                if (_isUserStopped)
                {
                    OnComplete?.Invoke(false);
                }
                else
                {
                    OnProgress?.Invoke(100f);
                    OnComplete?.Invoke(true);
                }
            }
            catch (Exception ex)
            {
                OnError?.Invoke($"Ошибка при записи WAV: {ex.Message}");
            }
            finally
            {
                if (waveWriter != null)
                    waveWriter.Close();
            }
        }

        private WaveWriter CreateWaveWriter(string outputFile, int channels, int sampleRate, int bitsPerSample)
        {
            switch (bitsPerSample)
            {
                case 8:
                    return new WaveWriter(outputFile, channels, sampleRate, 8, true);
                case 16:
                    return new WaveWriter(outputFile, channels, sampleRate, 16, true);
                case 24:
                    return new WaveWriter(outputFile, channels, sampleRate, 24, true);
                case 32:
                    return new WaveWriter(outputFile, channels, sampleRate, 32, true);
                default:
                    throw new ArgumentException($"Неподдерживаемая битность: {bitsPerSample}");
            }
        }
        private bool IsValidBitDepth(int bitsPerSample)
        {
            return bitsPerSample == 8 || bitsPerSample == 16 ||
                   bitsPerSample == 24 || bitsPerSample == 32;
        }
        public void ConvertFloatToWav(string inputFile, string outputFile, int bitsPerSample)
        {
            ConvertToWav(inputFile, outputFile, 0, 0, bitsPerSample);
        }
        public void AbortConversion()
        {
            _isUserStopped = true;
        }
    }
}