namespace Auri.Wizard.Recommendation.Strategy
{
    public class MaxCompatibilityStrategy : BaseRecommendationStrategy
    {
        public override int Priority => 30;

        public override bool CanApply(RecommendationContext context) => context.IsUsage(StrategyType.MAX_COMPACT);

        public override QuickStartResult Apply(RecommendationContext context)
        {
            (int tvbrValue, string tvbrDescription) = (context.Quality, context.Special) switch
            {
                // Базовые настройки без компрессии
                (StrategyQuality.BEST, StrategySpecial.NONE) => (0, "~245–260"),                           // BEST / NONE — VBR 0 (~245–260 kbps, максимальное качество)
                (StrategyQuality.BALANCED, StrategySpecial.NONE) => (1, "~225–245"),                       // BALANCED / NONE — VBR 1 (~225–245 kbps, прозрачность)
                (StrategyQuality.COMPACT, StrategySpecial.NONE) => (2, "~190–215"),                        // COMPACT / NONE — VBR 2 (~190–215 kbps, высокое качество)

                // Средняя компрессия
                (StrategyQuality.BEST, StrategySpecial.MEDIUM_COMPRESS) => (2, "~190–215"),                // BEST / MEDIUM_COMPRESS — VBR 2 (~190–215 kbps, отличное качество)
                (StrategyQuality.BALANCED, StrategySpecial.MEDIUM_COMPRESS) => (3, "~170–195"),            // BALANCED / MEDIUM_COMPRESS — VBR 3 (~170–195 kbps, хороший баланс)
                (StrategyQuality.COMPACT, StrategySpecial.MEDIUM_COMPRESS) => (4, "~150–175"),             // COMPACT / MEDIUM_COMPRESS — VBR 4 (~150–175 kbps, эффективный размер)

                // Высокая компрессия
                (StrategyQuality.BEST, StrategySpecial.HIGH_COMPRESS) => (4, "~150–175"),                  // BEST / HIGH_COMPRESS — VBR 4 (~150–175 kbps, качество выше среднего)
                (StrategyQuality.BALANCED, StrategySpecial.HIGH_COMPRESS) => (5, "~130–155"),              // BALANCED / HIGH_COMPRESS — VBR 5 (~130–155 kbps, для речи и фоновой музыки)
                (StrategyQuality.COMPACT, StrategySpecial.HIGH_COMPRESS) => (6, "~115–135"),               // COMPACT / HIGH_COMPRESS — VBR 6 (~115–135 kbps, экономия места)

                // значение по умолчанию — VBR 4 (~150–175 kbps, сбалансированный вариант)
                _ => (4, "~150–175")
            };

            return new QuickStartResult
            {
                Format = "mp3",
                FormatDisplayName = "MP3",
                Preset = CreateMp3Preset(tvbrValue, tvbrDescription),
                Description = GetMp3Description(tvbrDescription, tvbrValue)
            };
        }
    }
}