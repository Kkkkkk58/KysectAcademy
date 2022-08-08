using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DataAccess.EfStructures;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Report;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Tests.Base;
using KysectAcademyTask.Tests.TestModels;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using Xunit;

namespace KysectAcademyTask.Tests;

public class DbTests : BaseTests
{
    private readonly string _relativeRootPath = @$"FilesForTests{Path.DirectorySeparatorChar}DbTests{Path.DirectorySeparatorChar}RootDirectory";

    [Fact]
    public void DbTest_NoDbConfigProvided_SourceIsFileComparison()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

        AppSettingsConfig config = GetConfig(null);
        RunApplication(config);

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
        Assert.True(AllResultsAreFromSource(results, ResultSource.NewFileComparison));
    }

    [Fact]
    public void DbTest_EnableDbCompareTwice_FinalSourceIsDb()
    {
        InitPaths(_relativeRootPath, ReportType.Json);

        AppSettingsConfig config = GetConfig("DataSource=file:memdb1?mode=memory&cache=shared");
        RunApplication(config);

        IReadOnlyCollection<TestSubmitComparisonResult> results = GetResults();
        Assert.True(AllResultsAreFromSource(results, ResultSource.NewFileComparison));

        RunApplication(config);

        results = GetResults();
        Assert.True(AllResultsAreFromSource(results, ResultSource.Database));
    }

    private AppSettingsConfig GetConfig(string connectionString)
    {
        var connectionStrings = new Dictionary<string, string>
        {
            { "SubmitComparison", connectionString }
        };

        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(connectionStrings, DataProvider.SqLite),
            ReportConfig = new ReportConfig(ReportType.Json, ResultPath),
            SubmitConfig = new SubmitConfig(RootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private bool AllResultsAreFromSource(IReadOnlyCollection<TestSubmitComparisonResult> results, ResultSource source)
    {
        return results.All(r => r.Source == source);
    }
}