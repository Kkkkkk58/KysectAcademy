namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

internal class ComparisonAlgorithm
{
    public enum Metrics
    {
        NormalizedLevenshtein = 1,
        JaroWinkler,
        Cosine,
        Jaccard,
        SorensenDice,
        RatcliffObershelp
    }

    public IComparisonAlgorithmImpl SetImplementation(Metrics metrics)
    {
        return metrics switch
        {
            Metrics.NormalizedLevenshtein => new NormalizedLevenshteinAlgorithm(),
            Metrics.JaroWinkler => new JaroWinklerAlgorithm(),
            Metrics.Cosine => new CosineAlgorithm(),
            Metrics.Jaccard => new JaccardAlgorithm(),
            Metrics.SorensenDice => new SorensenDiceAlgorithm(),
            Metrics.RatcliffObershelp => new RatcliffObershelpAlgorithm(),
            _ => throw new NotImplementedException(metrics.ToString())
        };
    }
}