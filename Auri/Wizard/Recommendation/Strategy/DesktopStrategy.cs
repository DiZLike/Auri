using Auri.Audio.Encoder;

namespace Auri.Wizard.Recommendation.Strategy
{
    public class DesktopStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 20;

        public override bool CanApply(RecommendationContext context) =>
            context.IsUsage(StrategyType.DESKTOP);

        public override QuickStartResult Apply(RecommendationContext context)
        {
            // FLAC для лучшего качества без компрессии
            if (context.Quality == StrategyQuality.BEST && context.Special == StrategySpecial.NONE)
            {
                return new QuickStartResult
                {
                    Format = "flac",
                    FormatDisplayName = "FLAC",
                    Preset = CreateFlacPreset(8),
                    Description = GetFlacDescription(8)
                };
            }

            // Определяем формат на основе битрейта
            (string format, string displayName, int bitrate, string description) = (context.Quality, context.Special) switch
            {
                // Высокие битрейты — AAC TVBR
                (StrategyQuality.BALANCED, StrategySpecial.NONE) => ("qaac", "M4A", 109, "~225–245"),
                (StrategyQuality.COMPACT, StrategySpecial.NONE) => ("qaac", "M4A", 100, "~195–215"),

                // Средняя компрессия — AAC TVBR на верхней границе, Opus на нижней
                (StrategyQuality.BEST, StrategySpecial.MEDIUM_COMPRESS) => ("qaac", "M4A", 100, "~195–215"),
                (StrategyQuality.BALANCED, StrategySpecial.MEDIUM_COMPRESS) => ("opus", "Opus", 128, "128"),
                (StrategyQuality.COMPACT, StrategySpecial.MEDIUM_COMPRESS) => ("opus", "Opus", 96, "96"),

                // Высокая компрессия — Opus
                (StrategyQuality.BEST, StrategySpecial.HIGH_COMPRESS) => ("opus", "Opus", 96, "96"),
                (StrategyQuality.BALANCED, StrategySpecial.HIGH_COMPRESS) => ("opus", "Opus", 64, "64"),
                (StrategyQuality.COMPACT, StrategySpecial.HIGH_COMPRESS) => ("opus", "Opus", 48, "48"),

                // значение по умолчанию
                _ => ("qaac", "M4A", 128, "~128 kbps (very good quality)")
            };

            // Создаём пресет в зависимости от формата
            EncoderPreset preset = format switch
            {
                "qaac" => CreateAacPreset(bitrate, description),
                "opus" => CreateOpusPreset(bitrate),
                _ => CreateAacPreset(bitrate, description)
            };

            string finalDescription = format == "qaac"
                ? GetAacDescription(description)
                : GetOpusDescription(bitrate);

            return new QuickStartResult
            {
                Format = format,
                FormatDisplayName = displayName,
                Preset = preset,
                Description = finalDescription
            };
        }
    }
}