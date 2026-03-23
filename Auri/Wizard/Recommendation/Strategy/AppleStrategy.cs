namespace Auri.Wizard.Recommendation.Strategy
{
    public class AppleStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 28;

        public override bool CanApply(RecommendationContext context) => context.IsUsage(StrategyType.APPLE);

        public override QuickStartResult Apply(RecommendationContext context)
        {
            (int tvbrValue, string tvbrDescription) = (context.Quality, context.Special) switch
            {
                (StrategyQuality.BEST, StrategySpecial.NONE) => (118, "~285–320"),
                (StrategyQuality.BALANCED, StrategySpecial.NONE) => (100, "~195–215"),
                (StrategyQuality.COMPACT, StrategySpecial.NONE) => (91, "~165–185"),

                (StrategyQuality.BEST, StrategySpecial.MEDIUM_COMPRESS) => (100, "~195–215"),
                (StrategyQuality.BALANCED, StrategySpecial.MEDIUM_COMPRESS) => (91, "~165–185"),
                (StrategyQuality.COMPACT, StrategySpecial.MEDIUM_COMPRESS) => (82, "~135–155"),

                (StrategyQuality.BEST, StrategySpecial.HIGH_COMPRESS) => (91, "~165–185"),
                (StrategyQuality.BALANCED, StrategySpecial.HIGH_COMPRESS) => (82, "~135–155"),
                (StrategyQuality.COMPACT, StrategySpecial.HIGH_COMPRESS) => (73, "~115–135"),

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