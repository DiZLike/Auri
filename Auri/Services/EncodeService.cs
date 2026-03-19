using System;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;
using Un4seen.Bass.AddOn.Mix;
using Un4seen.Bass.Misc;

namespace Auri.Services
{
    public class EncodeService
    {
        public event Action<string> OnError;
        public event Action<float> OnProgress;
        public event Action<bool> OnComplete;
        private BassAudioService _bass;
        private bool _isUserStopped;
        public EncodeService(BassAudioService bass)
        {
            _bass = bass;
        }

        public int CreateStream(string audioFile, int sampleRate, int chans)
        {
            if (!File.Exists(audioFile))
            {
                OnError?.Invoke($"Файл {audioFile} не найден");
                return 0;
            }
            int stream = Bass.BASS_StreamCreateFile(audioFile, 0, 0,
                BASSFlag.BASS_DEFAULT | BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
            if (stream == 0)
                OnError?.Invoke($"Ошибка создания потока аудио: {Bass.BASS_ErrorGetCode()}");

            int mixerHandel = CreateMixerStream(stream, sampleRate, chans);
            return mixerHandel;
        }
        private int CreateMixerStream(int sourceStream, int targetSampleRate, int targetChannels)
        {
            int mixerHandel = BassMix.BASS_Mixer_StreamCreate(targetSampleRate, targetChannels, BASSFlag.BASS_STREAM_DECODE | BASSFlag.BASS_SAMPLE_FLOAT);
            if (mixerHandel == 0)
                OnError?.Invoke($"Ошибка создания микшера: {Bass.BASS_ErrorGetCode()}");

            bool isError = !BassMix.BASS_Mixer_StreamAddChannel(mixerHandel, sourceStream, BASSFlag.BASS_DEFAULT);
            if (isError)
                OnError?.Invoke($"Ошибка добавления потока в микшер: {Bass.BASS_ErrorGetCode()}");
            return mixerHandel;
        }
        public void FreeStream(int stream)
        {
            Bass.BASS_StreamFree(stream);
        }
        public int CreateEncoder(int stream, string encoderPath, string args, int bitsPerSample)
        {
            BASSEncode flags = BASSEncode.BASS_ENCODE_DEFAULT;
            if (bitsPerSample == 32)
                flags |= BASSEncode.BASS_ENCODE_FP_32BIT;
            else if (bitsPerSample == 24)
                flags |= BASSEncode.BASS_ENCODE_FP_24BIT;
            else if (bitsPerSample == 16)
                flags |= BASSEncode.BASS_ENCODE_FP_16BIT;
            else if (bitsPerSample == 8)
                flags = BASSEncode.BASS_ENCODE_FP_8BIT;

            int encoder = BassEnc.BASS_Encode_Start(
                stream, $"\"{encoderPath}\" {args}", flags, null, IntPtr.Zero);
            if (encoder == 0)
                OnError?.Invoke($"Ошибка инициализации энкодера: {Bass.BASS_ErrorGetCode()}");
            return encoder;
        }
        public void StartEncode(int stream, int encoder, int pass, int totalPass)
        {
            _isUserStopped = false;
            long fileLength = Bass.BASS_ChannelGetLength(stream);
            float[] buffer = new float[65536];
            int bytesRead;

            float lastProgress = -1;
            const float progressThreshold = 0.1f;

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            const int minUpdateIntervalMs = 100;

            do
            {
                bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);
                if (bytesRead > 0)
                {
                    if (stopwatch.ElapsedMilliseconds >= minUpdateIntervalMs)
                    {
                        long currentPos = Bass.BASS_ChannelGetPosition(stream);
                        float currentPassProgress = (currentPos * 100f) / fileLength;

                        float totalProgress;

                        if (totalPass > 1)
                        {
                            float passWeight = 100f / totalPass;
                            float completedPassesProgress = (pass - 1) * passWeight;
                            float currentPassContribution = (currentPassProgress * passWeight) / 100f;
                            totalProgress = completedPassesProgress + currentPassContribution;
                        }
                        else
                        {
                            // Если только один проход, используем обычный прогресс
                            totalProgress = currentPassProgress;
                        }

                        if (Math.Abs(totalProgress - lastProgress) >= progressThreshold)
                        {
                            lastProgress = totalProgress;
                            OnProgress?.Invoke((float)Math.Round(totalProgress, 2));
                            stopwatch.Restart();
                        }
                    }
                }
            }
            while (bytesRead > 0 && !_isUserStopped);

            // Убеждаемся, что отправляем 100% только когда все проходы завершены
            if (pass == totalPass && !_isUserStopped)
            {
                OnProgress?.Invoke(100f);
                OnComplete.Invoke(true);
            }

            StopEncode(stream, encoder);
        }
        public void WriteWaveFile(int stream, string outputFile, int sampleRate, int channels, int bitsPerSample)
        {
            WaveWriter waveWriter = null;

            try
            {
                waveWriter = CreateWaveWriter(outputFile, channels, sampleRate, bitsPerSample);

                long totalBytes = Bass.BASS_ChannelGetLength(stream);
                if (totalBytes < 0)
                {
                    OnError?.Invoke("Не удалось получить длину файла");
                    return;
                }

                float[] buffer = new float[65536];
                int bytesRead;
                long totalBytesRead = 0;

                var stopwatch = System.Diagnostics.Stopwatch.StartNew();
                const int progressUpdateInterval = 100;

                while (!_isUserStopped)
                {
                    bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);

                    if (bytesRead <= 0)
                        break;

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
                    //OnComplete?.Invoke(false);
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
                StopEncode(stream, 0);
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
                    throw new ArgumentException($"Неподдерживаемая разрядность: {bitsPerSample}");
            }
        }
        public void AbortEncode()
        {
            _isUserStopped = true;
        }
        public void StopEncode(int stream, int encoder)
        {
            if (encoder != 0)
                BassEnc.BASS_Encode_Stop(encoder);
            if (stream != 0)
                FreeStream(stream);
        }
    }
}