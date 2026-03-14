using Auri.Audio.Encoder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace Auri.Managers
{
    /// <summary>
    /// Управляет предустановками настроек для различных кодеков
    /// </summary>
    public class EncoderPresetManager
    {
        private readonly string _configPath;

        public EncoderPresetManager()
        {
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoder_presets");
            Directory.CreateDirectory(_configPath);
        }

        /// <summary>
        /// Получить настройки для указанного формата и индекса пресета
        /// </summary>
        public EncoderSettings GetPreset(string format, int presetIndex)
        {
            string lowerFormat = format.ToLower();

            // Индекс 0xFF всегда означает пользовательский пресет для данного формата
            if (presetIndex == 0xFF)
            {
                EncoderSettings customPreset = LoadCustomPreset(lowerFormat);
                if (customPreset != null)
                    return customPreset;
            }

            // Возвращаем стандартный пресет
            EncoderSettings defaultPreset = GetDefaultPreset(lowerFormat, presetIndex);
            return defaultPreset ?? new EncoderSettings();
        }

        /// <summary>
        /// Сохранить пользовательские настройки для формата
        /// </summary>
        public void SaveCustomPreset(string format, EncoderSettings settings)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }

        /// <summary>
        /// Загрузить пользовательские настройки для формата
        /// </summary>
        public EncoderSettings LoadCustomPreset(string format)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<EncoderSettings>(File.ReadAllText(filePath));
            }
            return new EncoderSettings();
        }

        /// <summary>
        /// Получить список доступных форматов
        /// </summary>
        public string[] GetAvailableFormats()
        {
            return new string[] { "opus" };
        }

        /// <summary>
        /// Получить стандартный пресет для формата и индекса
        /// </summary>
        private EncoderSettings GetDefaultPreset(string format, int index)
        {
            string lowerFormat = format.ToLower();

            if (lowerFormat == "opus")
                return GetOpusPreset(index);
            return null;
        }

        #region Opus presets
        private EncoderSettings GetOpusPreset(int index)
        {
            // Корректируем индекс, если он вне диапазона
            if (index < 0 || index > 3)
                index = 1;

            switch (index)
            {
                case 0:
                    return CreateOpusSettings(32, "60", "vbr", "music");
                case 1:
                    return CreateOpusSettings(64, "60", "vbr", "music");
                case 2:
                    return CreateOpusSettings(128, "40", "vbr", "music");
                case 3:
                    return CreateOpusSettings(192, "20", "vbr", "music");
                default:
                    return CreateOpusSettings(128, "40", "vbr", "music");
            }
        }

        private EncoderSettings CreateOpusSettings(int bitrate, string frameSize, string mode, string content)
        {
            EncoderSettings settings = new EncoderSettings
            {
                Frequency = 48000,
                Channels = 2,
                Bitrate = bitrate
            };

            settings.CustomParams["mode"] = mode;
            settings.CustomParams["content"] = content;
            settings.CustomParams["complexity"] = "10";
            settings.CustomParams["framesize"] = frameSize;

            return settings;
        }
        #endregion
    }
}