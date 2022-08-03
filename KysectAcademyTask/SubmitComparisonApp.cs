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
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar;
using Microsoft.EntityFrameworkCore;


namespace KysectAcademyTask;

public class SubmitComparisonApp
{
    private readonly AppSettingsConfig _config;
    private const string DbPreparingMessage = "\t\t\tPreparing database...";
    private const string DbUpdatingMessage = "\n\n\t\t\tSaving new results to database...";

    public SubmitComparisonApp(AppSettingsConfig config)
    {
        _config = config;
    }

    public void Run()
    {
        try
        {
            IReadOnlyList<SubmitInfo> submits = new SubmitGetter(_config.SubmitConfig).GetSubmits();
            FileComparisonDbContext dbContext = GetDbContext(_config.DbConfig);
            AllRepos allRepos = GetAllRepos(dbContext);
            SubmitInfoProcessor submitInfoProcessor = GetSubmitInfoProcessor(_config.SubmitConfig);

            if (dbContext is not null)
            {
                PrepareDatabase(allRepos, submits, submitInfoProcessor);
            }

            SubmitComparisonProcessor submitComparisonProcessor =
                GetSubmitComparisonProcessor(_config.SubmitConfig, submits, submitInfoProcessor, dbContext);
            SetProgressBar(submitComparisonProcessor, _config.ProgressBarConfig);
            ComparisonResultsTable results = submitComparisonProcessor.GetComparisonResults();
            IReporter reporter = new ReporterFactory().GetReporter(_config.ReportConfig);
            reporter.MakeReport(results);

            if (dbContext is not null)
            {
                UpdateDatabase(allRepos.ComparisonResultRepo, allRepos.FileEntityRepo, results);
            }
        }
        catch (Exception e)
        {
            throw new ApplicationException($"An error occurred during the app run: {e.Message}", e);
        }
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
        string connectionString = dbConfig.ConnectionStrings?["SubmitComparison"];
        if (connectionString is null or "")
        {
            return null;
        }

        return new FileComparisonDbContextFactory()
            .GetDbContext(dbConfig.DataProvider, connectionString);
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

    private void SetProgressBar(SubmitComparisonProcessor submitComparisonProcessor, ProgressBarConfig config)
    {
        if (config.IsEnabled)
        {
            submitComparisonProcessor.SetProgressBar(new ConsoleComparisonProgressBar());
        }
    }
}