using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public class CosineAlgorithm : IComparisonAlgorithmImpl
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new Cosine().Similarity(fileContent1, fileContent2);
    }
}