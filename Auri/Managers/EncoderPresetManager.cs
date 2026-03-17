using Auri.Audio.Encoder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static Un4seen.Bass.Misc.BaseEncoder;

namespace Auri.Managers
{
    public class EncoderPresetManager
    {
        private readonly string _configPath;

        public EncoderPresetManager()
        {
            _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "encoder_presets");
            Directory.CreateDirectory(_configPath);
        }
        public EncoderPreset GetPreset(string format, int presetIndex)
        {
            string lowerFormat = format.ToLower();

            // Индекс 0xFF всегда означает пользовательский пресет для данного формата
            if (presetIndex == 0xFF)
            {
                EncoderPreset customPreset = LoadCustomPreset(lowerFormat);
                if (customPreset != null)
                    return customPreset;
            }

            // Возвращаем стандартный пресет
            EncoderPreset defaultPreset = GetDefaultPreset(lowerFormat, presetIndex);
            return defaultPreset ?? new EncoderPreset();
        }
        public void SaveCustomPreset(string format, EncoderPreset settings)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
        public EncoderPreset LoadCustomPreset(string format)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<EncoderPreset>(File.ReadAllText(filePath));
            }
            return new EncoderPreset();
        }
        private EncoderPreset GetDefaultPreset(string format, int index)
        {
            string lowerFormat = format.ToLower();

            if (lowerFormat == "opus")
                return GetOpusPreset(index);
            else if (lowerFormat == "mp3")
                return GetMp3Preset(index);
            else if (lowerFormat == "flac")
                return GetFlacPreset(index);
            else if (lowerFormat == "wav")
                return GetWavePreset(index);
            return null;
        }
        private EncoderPreset GetMp3Preset(int index)
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
        private EncoderPreset GetOpusPreset(int index)
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
        private EncoderPreset GetFlacPreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateFlacSettings(0);
                case 1:
                    return CreateFlacSettings(5);
                case 2:
                    return CreateFlacSettings(8);
                default:
                    return CreateFlacSettings(5);
            }
        }
        private EncoderPreset GetWavePreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateWaveSettings(44100, 16, 2);
                default:
                    return CreateWaveSettings(44100, 16, 2);
            }
        }
        private EncoderPreset CreateMp3Settings(int bitrate, string mode, string channelMode)
        {
            EncoderPreset settings = new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = bitrate
            };
            settings.CustomParams["Mode"] = mode;
            settings.CustomParams["ChannelMode"] = channelMode;
            settings.CustomParams["Quality"] = 0;

            return settings;
        }
        private EncoderPreset CreateOpusSettings(int bitrate, float frameSize, string mode, string content)
        {
            EncoderPreset settings = new EncoderPreset
            {
                SampleRate = 48000,
                Channels = 2,
                Bitrate = bitrate
            };

            settings.CustomParams["Mode"] = mode;
            settings.CustomParams["Content"] = content;
            settings.CustomParams["Complexity"] = "10";
            settings.CustomParams["Framesize"] = frameSize;

            return settings;
        }
        private EncoderPreset CreateFlacSettings(int compress)
        {
            EncoderPreset settings = new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = 192
            };
            settings.CustomParams["Compress"] = compress;
            settings.CustomParams["UseLossyWav"] = false;
            settings.CustomParams["LossyWavQuality"] = "S";
            return settings;
        }
        private EncoderPreset CreateWaveSettings(int frequency, int bits, int channels)
        {
            EncoderPreset settings = new EncoderPreset
            {
                SampleRate = frequency,
                Channels = channels,
                BitsPerSample = bits
            };
            return settings;
        }
    }
}