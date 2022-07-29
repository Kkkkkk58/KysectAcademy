using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Repos;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.DbConfiguration;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Utils.ProgressTracking;
using Microsoft.EntityFrameworkCore;

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
        var submitInfoProcessor =
            new SubmitInfoProcessor(config.SubmitConfig.RootDir, config.SubmitConfig.SubmitTimeFormat);
        var submitSuitabilityChecker = new SubmitSuitabilityChecker(config.SubmitConfig.Filters);
        FileComparisonDbContext context = GetContext(config.DbConfig);
        IComparisonResultRepo resultRepo = new ComparisonResultRepo(context);
        FileProcessor fileProcessor = GetFileProcessor(config.SubmitConfig, resultRepo, context);

        return new SubmitComparisonProcessor(submitGetter, submitInfoProcessor, submitSuitabilityChecker,
            fileProcessor);
    }

    private static FileComparisonDbContext GetContext(DbConfig dbConfig)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FileComparisonDbContext>();
        string connectionString = dbConfig.ConnectionStrings["SubmitComparison"];
        optionsBuilder
            .UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
            .EnableSensitiveDataLogging();
        return new FileComparisonDbContext(optionsBuilder.Options);
    }

    private static IComparisonResultRepo GetComparisonResultRepo(FileComparisonDbContext context)
    {
        return new ComparisonResultRepo(context);
    }

    private static FileProcessor GetFileProcessor(SubmitConfig submitConfig, IComparisonResultRepo resultRepo, FileComparisonDbContext context)
    {
        FileRequirements? fileRequirements = submitConfig.Filters?.FileRequirements;
        DirectoryRequirements? directoryRequirements = submitConfig.Filters?.DirectoryRequirements;
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics = submitConfig.Metrics;

        return new FileProcessor(fileRequirements, directoryRequirements, metrics, resultRepo, context);
    }
}