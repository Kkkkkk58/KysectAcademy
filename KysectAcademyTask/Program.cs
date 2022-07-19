using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.SubmitsComparison;
using KysectAcademyTask.Utils.ProgressTracking;

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
        SubmitComparisonProcessor submitComparisonProcessor = new(config.InputDirectory, config.Filters, config.Metrics);
        submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
        IReporter reporter = new ReporterFactory().GetReporter(config.Report);
        reporter.MakeReport(results);
    }
}