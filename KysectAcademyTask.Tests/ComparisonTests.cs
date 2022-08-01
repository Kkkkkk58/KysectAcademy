using System.Text.Json;
using System.Text.Json.Serialization;
using KysectAcademyTask.AppSettings;
using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
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
        string rootPath = GetRootPath(@"FilesForTests\ComparisonTests\SimilarFiles");
        string resultPath = GetResultPath(rootPath);

        AppSettingsConfig config = GetConfig(resultPath, rootPath);

        RunApplication(config);

        double result = GetResultFromJsonFile(resultPath);
        Assert.Equal(1, result);

    }

    [Fact]
    public void FileComparison_TotallyDifferentFiles_SimilarityRateIsZero()
    {
        string rootPath = GetRootPath(@"FilesForTests\ComparisonTests\DifferentFiles");
        string resultPath = GetResultPath(rootPath);

        AppSettingsConfig config = GetConfig(resultPath, rootPath);

        RunApplication(config);

        double result = GetResultFromJsonFile(resultPath);
        Assert.Equal(0, result);
    }

    [Fact]
    public void FileComparison_FilesHaveSomeSimilarities_SimilarityRateIsBetweenZeroAndOne()
    {
        string rootPath = GetRootPath(@"FilesForTests\ComparisonTests\FilesWithSomeSimilarities");
        string resultPath = GetResultPath(rootPath);

        AppSettingsConfig config = GetConfig(resultPath, rootPath);

        RunApplication(config);

        double result = GetResultFromJsonFile(resultPath);
        Assert.True(result is > 0 and < 1);
    }

    private string GetRootPath(string relativeRootPath)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeRootPath);
    }

    private string GetResultPath(string rootPath)
    {
        return Path.Combine(rootPath, "result.json");
    }

    private AppSettingsConfig GetConfig(string resultPath, string rootPath)
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null),
            ReportConfig = new ReportConfig(ReportType.Json, resultPath),
            SubmitConfig = new SubmitConfig(rootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private void RunApplication(AppSettingsConfig config)
    {
        var app = new SubmitComparisonApp(config);
        app.Run();
    }

    private double GetResultFromJsonFile(string path)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        string jsonString = File.ReadAllText(path);
        Result[] results = JsonSerializer.Deserialize<Result[]>(jsonString, options);

        return results!
            .Single()
            .SimilarityRate;
    }

    private struct Result
    {
        public string FileName1 { get; set; }
        public string FileName2 { get; set; }
        public ComparisonAlgorithm.Metrics Metrics { get; set; }
        public double SimilarityRate { get; set; }
    }
}