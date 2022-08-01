using KysectAcademyTask.AppSettings;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

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

    protected string GetResultPath(string rootPath)
    {
        return Path.Combine(rootPath, "result.json");
    }

    protected void RunApplication(AppSettingsConfig config)
    {
        var app = new SubmitComparisonApp(config);
        app.Run();
    }
}