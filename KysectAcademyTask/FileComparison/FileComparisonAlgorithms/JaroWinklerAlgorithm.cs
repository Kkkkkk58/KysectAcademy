using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

internal class JaroWinklerAlgorithm : IComparisonAlgorithm
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new JaroWinkler().Similarity(fileContent1, fileContent2);
    }
}