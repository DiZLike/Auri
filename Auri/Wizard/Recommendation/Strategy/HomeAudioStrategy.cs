namespace Auri.Wizard.Recommendation.Strategy
{
    public class HomeAudioStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 30;

        public override bool CanApply(RecommendationContext context) =>
            context.IsUsage("home");

        public override QuickStartResult Apply(RecommendationContext context)
        {
            int bitrate = (context.Quality, context.Special) switch
            {
                ("best", "high_efficiency") => 192,
                ("balanced", "high_efficiency") => 128,
                ("compact", "high_efficiency") => 96,
                ("best", _) => 320,
                ("balanced", _) => 192,
                ("compact", _) => 160,
                _ => 0
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