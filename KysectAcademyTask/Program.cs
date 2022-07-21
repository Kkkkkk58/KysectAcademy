using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.Submit;
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
        SubmitComparisonProcessor submitComparisonProcessor = GetSubmitComparisonProcessor(config);
        submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
        IReporter reporter = new ReporterFactory().GetReporter(config.ReportConfig);
        reporter.MakeReport(results);
    }

    private static SubmitComparisonProcessor GetSubmitComparisonProcessor(AppSettingsConfig config)
    {
        var submitGetter = new SubmitGetter(config.SubmitConfig);
        var submitInfoProcessor = new SubmitInfoProcessor(config.SubmitConfig.RootDir, config.SubmitConfig.SubmitTimeFormat);
        var submitSuitabilityChecker = new SubmitSuitabilityChecker(config.SubmitConfig.Filters);
        FileProcessorBuilder fileProcessorBuilder = new FileProcessorBuilder()
            .BuildDirectoryRequirements(config.SubmitConfig.Filters?.DirectoryRequirements)
            .BuildFileRequirements(config.SubmitConfig.Filters?.FileRequirements)
            .BuildMetrics(config.SubmitConfig.Metrics);

        return new SubmitComparisonProcessor(submitGetter, submitInfoProcessor, submitSuitabilityChecker,
            fileProcessorBuilder);
    }
}