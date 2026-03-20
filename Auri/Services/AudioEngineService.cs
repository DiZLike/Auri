using Auri.Managers;
using System;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace Auri.Services
{
    public class AudioEngineService
    {
        private bool _isInitialized;
        private string _decPluginsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "plugins", "decoders");

        public AudioEngineService()
        {
            if (!InitializeBass())
                return;
            PluginsLoad();
        }
        private bool InitializeBass()
        {
            _isInitialized = Bass.BASS_Init(-1, 44100, BASSInit.BASS_DEVICE_DEFAULT, IntPtr.Zero);
            if (!_isInitialized)
                ExceptionManager.RaiseBassError(Error.BASS_INIT_FAILED);
            return _isInitialized;
        }
        private bool PluginsLoad()
        {
            if (!Directory.Exists(_decPluginsPath))
            {
                ExceptionManager.RaiseError(Error.PLUGINS_FOLDER_MISSING);
                return false;
            }

            var dlls = Directory.GetFiles(_decPluginsPath, "*.dll", SearchOption.TopDirectoryOnly);
            if (dlls.Length < 1)
            {
                ExceptionManager.RaiseError(Error.PLUGINS_FOLDER_MISSING);
                return false;
            }
            foreach (var dll in dlls)
            {
                int pHandle = Bass.BASS_PluginLoad(dll);
                if (pHandle == 0)
                    ExceptionManager.RaiseBassError(Error.PLUGIN_LOAD_FAILED);
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