using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.ComparisonResult;

public interface IComparisonResult
{
    public double SimilarityRate { get; }
    public ResultSource Source { get; }
}