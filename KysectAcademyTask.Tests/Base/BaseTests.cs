using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;

namespace KysectAcademyTask.Tests.Base;

public class BaseTests
{
    protected static readonly IReadOnlyList<ComparisonAlgorithm.Metrics> DefaultMetrics =
        new List<ComparisonAlgorithm.Metrics> { ComparisonAlgorithm.Metrics.Jaccard };

    protected const string DefaultDateTimeFormat = "yyyyMMddHHmmss";
    protected const int DefaultDirDepth = 5;

    protected string GetRootPath(string relativeRootPath)
    {
        return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, relativeRootPath);
    }

    protected string GetResultPath(string rootPath, ReportType reportType)
    {
        return reportType switch
        {
            ReportType.Json => Path.Combine(rootPath, "result.json"),
            ReportType.Txt => Path.Combine(rootPath, "result.txt"),
            ReportType.Console => throw new ArgumentException("No available path for console reports"),
            _ => throw new NotImplementedException()
        };
    }
    
    protected void RunApplication(AppSettingsConfig config)
    {
        var app = new SubmitComparisonApp(config);
        app.Run();
    }
}