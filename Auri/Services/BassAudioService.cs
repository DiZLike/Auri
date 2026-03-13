using System;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace Auri.Services
{
    public class BassAudioService
    {
        public event Action<string> OnError;
        //public event Action<float> OnProgress;
        //public event Action<bool> OnComplete;

        private bool _isInitialized;
        private string _decPluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "decoders");

        public BassAudioService()
        {
            if (!InitializeBass())
                return;
            PluginsLoad();
            //Encoder.OnProgress += (progress) => OnProgress?.Invoke(progress);
            //Encoder.OnComplete += (status) => OnComplete?.Invoke(status);
        }
        private bool InitializeBass()
        {
            _isInitialized = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!_isInitialized)
                OnError?.Invoke($"Ошибка инициализации: {Bass.BASS_ErrorGetCode()}");
            return _isInitialized;
        }
        private bool PluginsLoad()
        {
            if (!Directory.Exists(_decPluginsPath))
            {
                OnError?.Invoke("Отсутствуют плагины декодирования аудио. Поддержка только MP3");
                return false;
            }

            var dlls = Directory.GetFiles(_decPluginsPath, "*.dll", SearchOption.TopDirectoryOnly);
            if (dlls.Length < 1)
            {
                OnError?.Invoke("Отсутствуют плагины декодирования аудио. Поддержка только MP3");
                return false;
            }
            foreach (var dll in dlls)
            {
                int pHandle = Bass.BASS_PluginLoad(dll);
                if (pHandle == 0)
                    OnError?.Invoke($"Ошибка загрузки плагина декодирования: {Bass.BASS_ErrorGetCode()}");
            }
            return true;
        }
        public string GetDuration(string audio)
        {
            int stream = Bass.BASS_StreamCreateFile(audio, 0, 0, BASSFlag.BASS_DEFAULT);
            if (stream == 0)
                return "00:00";
            try
            {
                long durBytes = Bass.BASS_ChannelGetLength(stream);
                double seconds = Bass.BASS_ChannelBytes2Seconds(stream, durBytes);
                TimeSpan duration = TimeSpan.FromSeconds(seconds);
                return $"{(int)duration.TotalMinutes:D2}:{duration.Seconds:D2}";
            }
            finally
            {
                // Важно освобождать ресурсы
                Bass.BASS_StreamFree(stream);
            }
        }
    }
}