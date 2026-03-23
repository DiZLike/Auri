namespace Auri.Wizard.Recommendation
{
    public enum StrategyType
    {
        MOBILE, DESKTOP, HOME, APPLE,  MAX_COMPACT
    }
    public enum StrategyQuality
    {
        BEST, BALANCED, COMPACT
    }
    public enum StrategySpecial
    {
        HIGH_COMPRESS, MEDIUM_COMPRESS, NONE
    }
    public interface IRecommendationStrategy
    {
        bool CanApply(RecommendationContext context);
        QuickStartResult Apply(RecommendationContext context);
        int Priority { get; }
    }
}