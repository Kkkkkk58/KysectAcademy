using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.Tests.Base;

public class BaseTests
{
    protected static readonly IReadOnlyList<ComparisonAlgorithm.Metrics> DefaultMetrics =
        new List<ComparisonAlgorithm.Metrics> { ComparisonAlgorithm.Metrics.Jaccard };

    protected const string DefaultDateTimeFormat = "yyyyMMddHHmmss";
    protected const int DefaultDirDepth = 5;
}