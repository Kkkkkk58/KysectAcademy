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
    private readonly string _relativeRootPath =
        @$"FilesForTests{Path.DirectorySeparatorChar}ReportTests{Path.DirectorySeparatorChar}RootDirectory";

    [Theory]
    [InlineData(ReportType.Txt)]
    [InlineData(ReportType.Json)]
    public void ReportTest_OutputFormat_SavedFile(ReportType reportType)
    {
        LaunchTestCase(reportType);

        Assert.True(File.Exists(ResultPath));
    }
    
    [Theory]
    [InlineData(ReportType.Txt)]
    [InlineData(ReportType.Json)]
    public void ReportTest_OutputFormatAndFilesToCompare_SavedNonEmptyFile(ReportType reportType)
    {
        LaunchTestCase(reportType);

        string resultString = File.ReadAllText(ResultPath);
        Assert.NotEmpty(resultString);
    }

    [Fact]
    public void ReportTest_JsonFormat_ProducesValidJson()
    {
        LaunchTestCase(ReportType.Json);

        string jsonString = File.ReadAllText(ResultPath);
        bool deserializationSucceeded = TryDeserialize(jsonString);
        Assert.True(deserializationSucceeded);
    }

    private void LaunchTestCase(ReportType reportType)
    {
        InitPaths(_relativeRootPath, reportType);
        AppSettingsConfig config = GetConfig(reportType);

        RunApplication(config);
    }

    private AppSettingsConfig GetConfig(ReportType reportType)
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null, null),
            ReportConfig = new ReportConfig(reportType, ResultPath),
            SubmitConfig = new SubmitConfig(RootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private bool TryDeserialize(string jsonString)
    {
        JsonSerializerOptions options = GetDeserializationOptions();

        try
        {
            TestSubmitComparisonResult[] results =
                JsonSerializer.Deserialize<TestSubmitComparisonResult[]>(jsonString, options);
        }
        catch (JsonException)
        {
            return false;
        }

        return true;
    }

    private JsonSerializerOptions GetDeserializationOptions()
    {
        return new JsonSerializerOptions
        {
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }
}