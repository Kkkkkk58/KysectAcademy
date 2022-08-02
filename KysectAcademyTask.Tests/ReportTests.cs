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
    [Fact]
    public void ReportTest_TxtFormat_SavedFile()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory");
        string resultPath = GetResultPath(rootPath, ReportType.Txt);
        DeleteFileIfExists(resultPath);

        AppSettingsConfig config = GetConfig(resultPath, rootPath, ReportType.Txt);

        RunApplication(config);

        Assert.True(File.Exists(resultPath));
    }

    [Fact]
    public void ReportTest_TxtFormat_SavedNonEmptyFile()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory");
        string resultPath = GetResultPath(rootPath, ReportType.Txt);
        DeleteFileIfExists(resultPath);

        AppSettingsConfig config = GetConfig(resultPath, rootPath, ReportType.Txt);

        RunApplication(config);

        string resultString = File.ReadAllText(resultPath);
        Assert.NotEmpty(resultString);
    }

    [Fact]
    public void ReportTest_JsonFormat_SavedFile()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory");
        string resultPath = GetResultPath(rootPath, ReportType.Json);
        DeleteFileIfExists(resultPath);
        AppSettingsConfig config = GetConfig(resultPath, rootPath, ReportType.Json);

        RunApplication(config);

        Assert.True(File.Exists(resultPath));
    }

    [Fact]
    public void ReportTest_JsonFormat_SavedNonEmptyFile()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory");
        string resultPath = GetResultPath(rootPath, ReportType.Json);
        DeleteFileIfExists(resultPath);
        AppSettingsConfig config = GetConfig(resultPath, rootPath, ReportType.Json);

        RunApplication(config);

        string resultString = File.ReadAllText(resultPath);
        Assert.NotEmpty(resultString);
    }
    
    [Fact]
    public void ReportTest_JsonFormat_ProducesValidJson()
    {
        string rootPath = GetRootPath(@"FilesForTests\ReportTests\RootDirectory");
        string resultPath = GetResultPath(rootPath, ReportType.Json);
        DeleteFileIfExists(resultPath);
        AppSettingsConfig config = GetConfig(resultPath, rootPath, ReportType.Json);

        RunApplication(config);

        string jsonString = File.ReadAllText(resultPath);
        bool deserializationSucceeded = TryDeserialize(jsonString);
        Assert.True(deserializationSucceeded);
    }

    private AppSettingsConfig GetConfig(string resultPath, string rootPath, ReportType reportType)
    {
        return new AppSettingsConfig
        {
            DbConfig = new DbConfig(null),
            ReportConfig = new ReportConfig(reportType, resultPath),
            SubmitConfig = new SubmitConfig(rootPath, null, DefaultMetrics, DefaultDateTimeFormat, DefaultDirDepth),
            ProgressBarConfig = new ProgressBarConfig(false)
        };
    }

    private static void DeleteFileIfExists(string resultPath)
    {
        if (resultPath is null)
        {
            throw new ArgumentNullException(nameof(resultPath));
        }

        if (File.Exists(resultPath))
        {
            File.Delete(resultPath);
        }
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