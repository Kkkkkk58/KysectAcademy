using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

internal class JaccardAlgorithm : IComparisonAlgorithm
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new Jaccard().Similarity(fileContent1, fileContent2);
    }
}