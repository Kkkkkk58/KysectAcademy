using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.Report;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Tests.Base;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using Xunit;

namespace KysectAcademyTask.Tests;

public class ComparisonTests : BaseTests
{
    [Fact]
    public void FileComparison_SimilarFiles_SimilarityRateIsOne()
    {
        LaunchTestCase(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}SimilarFiles");

        double result = GetSimilarityRateFromReport();
        Assert.Equal(1, result);
    }

    [Fact]
    public void FileComparison_TotallyDifferentFiles_SimilarityRateIsZero()
    {
        LaunchTestCase(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}DifferentFiles");

        double result = GetSimilarityRateFromReport();
        Assert.Equal(0, result);
    }

    [Fact]
    public void FileComparison_FilesHaveSomeSimilarities_SimilarityRateIsBetweenZeroAndOne()
    {
        LaunchTestCase(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}FilesWithSomeSimilarities");

        double result = GetSimilarityRateFromReport();
        Assert.True(result is > 0 and < 1);
    }

    private void LaunchTestCase(string relativeRootPath)
    {
        InitPaths(relativeRootPath, ReportType.Json);
        AppSettingsConfig config = GetConfig();

        RunApplication(config);
    }

    private AppSettingsConfig GetConfig()
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null, null),
            ReportConfig = new ReportConfig(ReportType.Json, ResultPath),
            SubmitConfig = new SubmitConfig(RootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private double GetSimilarityRateFromReport()
    {
        return GetResults()
            .Single()
            .SimilarityRate;
    }
}