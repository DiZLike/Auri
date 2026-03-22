using Auri.Audio.Encoder;
using System.Collections.Generic;

namespace Auri.Wizard.Recommendation
{
    public abstract class BaseRecommendationStrategy : IRecommendationStrategy
    {
        public abstract bool CanApply(RecommendationContext context);
        public abstract QuickStartResult Apply(RecommendationContext context);
        public abstract int Priority { get; }

        protected EncoderPreset CreateFlacPreset()
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                BitsPerSample = 16,
                Bitrate = 0,
                CustomParams = new Dictionary<string, object>
                {
                    ["CompressionLevel"] = 5
                }
            };
        }

        protected EncoderPreset CreateMp3Preset(int bitrate)
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = bitrate,
                CustomParams = new Dictionary<string, object>
                {
                    ["Mode"] = "cbr",
                    ["ChannelMode"] = "j"
                }
            };
        }

        protected EncoderPreset CreateAacPreset(int bitrate)
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Bitrate = bitrate,
                CustomParams = new Dictionary<string, object>
                {
                    ["Mode"] = "vbr",
                    ["Quality"] = bitrate >= 192 ? 2 : 1
                }
            };
        }

        protected EncoderPreset CreateOpusPreset(int bitrate)
        {
            return new EncoderPreset
            {
                SampleRate = 48000,
                Channels = 2,
                Bitrate = bitrate,
                CustomParams = new Dictionary<string, object>
                {
                    ["Application"] = "audio",
                    ["Complexity"] = 10
                }
            };
        }

        protected string GetMp3Description(int bitrate)
        {
            if (bitrate == 320)
                return "MP3 320 kbps — максимальное качество для этого формата. Работает на любых устройствах.";
            if (bitrate == 128)
                return "MP3 128 kbps — хороший баланс качества и размера. Универсальный выбор для всех устройств.";
            return $"MP3 {bitrate} kbps — отличное качество при приемлемом размере файла.";
        }

        protected string GetAacDescription(int bitrate)
        {
            if (bitrate == 256)
                return "AAC 256 kbps VBR — превосходное качество для устройств Apple. Оптимален для iPhone, iPad и AirPods.";
            if (bitrate == 96)
                return "AAC 96 kbps VBR — экономичный формат для мобильных устройств Apple. Хорошее качество при компактном размере.";
            return $"AAC {bitrate} kbps VBR — сбалансированный выбор для повседневного использования.";
        }

        protected string GetOpusDescription(int bitrate)
        {
            if (bitrate >= 192)
                return "Opus 192 kbps — современный формат с максимальным качеством. Лучший выбор для аудиофилов.";
            if (bitrate >= 128)
                return "Opus 128 kbps — оптимальное соотношение качества и размера. Универсальный современный формат.";
            if (bitrate >= 96)
                return "Opus 96 kbps — идеален для мобильных устройств. Отличное качество при экономии места.";
            if (bitrate >= 64)
                return "Opus 64 kbps — невероятно эффективный формат. Отличное качество при минимальном размере файла.";
            return "Opus 48 kbps — максимальная экономия места. Идеально для подкастов и речи, музыка звучит достойно.";
        }
    }
}