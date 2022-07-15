namespace KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

internal interface IComparisonAlgorithmImpl
{
    double GetSimilarityRate(string fileContent1, string fileContent2);
}