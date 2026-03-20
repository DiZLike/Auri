using Auri.Managers;
using System;
using System.Collections.Generic;
using System.IO;
using Un4seen.Bass;
using Un4seen.Bass.AddOn.Enc;

namespace Auri.Services
{
    public class AudioEngineService
    {
        private bool _isInitialized;
        private List<int> _pluginsHandle;
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
            _pluginsHandle = new List<int>();
            foreach (var dll in dlls)
            {
                int pHandle = Bass.BASS_PluginLoad(dll);
                if (pHandle == 0)
                    ExceptionManager.RaiseBassError(Error.PLUGIN_LOAD_FAILED, dll);
                _pluginsHandle.Add(pHandle);
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
        public void EngineFree()
        {
            // Очистка плагинов
            if (_pluginsHandle != null && _pluginsHandle.Count > 0)
            {
                foreach (var plugin in _pluginsHandle)
                    Bass.BASS_PluginFree(plugin);
            }

            // Очистка Bass
            Bass.BASS_Free();
        }
    }
}