using System;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace Auri.Services
{
    public class BassAudioService
    {
        public event Action<string> OnError;

        public EncodeService Encoder { get; set; }

        private bool _isInitialized;
        private string _decPluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "decoders");

        public BassAudioService()
        {
            if (!InitializeBass())
                return;
            PluginsLoad();

            Encoder = new EncodeService(this);
            Encoder.OnError += (msg) => OnError?.Invoke(msg);
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
    }
}