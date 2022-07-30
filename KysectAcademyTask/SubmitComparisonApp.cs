using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Repos;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.DbInteraction;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils.ProgressTracking;
using KysectAcademyTask.Submit;
using KysectAcademyTask.SubmitComparison;
using Microsoft.EntityFrameworkCore;


namespace KysectAcademyTask;

internal class SubmitComparisonApp
{
    public void Run()
    {
        AppSettingsConfig config = AppSettingsParser.GetInstance().Config;
        IReadOnlyList<SubmitInfo> submits = new SubmitGetter(config.SubmitConfig).GetSubmits();
        FileComparisonDbContext dbContext = GetDbContext(config.DbConfig);
        PrepareDatabase(dbContext, submits);
        SubmitComparisonProcessor submitComparisonProcessor = GetSubmitComparisonProcessor(config.SubmitConfig, submits, dbContext);
        submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
        IReporter reporter = new ReporterFactory().GetReporter(config.ReportConfig);
        reporter.MakeReport(results);
    }

    private FileComparisonDbContext GetDbContext(DbConfig dbConfig)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FileComparisonDbContext>();
        string connectionString = dbConfig.ConnectionStrings["SubmitComparison"];
        optionsBuilder
            .UseSqlServer(connectionString, options => options.EnableRetryOnFailure())
            .EnableSensitiveDataLogging();

        return new FileComparisonDbContext(optionsBuilder.Options);
    }

    private void PrepareDatabase(FileComparisonDbContext context, IReadOnlyList<SubmitInfo> submits)
    {
        IGroupRepo groupRepo = new GroupRepo(context);
        IHomeWorkRepo homeWorkRepo = new HomeWorkRepo(context);
        IStudentRepo studentRepo = new StudentRepo(context);
        ISubmitRepo submitRepo = new SubmitRepo(context);
        new DbPreparer(groupRepo, homeWorkRepo, studentRepo, submitRepo).Prepare(submits);
    }

    private SubmitComparisonProcessor GetSubmitComparisonProcessor(SubmitConfig config, IReadOnlyList<SubmitInfo> submits, FileComparisonDbContext context)
    {
        var submitInfoProcessor =
            new SubmitInfoProcessor(config.RootDir, config.SubmitTimeFormat);
        var submitSuitabilityChecker = new SubmitSuitabilityChecker(config.Filters);
        IComparisonResultRepo resultRepo = new ComparisonResultRepo(context);
        FileProcessor fileProcessor = GetFileProcessor(config, resultRepo);

        return new SubmitComparisonProcessor(submits, submitInfoProcessor, submitSuitabilityChecker,
            fileProcessor);
    }

    private FileProcessor GetFileProcessor(SubmitConfig config, IComparisonResultRepo resultRepo)
    {
        FileRequirements? fileRequirements = config.Filters?.FileRequirements;
        DirectoryRequirements? directoryRequirements = config.Filters?.DirectoryRequirements;
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics = config.Metrics;

        return new FileProcessor(fileRequirements, directoryRequirements, metrics, resultRepo);
    }
}