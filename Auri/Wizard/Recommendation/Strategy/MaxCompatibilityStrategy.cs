namespace Auri.Wizard.Recommendation.Strategy
{
    public class MaxCompatibilityStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 10;

        public override bool CanApply(RecommendationContext context) => context.IsUsage("max_compat");

        public override QuickStartResult Apply(RecommendationContext context)
        {
            int bitrate = context.Quality switch
            {
                "best" => 320,
                "compact" => 160,
                _ => 192
            };

            return new QuickStartResult
            {
                Format = "mp3",
                FormatDisplayName = "MP3",
                Preset = CreateMp3Preset(bitrate),
                Description = GetMp3Description(bitrate)
            };
        }
    }
}