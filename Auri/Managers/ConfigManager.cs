using Auri.Audio.Encoder;
using Auri.Config;
using Newtonsoft.Json;
using System;
using System.Diagnostics;
using System.IO;

namespace Auri.Managers
{
    public class ConfigManager
    {
        private EncoderPresetManager _presets;
        private readonly string _configPath;
        private AppSettings _settings;

        public AppSettings Settings => _settings;
        public ConfigManager()
        {
            _presets = new EncoderPresetManager();
            _configPath = Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory,
                "settings.json");
            LoadSettings();
        }

        public void SaveSettings()
        {
            try
            {
                string directory = Path.GetDirectoryName(_configPath);
                if (!Directory.Exists(directory))
                    Directory.CreateDirectory(directory);
                string json = JsonConvert.SerializeObject(_settings, Formatting.Indented);

                File.WriteAllText(_configPath, json);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Ошибка сохранения настроек: {ex.Message}");
            }
        }
        private void LoadSettings()
        {
            try
            {
                if (File.Exists(_configPath))
                {
                    string json = File.ReadAllText(_configPath);
                    _settings = JsonConvert.DeserializeObject<AppSettings>(json);
                }
                else
                    _settings = new AppSettings();
            }
            catch (Exception)
            {
                _settings = new AppSettings();
            }
        }

        // Пресеты
        public EncoderSettings GetEncoderPreset(string format, int index)
        {
            return _presets.GetPreset(format, index);
        }
        public EncoderSettings GetUserEncoderPreset(string format)
        {
            return _presets.GetPreset(format, 0xFF);
        }
        public void SaveUserEncoderPreset(string format, EncoderSettings settings)
        {
            _presets.SaveCustomPreset(format, settings);
        }


    }
}