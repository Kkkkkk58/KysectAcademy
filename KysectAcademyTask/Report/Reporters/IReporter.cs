using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.Report.Reporters;

public interface IReporter
{
    public void MakeReport(ComparisonResultsTable results);
}