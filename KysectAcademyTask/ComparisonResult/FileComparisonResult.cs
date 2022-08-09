using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.ComparisonResult;

public readonly struct FileComparisonResult : IComparisonResult
{
    public string FileName1 { get; }
    public string FileName2 { get; }
    public ComparisonAlgorithm.Metrics Metrics { get; }
    public double SimilarityRate { get; }
    public ResultSource Source { get; }

    public FileComparisonResult(string fileName1, string fileName2, ComparisonAlgorithm.Metrics metrics,
        double similarityRate, ResultSource source = ResultSource.NewFileComparison)
    {
        FileName1 = fileName1;
        FileName2 = fileName2;
        Metrics = metrics;
        SimilarityRate = similarityRate;
        Source = source;
    }

    public override string ToString()
    {
        return
            $"|{FileName1}\n|{FileName2}\n|{{using {Metrics} metrics}}\n* Received from {Source}\n=> {SimilarityRate:0.##}";
    }
}