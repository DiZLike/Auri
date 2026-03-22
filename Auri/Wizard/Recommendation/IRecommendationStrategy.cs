namespace Auri.Wizard.Recommendation
{
    public interface IRecommendationStrategy
    {
        bool CanApply(RecommendationContext context);
        QuickStartResult Apply(RecommendationContext context);
        int Priority { get; }
    }
}