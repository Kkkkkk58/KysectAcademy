using KysectAcademyTask.ComparisonResult;

namespace KysectAcademyTask.Report.Reporters;

public interface IReporter<T> where T : IComparisonResult
{
    public void MakeReport(ComparisonResultsTable<T> results);
}