namespace Auri.Wizard.Recommendation.Strategy
{
    public class HomeAudioStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 10;

        public override bool CanApply(RecommendationContext context) =>
            context.IsUsage(StrategyType.HOME);

        public override QuickStartResult Apply(RecommendationContext context)
        {
            if (context.Quality == StrategyQuality.BEST
                && context.Special == StrategySpecial.NONE)
                return new QuickStartResult
                {
                    Format = "flac",
                    FormatDisplayName = "FLAC",
                    Preset = CreateFlacPreset(8),
                    Description = GetFlacDescription(8)
                };

            (int tvbrValue, string tvbrDescription) = (context.Quality, context.Special) switch
            {
                (StrategyQuality.BALANCED, StrategySpecial.NONE) => (118, "~285–320"),
                (StrategyQuality.COMPACT, StrategySpecial.NONE) => (109, "~225–245"),

                (StrategyQuality.BEST, StrategySpecial.MEDIUM_COMPRESS) => (109, "~225–245"),
                (StrategyQuality.BALANCED, StrategySpecial.MEDIUM_COMPRESS) => (100, "~195–215"),
                (StrategyQuality.COMPACT, StrategySpecial.MEDIUM_COMPRESS) => (91, "~165–185"),

                (StrategyQuality.BEST, StrategySpecial.HIGH_COMPRESS) => (100, "~195–215"),
                (StrategyQuality.BALANCED, StrategySpecial.HIGH_COMPRESS) => (91, "~165–185"),
                (StrategyQuality.COMPACT, StrategySpecial.HIGH_COMPRESS) => (82, "~135–155"),

                // значение по умолчанию — ~165–185 kbps (QAAC default)
                _ => (91, "~165–185")
            };

            return new QuickStartResult
            {
                Format = "qaac",
                FormatDisplayName = "M4A",
                Preset = CreateAacPreset(tvbrValue, tvbrDescription),
                Description = GetAacDescription(tvbrDescription)
            };
        }
    }
}