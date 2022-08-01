using F23.StringSimilarity;

namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public class SorensenDiceAlgorithm : IComparisonAlgorithmImpl
{
    public double GetSimilarityRate(string fileContent1, string fileContent2)
    {
        return new SorensenDice().Similarity(fileContent1, fileContent2);
    }
}