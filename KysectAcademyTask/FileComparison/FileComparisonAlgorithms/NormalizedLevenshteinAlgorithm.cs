using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public class NormalizedLevenshteinAlgorithm : IComparisonAlgorithmImpl
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new NormalizedLevenshtein().Similarity(fileContent1, fileContent2);
    }
}