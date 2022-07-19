using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.SubmitComparison;
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
        SubmitComparisonProcessor submitComparisonProcessor = new(config.SubmitConfig);
        submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
        IReporter reporter = new ReporterFactory().GetReporter(config.ReportConfig);
        reporter.MakeReport(results);
    }
}