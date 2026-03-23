namespace Auri.Wizard.Recommendation.Strategy
{
    public class MobileStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 25;

        public override bool CanApply(RecommendationContext context) =>
            context.IsUsage(StrategyType.MOBILE);

        public override QuickStartResult Apply(RecommendationContext context)
        {
            int bitrate = (context.Quality, context.Special) switch
            {
                (StrategyQuality.BEST, StrategySpecial.NONE) => 192,
                (StrategyQuality.BALANCED, StrategySpecial.NONE) => 128,
                (StrategyQuality.COMPACT, StrategySpecial.NONE) => 96,

                (StrategyQuality.BEST, StrategySpecial.MEDIUM_COMPRESS) => 128,
                (StrategyQuality.BALANCED, StrategySpecial.MEDIUM_COMPRESS) => 96,
                (StrategyQuality.COMPACT, StrategySpecial.MEDIUM_COMPRESS) => 64,

                (StrategyQuality.BEST, StrategySpecial.HIGH_COMPRESS) => 64,
                (StrategyQuality.BALANCED, StrategySpecial.HIGH_COMPRESS) => 48,
                (StrategyQuality.COMPACT, StrategySpecial.HIGH_COMPRESS) => 32,
                _ => 96,
            };

            return new QuickStartResult
            {
                Format = "opus",
                FormatDisplayName = "Opus",
                Preset = CreateOpusPreset(bitrate),
                Description = GetOpusDescription(bitrate)
            };
        }
    }
}