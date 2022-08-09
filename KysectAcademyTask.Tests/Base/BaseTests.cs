using System.Text.Json;
using System.Text.Json.Serialization;
using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;
using KysectAcademyTask.Tests.TestModels;

namespace KysectAcademyTask.Tests.Base;

public class BaseTests : IDisposable
{
    protected static readonly IReadOnlyList<ComparisonAlgorithm.Metrics> DefaultMetrics =
        new List<ComparisonAlgorithm.Metrics> { ComparisonAlgorithm.Metrics.Jaccard };

    protected const string DefaultDateTimeFormat = "yyyyMMddHHmmss";
    protected const int DefaultDirDepth = 5;

    protected string RootPath, ResultPath;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            DeleteResultFile();
        }
    }

    private void DeleteResultFile()
    {
        if (File.Exists(ResultPath))
        {
            File.Delete(ResultPath);
        }
    }

    protected void InitPaths(string relativeRootPath, ReportType reportType)
    {
        RootPath = GetRootPath(relativeRootPath);
        ResultPath = GetResultPath(reportType);
    }

    private string GetRootPath(string relativeRootPath)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeRootPath);
    }

    private string GetResultPath(ReportType reportType)
    {
        return reportType switch
        {
            ReportType.Json => Path.Combine(RootPath, $"{Guid.NewGuid()}.json"),
            ReportType.Txt => Path.Combine(RootPath, $"{Guid.NewGuid()}.txt"),
            ReportType.Console => throw new ArgumentException("No available path for console reports"),
            _ => throw new NotImplementedException()
        };
    }

    protected IReadOnlyCollection<TestSubmitComparisonResult> GetResults()
    {
        JsonSerializerOptions options = GetDeserializationOptions();
        string jsonString = File.ReadAllText(ResultPath);
        TestSubmitComparisonResult[] results =
            JsonSerializer.Deserialize<TestSubmitComparisonResult[]>(jsonString, options);

        return results;
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

    protected void RunApplication(AppSettingsConfig config)
    {
        var app = new SubmitComparisonApp(config);
        app.Run();
    }
}