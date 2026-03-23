using Auri.Audio.Encoder;
using System.Collections.Generic;
using System.Globalization;

namespace Auri.Wizard.Recommendation
{
    public abstract class BaseRecommendationStrategy : IRecommendationStrategy
    {
        public abstract bool CanApply(RecommendationContext context);
        public abstract QuickStartResult Apply(RecommendationContext context);
        public abstract int Priority { get; }

        protected EncoderPreset CreateFlacPreset(int compress)
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                BitsPerSample = 16,
                Bitrate = 0,
                Format = "FLAC",
                CustomParams = new Dictionary<string, object>
                {
                    ["Compress"] = compress,
                    ["UseLossyWav"] = false,
                    ["LossyWavQuality"] = "S"
                }
            };
        }

        protected EncoderPreset CreateMp3Preset(int vbr, string avgBitrate)
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Format = "MP3",
                CustomParams = new Dictionary<string, object>
                {
                    ["AvgBitrate"] =  ParseBitrateRange(avgBitrate),
                    ["VbrBitrate"] = vbr,
                    ["Mode"] = "vbr",
                    ["ChannelMode"] = "j",
                    ["Quality"] = 0
                }
            };
        }

        protected EncoderPreset CreateAacPreset(int vbr, string avgBitrate)
        {
            return new EncoderPreset
            {
                SampleRate = 44100,
                Channels = 2,
                Format = "QAAC",
                CustomParams = new Dictionary<string, object>
                {
                    ["AvgBitrate"] = ParseBitrateRange(avgBitrate),
                    ["VbrBitrate"] = vbr,
                    ["Mode"] = "vbr",
                    ["He"] = false,
                    ["Quality"] = 2
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
                Format = "Opus",
                CustomParams = new Dictionary<string, object>
                {
                    ["Content"] = "music",
                    ["Complexity"] = 10,
                    ["Mode"] = "vbr",
                    ["Framesize"] = 60
                }
            };
        }

        protected string GetMp3Description(string bitrateRange, int vbr)
        {
            int averageBitrate = ParseBitrateRange(bitrateRange);

            if (averageBitrate >= 245)
                return $"MP3 VBR {vbr} (~260 kbps) — максимальное качество для этого формата. Оптимальное распределение битрейта на сложных участках.";

            if (averageBitrate >= 225)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — прозрачное звучание, неотличимое от оригинала. Отличный выбор для музыкальных коллекций.";

            if (averageBitrate >= 190)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — высокое качество звучания. Переменный битрейт эффективно распределяет биты между простыми и сложными участками.";

            if (averageBitrate >= 170)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — отличный баланс качества и размера. Подходит для большинства музыкальных жанров.";

            if (averageBitrate >= 150)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — хорошее качество при умеренном размере файла. Эффективно для фоновой музыки и подкастов.";

            if (averageBitrate >= 130)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — сбалансированный вариант для речи и несложной музыки. Обеспечивает приемлемое качество при компактном размере.";

            if (averageBitrate >= 115)
                return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — экономичный режим. Хорошо подходит для подкастов, аудиокниг и фонового воспроизведения.";

            return $"MP3 VBR {vbr} (~{averageBitrate} kbps) — базовое качество для максимальной экономии места.";
        }

        protected string GetAacDescription(string bitrateRange)
        {
            int bitrate = ParseBitrateRange(bitrateRange);
            if (bitrate >= 320)
                return $"AAC {bitrateRange} kbps — максимальное качество. Идеально для профессионального прослушивания и аудиофильских систем.";
            if (bitrate >= 256)
                return $"AAC {bitrateRange} kbps — превосходное качество. Оптимален для большинства сценариев использования.";
            if (bitrate >= 192)
                return $"AAC {bitrateRange} kbps — отличный баланс качества и размера. Высокая детализация для современных устройств.";
            if (bitrate >= 128)
                return $"AAC {bitrateRange} kbps — универсальный стандарт. Хорошее качество при оптимальном размере файла.";
            if (bitrate >= 96)
                return $"AAC {bitrateRange} kbps — экономичный формат. Хорошее качество при компактном размере.";
            return $"AAC {bitrateRange} kbps — базовый уровень качества. Подходит для фонового прослушивания и подкастов.";
        }

        protected string GetOpusDescription(int bitrate)
        {
            if (bitrate >= 192)
                return $"Opus {bitrate} kbps — современный формат с максимальным качеством.";
            if (bitrate >= 128)
                return $"Opus {bitrate} kbps — оптимальное соотношение качества и размера. Универсальный современный формат.";
            if (bitrate >= 96)
                return $"Opus {bitrate} kbps — идеален для мобильных устройств. Отличное качество при экономии места.";
            if (bitrate >= 64)
                return $"Opus {bitrate} kbps — невероятно эффективный формат. Отличное качество при минимальном размере файла.";
            return $"Opus {bitrate} kbps — максимальная экономия места. Идеально для подкастов и речи, музыка звучит достойно.";
        }
        protected string GetFlacDescription(int compressionLevel)
        {
            if (compressionLevel == 8)
                return "FLAC — максимальное сжатие без потерь. Идеальное качество студийного уровня при минимальном для lossless формата размере файла.";
            if (compressionLevel >= 1)
                return "FLAC — сбалансированное сжатие без потерь. Мастер-копия вашей музыки с оригинальным качеством.";

            return "FLAC (без сжатия/быстрое) — максимальная скорость обработки. Полное сохранение оригинального качества аудиодорожки.";
        }
        protected int ParseBitrateRange(string bitrateRange)
        {
            if (string.IsNullOrEmpty(bitrateRange))
                return 0;

            // Удаляем символ "~" и пробелы
            string cleaned = bitrateRange.Replace("~", "").Trim();

            // Разделяем по тире или длинному тире
            string[] parts = cleaned.Split(new[] { '-', '–' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length == 2)
            {
                // Парсим минимальный и максимальный битрейт, возвращаем среднее
                if (int.TryParse(parts[0], out int min) && int.TryParse(parts[1], out int max))
                {
                    return (min + max) / 2;
                }
            }

            // Если не удалось распарсить, пробуем распарсить как одно число
            if (int.TryParse(cleaned, out int singleValue))
                return singleValue;

            return 0;
        }
    }
}