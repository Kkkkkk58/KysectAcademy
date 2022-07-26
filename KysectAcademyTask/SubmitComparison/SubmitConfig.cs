using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.SubmitComparison;

internal readonly struct SubmitConfig
{
    public string RootDir { get; }
    public Filters? Filters { get; }
    public IReadOnlyList<ComparisonAlgorithm.Metrics> Metrics { get; }
    public string SubmitTimeFormat { get; }
    public int SubmitDirDepth { get; }

    public SubmitConfig(string rootDir, Filters? filters, IReadOnlyList<ComparisonAlgorithm.Metrics> metrics,
        string submitTimeFormat, int submitDirDepth)
    {
        RootDir = rootDir;
        Filters = filters;
        Metrics = metrics;
        SubmitTimeFormat = submitTimeFormat;
        SubmitDirDepth = submitDirDepth;
    }
}