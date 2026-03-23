namespace Auri.Wizard.Recommendation
{
    public class RecommendationContext
    {
        public StrategyType Usage { get; set; }      // "mobile", "pc", "home", "apple", "max_compat"
        public StrategyQuality Quality { get; set; }    // "best", "balanced", "compact"
        public StrategySpecial Special { get; set; }    // "high_efficiency", "streaming", "none"

        public RecommendationContext(StrategyType usage, StrategyQuality quality, StrategySpecial special)
        {
            Usage = usage;
            Quality = quality;
            Special = special;
        }

        public bool IsUsage(StrategyType type) => Usage == type;
        public bool IsQuality(StrategyQuality level) => Quality == level;
        public bool IsSpecial(StrategySpecial need) => Special == need;
    }
}