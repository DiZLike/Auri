namespace Auri.Wizard.Recommendation
{
    public class RecommendationContext
    {
        public string Usage { get; set; }      // "mobile", "pc", "home", "apple", "max_compat"
        public string Quality { get; set; }    // "best", "balanced", "compact"
        public string Special { get; set; }    // "high_efficiency", "streaming", "none"

        public RecommendationContext(string usage, string quality, string special)
        {
            Usage = usage;
            Quality = quality;
            Special = special;
        }

        public bool IsUsage(string type) => Usage == type;
        public bool IsQuality(string level) => Quality == level;
        public bool IsSpecial(string need) => Special == need;
    }
}