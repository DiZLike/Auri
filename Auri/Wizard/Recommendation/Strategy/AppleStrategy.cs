namespace Auri.Wizard.Recommendation.Strategy
{
    public class AppleStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 20;

        public override bool CanApply(RecommendationContext context) => context.IsUsage("apple");

        public override QuickStartResult Apply(RecommendationContext context)
        {
            int bitrate = context.Quality switch
            {
                "best" => 256,
                "compact" => 128,
                _ => 192
            };

            return new QuickStartResult
            {
                Format = "aac",
                FormatDisplayName = "AAC",
                Preset = CreateAacPreset(bitrate),
                Description = GetAacDescription(bitrate)
            };
        }
    }
}