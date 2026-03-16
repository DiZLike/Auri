using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
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
            BASSFlag flags = BASSFlag.BASS_SAMPLE_FLOAT;
            int stream = Bass.BASS_StreamCreateFile(audioFile, 0, 0,
                BASSFlag.BASS_DEFAULT | BASSFlag.BASS_STREAM_DECODE | flags);
            if (stream == 0)
                OnError?.Invoke($"Ошибка создания потока аудио: {Bass.BASS_ErrorGetCode()}");

            int mixerHandel = BassMix.BASS_Mixer_StreamCreate(sampleRate, chans, BASSFlag.BASS_STREAM_DECODE | flags);
            if (mixerHandel == 0)
                OnError?.Invoke($"Ошибка создания микшера: {Bass.BASS_ErrorGetCode()}");
            bool isError = !BassMix.BASS_Mixer_StreamAddChannel(mixerHandel, stream, BASSFlag.BASS_DEFAULT);
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
            byte[] buffer = new byte[65536];
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
            if (pass == totalPass)
            {
                OnProgress?.Invoke(100f);
                OnComplete.Invoke(true);
            }

            StopEncode(stream, encoder);
        }
        public void StartWaveWrite(int stream, int frequency, int bits, int channels, string outputFile)
        {
            WaveWriter wav = new WaveWriter(outputFile, channels, frequency, bits, true);

            byte[] buffer = new byte[65536];
            int bytesRead;

            do
            {
                bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);
                if (bytesRead > 0)
                    wav.Write(buffer, bytesRead);
            }
            while (bytesRead > 0 && !_isUserStopped);
            wav.Close();
        }
        public void AbortEncode()
        {
            _isUserStopped = true;
        }
        public void StopEncode(int stream, int encoder)
        {
            BassEnc.BASS_Encode_Stop(encoder);
            FreeStream(stream);
        }
    }
}