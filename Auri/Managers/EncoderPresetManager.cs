using Auri.Audio.Encoder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static Un4seen.Bass.Misc.BaseEncoder;

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
        /// Получить стандартный пресет для формата и индекса
        /// </summary>
        private EncoderSettings GetDefaultPreset(string format, int index)
        {
            string lowerFormat = format.ToLower();

            if (lowerFormat == "opus")
                return GetOpusPreset(index);
            else if (lowerFormat == "mp3")
                return GetMp3Preset(index);
            return null;
        }

        #region Opus presets
        private EncoderSettings GetOpusPreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateOpusSettings(32, 60, "vbr", "music");
                case 1:
                    return CreateOpusSettings(64, 60, "vbr", "music");
                case 2:
                    return CreateOpusSettings(128, 40, "vbr", "music");
                case 3:
                    return CreateOpusSettings(192, 20, "vbr", "music");
                default:
                    return CreateOpusSettings(128, 40, "vbr", "music");
            }
        }

        private EncoderSettings CreateOpusSettings(int bitrate, float frameSize, string mode, string content)
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

        #region Mp3 presets
        private EncoderSettings GetMp3Preset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateMp3Settings(32, "cbr", "j");
                case 1:
                    return CreateMp3Settings(64, "cbr", "j");
                case 2:
                    return CreateMp3Settings(128, "cbr", "j");
                case 3:
                    return CreateMp3Settings(192, "cbr", "j");
                case 4:
                    return CreateMp3Settings(256, "cbr", "j");
                case 5:
                    return CreateMp3Settings(320, "cbr", "j");
                default:
                    return CreateMp3Settings(128, "cbr", "j");
            }
        }
        private EncoderSettings CreateMp3Settings(int bitrate, string mode, string channelMode)
        {
            EncoderSettings settings = new EncoderSettings
            {
                Frequency = 44100,
                Channels = 2,
                Bitrate = bitrate
            };
            settings.CustomParams["mode"] = mode;
            settings.CustomParams["channelMode"] = channelMode;
            settings.CustomParams["quality"] = 0;

            return settings;
        }

        #endregion
    }
}