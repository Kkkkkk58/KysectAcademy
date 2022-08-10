namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public interface IComparisonAlgorithmImpl
{
    double GetSimilarityRate(string fileContent1, string fileContent2);
}