namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

public interface IComparisonAlgorithm
{
    double GetSimilarityRate(string fileContent1, string fileContent2);
}