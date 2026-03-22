namespace Auri.Wizard.Recommendation.Strategy
{
    public class DefaultOpusStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 100; // Самый низкий приоритет - fallback

        public override bool CanApply(RecommendationContext context) => true;

        public override QuickStartResult Apply(RecommendationContext context)
        {
            int bitrate = context.Quality == "best" ? 192 :
                          context.Quality == "compact" ? 64 :
                          context.IsUsage("mobile") ? 96 : 128;

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