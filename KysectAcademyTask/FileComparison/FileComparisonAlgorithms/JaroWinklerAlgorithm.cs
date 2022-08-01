using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public class JaroWinklerAlgorithm : IComparisonAlgorithmImpl
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new JaroWinkler().Similarity(fileContent1, fileContent2);
    }
}