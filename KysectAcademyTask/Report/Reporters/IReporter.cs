using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.Report.Reporters;

internal interface IReporter
{
    public void MakeReport(ComparisonResultsTable results);
}