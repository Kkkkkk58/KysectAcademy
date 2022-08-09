using System.Text.Json;
using System.Text.Json.Serialization;
using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.Report;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Tests.Base;
using KysectAcademyTask.Tests.TestModels;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;
using Xunit;

namespace KysectAcademyTask.Tests;

public class ComparisonTests : BaseTests
{
    [Fact]
    public void FileComparison_SimilarFiles_SimilarityRateIsOne()
    {
        InitPaths(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}SimilarFiles",
            ReportType.Json);
        AppSettingsConfig config = GetConfig();

        RunApplication(config);

        double result = GetResultFromJsonFile();
        Assert.Equal(1, result);
    }

    [Fact]
    public void FileComparison_TotallyDifferentFiles_SimilarityRateIsZero()
    {
        InitPaths(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}DifferentFiles",
            ReportType.Json);
        AppSettingsConfig config = GetConfig();

        RunApplication(config);

        double result = GetResultFromJsonFile();
        Assert.Equal(0, result);
    }

    [Fact]
    public void FileComparison_FilesHaveSomeSimilarities_SimilarityRateIsBetweenZeroAndOne()
    {
        InitPaths(
            @$"FilesForTests{Path.DirectorySeparatorChar}ComparisonTests{Path.DirectorySeparatorChar}FilesWithSomeSimilarities",
            ReportType.Json);
        AppSettingsConfig config = GetConfig();

        RunApplication(config);

        double result = GetResultFromJsonFile();
        Assert.True(result is > 0 and < 1);
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

    private double GetResultFromJsonFile()
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        string jsonString = File.ReadAllText(ResultPath);
        TestSubmitComparisonResult[] results =
            JsonSerializer.Deserialize<TestSubmitComparisonResult[]>(jsonString, options);

        return results!
            .Single()
            .SimilarityRate;
    }
}