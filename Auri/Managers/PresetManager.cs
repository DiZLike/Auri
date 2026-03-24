using Auri.Audio.Encoder;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using static Un4seen.Bass.Misc.BaseEncoder;

namespace Auri.Managers
{
    public class PresetManager
    {
        private readonly string _configPath;

        public PresetManager()
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            _configPath = Path.Combine(documentsPath, "Auri", "Presets");
            Directory.CreateDirectory(_configPath);
        }
        public void SaveCustomPreset(string format, EncoderPreset settings)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(settings, Formatting.Indented));
        }
        public EncoderPreset GetCustomPreset(string format)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_custom.json");
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<EncoderPreset>(File.ReadAllText(filePath));
            }
            return new EncoderPreset();
        }
        public EncoderPreset GetQuickStartPreset(string format)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_quickstart.json");
            if (File.Exists(filePath))
            {
                return JsonConvert.DeserializeObject<EncoderPreset>(File.ReadAllText(filePath));
            }
            return null;
        }
        public void SaveQuickStartPreset(string format, EncoderPreset preset)
        {
            string filePath = Path.Combine(_configPath, $"{format.ToLower()}_quickstart.json");
            File.WriteAllText(filePath, JsonConvert.SerializeObject(preset, Formatting.Indented));
        }
        public EncoderPreset GetPreset(string format, int presetIndex)
        {
            string lowerFormat = format.ToLower();

            // Индекс 0xFF всегда означает пользовательский пресет для данного формата
            if (presetIndex == 0xFF)
            {
                EncoderPreset customPreset = GetCustomPreset(lowerFormat);
                if (customPreset != null)
                    return customPreset;
            }

            // Возвращаем стандартный пресет
            EncoderPreset defaultPreset = GetDefaultPreset(lowerFormat, presetIndex);
            return defaultPreset ?? new EncoderPreset();
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
            else if (lowerFormat == "qaac")
                return GetQAacPreset(index);
            return null;
        }
        private EncoderPreset GetMp3Preset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateMp3Presets(32, "cbr", "j");
                case 1:
                    return CreateMp3Presets(64, "cbr", "j");
                case 2:
                    return CreateMp3Presets(128, "cbr", "j");
                case 3:
                    return CreateMp3Presets(192, "cbr", "j");
                case 4:
                    return CreateMp3Presets(256, "cbr", "j");
                case 5:
                    return CreateMp3Presets(320, "cbr", "j");
                default:
                    return CreateMp3Presets(128, "cbr", "j");
            }
        }
        private EncoderPreset GetOpusPreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateOpusPresets(32, 60, "vbr", "music");
                case 1:
                    return CreateOpusPresets(64, 60, "vbr", "music");
                case 2:
                    return CreateOpusPresets(128, 40, "vbr", "music");
                case 3:
                    return CreateOpusPresets(192, 20, "vbr", "music");
                default:
                    return CreateOpusPresets(128, 40, "vbr", "music");
            }
        }
        private EncoderPreset GetFlacPreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateFlacPresets(0);
                case 1:
                    return CreateFlacPresets(5);
                case 2:
                    return CreateFlacPresets(8);
                default:
                    return CreateFlacPresets(5);
            }
        }
        private EncoderPreset GetWavePreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateWavePresets(44100, 16, 1);
                case 1:
                    return CreateWavePresets(44100, 16, 2);
                case 2:
                    return CreateWavePresets(48000, 16, 2);
                case 3:
                    return CreateWavePresets(48000, 24, 2);
                default:
                    return CreateWavePresets(44100, 16, 2);
            }
        }
        private EncoderPreset GetQAacPreset(int index)
        {
            switch (index)
            {
                case 0:
                    return CreateQAacPresets("abr", 32, true);
                case 1:
                    return CreateQAacPresets("abr", 64, true);
                case 2:
                    return CreateQAacPresets("vbr", 46, false);
                case 3:
                    return CreateQAacPresets("vbr", 67, false);
                case 4:
                    return CreateQAacPresets("vbr", 89, false);
                case 5:
                    return CreateQAacPresets("vbr", 127, false);
                default:
                    return CreateQAacPresets("vbr", 67, false);
            }
        }
        private EncoderPreset CreateMp3Presets(int bitrate, string mode, string channelMode)
        {
            EncoderPreset preset = new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
            };
            if (mode == "vbr")
                preset.CustomParams["VbrBitrate"] = bitrate;
            else preset.Bitrate = bitrate;

            preset.CustomParams["Mode"] = mode;
            preset.CustomParams["ChannelMode"] = channelMode;
            preset.CustomParams["Quality"] = 0;

            return preset;
        }
        private EncoderPreset CreateOpusPresets(int bitrate, float frameSize, string mode, string content)
        {
            EncoderPreset preset = new EncoderPreset
            {
                SampleRate = 48000,
                Channels = 2,
                Bitrate = bitrate
            };

            preset.CustomParams["Mode"] = mode;
            preset.CustomParams["Content"] = content;
            preset.CustomParams["Complexity"] = "10";
            preset.CustomParams["Framesize"] = frameSize;

            return preset;
        }
        private EncoderPreset CreateFlacPresets(int compress)
        {
            EncoderPreset preset = new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = 192
            };
            preset.CustomParams["Compress"] = compress;
            preset.CustomParams["UseLossyWav"] = false;
            preset.CustomParams["LossyWavQuality"] = "S";
            return preset;
        }
        private EncoderPreset CreateWavePresets(int frequency, int bits, int channels)
        {
            EncoderPreset preset = new EncoderPreset
            {
                SampleRate = frequency,
                Channels = channels,
                BitsPerSample = bits
            };
            return preset;
        }
        private EncoderPreset CreateQAacPresets(string mode, int vbr, bool he)
        {
            EncoderPreset preset = new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = 128
            };
            preset.CustomParams["VbrBitrate"] = vbr;
            preset.CustomParams["Mode"] = mode;
            preset.CustomParams["He"] = he;
            preset.CustomParams["Quality"] = 2;

            return preset;
        }
    }
}