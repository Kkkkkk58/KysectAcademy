using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.SubmitsComparison;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        CompareSubmits();
    }

    private static void CompareSubmits()
    {
        AppSettingsConfig config = AppSettingsParser.GetInstance().Config;
        SubmitProcessor submitProcessor = new(config.InputDirectory, config.Filters, config.Metrics);
        ComparisonResultsTable results = submitProcessor.GetComparisonResults();
        IReporter reporter = new ReporterFactory().GetReporter(config.Report);
        reporter.MakeReport(results);
    }
}