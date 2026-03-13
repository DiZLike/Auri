using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

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

        public int CreateStream(string audioFile)
        {
            if (!File.Exists(audioFile))
            {
                OnError?.Invoke($"Файл {audioFile} не найден");
                return 0;
            }
            int stream = Bass.BASS_StreamCreateFile(audioFile, 0, 0,
                BASSFlag.BASS_DEFAULT | BASSFlag.BASS_SAMPLE_FLOAT | BASSFlag.BASS_STREAM_DECODE);
            if (stream == 0)
                OnError?.Invoke($"Ошибка создания потока аудио: {Bass.BASS_ErrorGetCode()}");
            return stream;
        }
        public void FreeStream(int stream)
        {
            Bass.BASS_StreamFree(stream);
        }
        public int CreateEncoder(int stream, string encoderPath, string args)
        {
            int encoder = BassEnc.BASS_Encode_Start(
                stream, $"\"{encoderPath}\" {args}", 0, null, IntPtr.Zero);
            if (encoder == 0)
                OnError?.Invoke($"Ошибка инициализации энкодера: {Bass.BASS_ErrorGetCode()}");
            return encoder;
        }

        public void StartEncode(int stream, int encoder)
        {
            long fileLength = Bass.BASS_ChannelGetLength(stream);
            byte[] buffer = new byte[65536];
            int bytesRead;

            // Оптимизация 1: Кэшируем последнее значение прогресса
            float lastProgress = -1;
            const float progressThreshold = 0.1f; // Минимальное изменение для вызова события

            // Оптимизация 2: Используем таймер для ограничения частоты обновлений
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            const int minUpdateIntervalMs = 100; // Минимум 100ms между обновлениями

            do
            {
                bytesRead = Bass.BASS_ChannelGetData(stream, buffer, buffer.Length);
                if (bytesRead > 0)
                {
                    // Оптимизация 3: Вычисляем прогресс только если прошло достаточно времени
                    if (stopwatch.ElapsedMilliseconds >= minUpdateIntervalMs)
                    {
                        long currentPos = Bass.BASS_ChannelGetPosition(stream);
                        float progress = (currentPos * 100f) / fileLength;

                        // Оптимизация 4: Обновляем только если прогресс изменился значительно
                        if (Math.Abs(progress - lastProgress) >= progressThreshold)
                        {
                            lastProgress = progress;
                            OnProgress?.Invoke((float)Math.Round(progress, 2));
                            stopwatch.Restart();
                        }
                    }
                }
            }
            while (bytesRead > 0 && !_isUserStopped);

            // Оптимизация 5: Гарантируем отправку 100% в конце
            OnProgress?.Invoke(100f);
            OnComplete.Invoke(true);

            StopEncode(stream, encoder);
        }
        public void StopEncode()
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
