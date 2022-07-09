using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

internal class CosineAlgorithm : IComparisonAlgorithm
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new Cosine().Similarity(fileContent1, fileContent2);
    }
}