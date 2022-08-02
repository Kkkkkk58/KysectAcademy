using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.Tests.TestModels;

public struct Result
{
    public string FileName1 { get; set; }
    public string FileName2 { get; set; }
    public ComparisonAlgorithm.Metrics Metrics { get; set; }
    public double SimilarityRate { get; set; }
}