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
using KysectAcademyTask.Submit;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar;
using Microsoft.EntityFrameworkCore;


namespace KysectAcademyTask;

internal class SubmitComparisonApp
{
    private const string DbPreparingMessage = "\t\t\tPreparing database...";
    private const string DbUpdatingMessage = "\n\n\t\t\tSaving new results to database...";

    public void Run()
    {
        AppSettingsConfig config = AppSettingsParser.GetInstance().Config;
        IReadOnlyList<SubmitInfo> submits = new SubmitGetter(config.SubmitConfig).GetSubmits();
        FileComparisonDbContext dbContext = GetDbContext(config.DbConfig);
        AllRepos allRepos = GetAllRepos(dbContext);
        SubmitInfoProcessor submitInfoProcessor = GetSubmitInfoProcessor(config.SubmitConfig);
        PrepareDatabase(allRepos, submits, submitInfoProcessor);
        SubmitComparisonProcessor submitComparisonProcessor =
            GetSubmitComparisonProcessor(config.SubmitConfig, submits, submitInfoProcessor, dbContext);
        submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
        UpdateDatabase(allRepos.ComparisonResultRepo, allRepos.FileEntityRepo, results);
        IReporter reporter = new ReporterFactory().GetReporter(config.ReportConfig);
        reporter.MakeReport(results);
    }

    private AllRepos GetAllRepos(FileComparisonDbContext dbContext)
    {
        IComparisonResultRepo comparisonResultRepo = new ComparisonResultRepo(dbContext);
        IFileEntityRepo fileEntityRepo = new FileEntityRepo(dbContext);
        IGroupRepo groupRepo = new GroupRepo(dbContext);
        IHomeWorkRepo homeWorkRepo = new HomeWorkRepo(dbContext);
        IStudentRepo studentRepo = new StudentRepo(dbContext);
        ISubmitRepo submitRepo = new SubmitRepo(dbContext);

        return new AllRepos(comparisonResultRepo, fileEntityRepo, groupRepo, homeWorkRepo, studentRepo, submitRepo);
    }

    private static SubmitInfoProcessor GetSubmitInfoProcessor(SubmitConfig config)
    {
        return new SubmitInfoProcessor(config.RootDir, config.SubmitTimeFormat);
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

    private void PrepareDatabase(AllRepos repos, IReadOnlyList<SubmitInfo> submits,
        SubmitInfoProcessor submitInfoProcessor)
    {
        Console.WriteLine(DbPreparingMessage);
        new DbPreparer(repos.GroupRepo, repos.FileEntityRepo, repos.HomeWorkRepo, repos.StudentRepo, repos.SubmitRepo,
            submitInfoProcessor).Prepare(submits);
    }

    private SubmitComparisonProcessor GetSubmitComparisonProcessor(SubmitConfig config,
        IReadOnlyList<SubmitInfo> submits, SubmitInfoProcessor submitInfoProcessor, FileComparisonDbContext context)
    {
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

    private void UpdateDatabase(IComparisonResultRepo resultRepo, IFileEntityRepo fileRepo,
        ComparisonResultsTable results)
    {
        Console.WriteLine(DbUpdatingMessage);
        new DbResultsUpdater(resultRepo, fileRepo).SaveNew(results);
    }
}