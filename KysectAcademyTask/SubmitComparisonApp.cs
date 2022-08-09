using KysectAcademyTask.AppSettings;
using KysectAcademyTask.ComparisonResult;
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
            IReadOnlyList<SubmitInfo> submits = GetListOfSubmits();
            SubmitComparisonDbContext dbContext = GetDbContext();
            AllRepos allRepos = GetAllRepos(dbContext);

            if (dbContext is not null)
            {
                PrepareDatabase(allRepos, submits);
            }

            DbResultsCacheManager cacheManager = GetCacheManager(allRepos.ComparisonResultRepo);
            SubmitComparisonProcessor submitComparisonProcessor = GetSubmitComparisonProcessor(submits, cacheManager);
            SetProgressBar(submitComparisonProcessor);
            ComparisonResultsTable<SubmitComparisonResult> results = submitComparisonProcessor.GetComparisonResults();
            IReporter<SubmitComparisonResult> reporter = GetReporter();
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

    private IReadOnlyList<SubmitInfo> GetListOfSubmits()
    {
        return new SubmitGetter(_config.SubmitConfig).GetSubmits();
    }

    private SubmitComparisonDbContext GetDbContext()
    {
        DbConfig dbConfig = _config.DbConfig;

        string connectionString = dbConfig.ConnectionStrings?["SubmitComparison"];
        if (connectionString is null or "" || dbConfig.DataProvider is null)
        {
            return null;
        }

        return new SubmitComparisonDbContextFactory()
            .GetDbContext((DataProvider)dbConfig.DataProvider, connectionString);
    }

    private static AllRepos GetAllRepos(SubmitComparisonDbContext dbContext)
    {
        IComparisonResultRepo comparisonResultRepo = new ComparisonResultRepo(dbContext);
        IGroupRepo groupRepo = new GroupRepo(dbContext);
        IHomeWorkRepo homeWorkRepo = new HomeWorkRepo(dbContext);
        IStudentRepo studentRepo = new StudentRepo(dbContext);
        ISubmitRepo submitRepo = new SubmitRepo(dbContext);

        return new AllRepos(comparisonResultRepo, groupRepo, homeWorkRepo, studentRepo, submitRepo);
    }

    private static void PrepareDatabase(AllRepos repos, IReadOnlyCollection<SubmitInfo> submits)
    {
        new DbPreparer(repos.GroupRepo, repos.HomeWorkRepo, repos.StudentRepo, repos.SubmitRepo)
            .Prepare(submits);
    }

    private DbResultsCacheManager GetCacheManager(IComparisonResultRepo comparisonResultRepo)
    {
        return new DbResultsCacheManager(comparisonResultRepo, _config.DbConfig.Recheck);
    }
    
    private SubmitComparisonProcessor GetSubmitComparisonProcessor(IReadOnlyList<SubmitInfo> submits, DbResultsCacheManager dbResultsCacheManager)
    {
        SubmitInfoProcessor submitInfoProcessor = GetSubmitInfoProcessor();
        var submitSuitabilityChecker =
            new SubmitSuitabilityChecker(_config.SubmitConfig.Filters, submitInfoProcessor, dbResultsCacheManager);
        FileProcessor fileProcessor = GetFileProcessor();

        return new SubmitComparisonProcessor(submits, submitInfoProcessor, submitSuitabilityChecker,
            fileProcessor, dbResultsCacheManager.Cache);
    }

    private SubmitInfoProcessor GetSubmitInfoProcessor()
    {
        SubmitConfig config = _config.SubmitConfig;
        return new SubmitInfoProcessor(config.RootDir, config.SubmitTimeFormat);
    }

    private FileProcessor GetFileProcessor()
    {
        SubmitConfig submitConfig = _config.SubmitConfig;

        FileRequirements? fileRequirements = submitConfig.Filters?.FileRequirements;
        DirectoryRequirements? directoryRequirements = submitConfig.Filters?.DirectoryRequirements;
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics = submitConfig.Metrics;

        return new FileProcessor(fileRequirements, directoryRequirements, metrics);
    }

    private IReporter<SubmitComparisonResult> GetReporter()
    {
        return new ReporterFactory().GetReporter<SubmitComparisonResult>(_config.ReportConfig);
    }

    private static void SaveResultsToDatabase(IComparisonResultRepo resultRepo, ISubmitRepo fileRepo,
        ComparisonResultsTable<SubmitComparisonResult> results)
    {
        var updater = new DbResultsUpdater(resultRepo, fileRepo);
        updater.UpdateResults(results);
    }

    private void SetProgressBar(SubmitComparisonProcessor submitComparisonProcessor)
    {
        if (_config.ProgressBarConfig.IsEnabled)
        {
            submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        }
    }
}