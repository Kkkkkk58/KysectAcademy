using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public class RatcliffObershelpAlgorithm : IComparisonAlgorithmImpl
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new RatcliffObershelp().Similarity(fileContent1, fileContent2);
    }
}