using KysectAcademyTask.AppSettings;
using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DataAccess.Repos;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.DbInteraction;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;
using KysectAcademyTask.Report.Reporters;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Submit;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar;

namespace KysectAcademyTask;

public class SubmitComparisonApp
{
    private readonly AppSettingsConfig _config;

    public SubmitComparisonApp(AppSettingsConfig config)
    {
        _config = config;
    }

    public void Run()
    {
        try
        {
            IReadOnlyList<SubmitInfo> submits = new SubmitGetter(_config.SubmitConfig).GetSubmits();
            SubmitComparisonDbContext dbContext = GetDbContext(_config.DbConfig);
            AllRepos allRepos = GetAllRepos(dbContext);
            
            if (dbContext is not null)
            {
                PrepareDatabase(allRepos, submits);
            }

            DbResultsCacheManager cacheManager =
                GetCacheManager(allRepos.ComparisonResultRepo, _config.DbConfig.Recheck);
            SubmitComparisonProcessor submitComparisonProcessor =
                GetSubmitComparisonProcessor(_config.SubmitConfig, submits, cacheManager);
            SetProgressBar(submitComparisonProcessor, _config.ProgressBarConfig);
            ComparisonResultsTable<SubmitComparisonResult> results = submitComparisonProcessor.GetComparisonResults();
            IReporter<SubmitComparisonResult> reporter = GetReporter(_config.ReportConfig);
            reporter.MakeReport(results);

            if (dbContext is not null)
            {
                SaveResultsToDatabase(allRepos.ComparisonResultRepo, allRepos.SubmitRepo, results);
            }
        }
        catch (Exception e)
        {
            throw new ApplicationException($"An error occurred during the app run: {e.Message}", e);
        }
    }

    private IReporter<SubmitComparisonResult> GetReporter(ReportConfig reportConfig)
    {
        return new ReporterFactory().GetReporter<SubmitComparisonResult>(reportConfig);
    }

    private DbResultsCacheManager GetCacheManager(IComparisonResultRepo comparisonResultRepo, bool dbConfigRecheck)
    {
        return new DbResultsCacheManager(comparisonResultRepo, dbConfigRecheck);
    }


    private AllRepos GetAllRepos(SubmitComparisonDbContext dbContext)
    {
        IComparisonResultRepo comparisonResultRepo = new ComparisonResultRepo(dbContext);
        IGroupRepo groupRepo = new GroupRepo(dbContext);
        IHomeWorkRepo homeWorkRepo = new HomeWorkRepo(dbContext);
        IStudentRepo studentRepo = new StudentRepo(dbContext);
        ISubmitRepo submitRepo = new SubmitRepo(dbContext);

        return new AllRepos(comparisonResultRepo, groupRepo, homeWorkRepo, studentRepo, submitRepo);
    }

    private static SubmitInfoProcessor GetSubmitInfoProcessor(SubmitConfig config)
    {
        return new SubmitInfoProcessor(config.RootDir, config.SubmitTimeFormat);
    }

    private SubmitComparisonDbContext GetDbContext(DbConfig dbConfig)
    {
        string connectionString = dbConfig.ConnectionStrings?["SubmitComparison"];
        if (connectionString is null or "" || dbConfig.DataProvider is null)
        {
            return null;
        }

        return new SubmitComparisonDbContextFactory()
            .GetDbContext((DataProvider)dbConfig.DataProvider, connectionString);
    }

    private void PrepareDatabase(AllRepos repos, IReadOnlyList<SubmitInfo> submits)
    {
        new DbPreparer(repos.GroupRepo, repos.HomeWorkRepo, repos.StudentRepo, repos.SubmitRepo)
            .Prepare(submits);
    }

    private SubmitComparisonProcessor GetSubmitComparisonProcessor(SubmitConfig config,
        IReadOnlyList<SubmitInfo> submits, DbResultsCacheManager dbResultsCacheManager)
    {
        SubmitInfoProcessor submitInfoProcessor = GetSubmitInfoProcessor(config);
        var submitSuitabilityChecker = new SubmitSuitabilityChecker(config.Filters, submitInfoProcessor, dbResultsCacheManager);
        FileProcessor fileProcessor = GetFileProcessor(config);

        return new SubmitComparisonProcessor(submits, submitInfoProcessor, submitSuitabilityChecker,
            fileProcessor, dbResultsCacheManager.Cache);
    }

    private FileProcessor GetFileProcessor(SubmitConfig config)
    {
        FileRequirements? fileRequirements = config.Filters?.FileRequirements;
        DirectoryRequirements? directoryRequirements = config.Filters?.DirectoryRequirements;
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics = config.Metrics;

        return new FileProcessor(fileRequirements, directoryRequirements, metrics);
    }

    private void SaveResultsToDatabase(IComparisonResultRepo resultRepo, ISubmitRepo fileRepo,
        ComparisonResultsTable<SubmitComparisonResult> results)
    {
        var updater = new DbResultsUpdater(resultRepo, fileRepo);
        updater.UpdateResults(results);
    }

    private void SetProgressBar(SubmitComparisonProcessor submitComparisonProcessor, ProgressBarConfig config)
    {
        if (config.IsEnabled)
        {
            submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        }
    }
}