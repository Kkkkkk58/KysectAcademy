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

public class ReportTests : BaseTests
{
    private const string RelativeRootPath = @"FilesForTests\ReportTests\RootDirectory";

    [Theory]
    [InlineData(ReportType.Txt)]
    [InlineData(ReportType.Json)]
    public void ReportTest_OutputFormat_SavedFile(ReportType reportType)
    {
        InitPaths(RelativeRootPath, reportType);
        AppSettingsConfig config = GetConfig(reportType);

        RunApplication(config);

        Assert.True(File.Exists(ResultPath));
    }

    [Theory]
    [InlineData(ReportType.Txt)]
    [InlineData(ReportType.Json)]
    public void ReportTest_OutputFormatAndFilesToCompare_SavedNonEmptyFile(ReportType reportType)
    {
        InitPaths(RelativeRootPath, reportType);
        AppSettingsConfig config = GetConfig(reportType);

        RunApplication(config);

        string resultString = File.ReadAllText(ResultPath);
        Assert.NotEmpty(resultString);
    }

    
    [Fact]
    public void ReportTest_JsonFormat_ProducesValidJson()
    {
        InitPaths(RelativeRootPath, ReportType.Json);
        AppSettingsConfig config = GetConfig(ReportType.Json);

        RunApplication(config);

        string jsonString = File.ReadAllText(ResultPath);
        bool deserializationSucceeded = TryDeserialize(jsonString);
        Assert.True(deserializationSucceeded);

        
    }

    private AppSettingsConfig GetConfig(ReportType reportType)
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null),
            ReportConfig = new ReportConfig(reportType, ResultPath),
            SubmitConfig = new SubmitConfig(RootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private bool TryDeserialize(string jsonString)
    {
        var options = new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };

        try
        {
            Result[] results = JsonSerializer.Deserialize<Result[]>(jsonString, options);
        }
        catch (JsonException)
        {
            return false;
        }

        return true;
    }
}