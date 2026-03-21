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
        private PresetManager _presets;
        private readonly string _configPath;
        private AppSettings _settings;

        public AppSettings Settings => _settings;
        public bool NoConfigured {  get; set; }
        public ConfigManager()
        {
            _presets = new PresetManager();
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
                ExceptionManager.RaiseError(Error.CONFIG_SAVE_FAILED, ex.Message);
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
                {
                    NoConfigured = true;
                    _settings = new AppSettings();
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.RaiseError(Error.CONFIG_LOAD_FAILED, ex.Message);
                _settings = new AppSettings();
            }
        }

        // Пресеты
        public EncoderPreset GetEncoderPreset(string format, int index)
        {
            return _presets.GetPreset(format, index);
        }
        public EncoderPreset GetUserEncoderPreset(string format)
        {
            return _presets.GetPreset(format, 0xFF);
        }
        public void SaveUserEncoderPreset(string format, EncoderPreset settings)
        {
            _presets.SaveCustomPreset(format, settings);
        }
        public EncoderPreset GetQuickStartPreset(string format)
        {
            return _presets.GetQuickStartPreset(format);
        }
        public void SaveQuickStartPreset(string format, EncoderPreset preset)
        {
            _presets.SaveQuickStartPreset(format, preset);
        }
    }
}