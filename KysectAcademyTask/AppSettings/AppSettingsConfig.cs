using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;
using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.AppSettings;

internal struct AppSettingsConfig
{
    public string InputDirectory { get; init; }
    public Filters? Filters { get; init; }
    public IReadOnlyList<ComparisonAlgorithm.Metrics> Metrics { get; init; }
    public ReportConfig Report { get; init; }

    public AppSettingsConfig(string? inputDirectory, Filters? filters,
        IReadOnlyList<ComparisonAlgorithm.Metrics>? metrics, ReportConfig? report)
    {
        InputDirectory = inputDirectory ?? throw new ArgumentNullException(nameof(inputDirectory));
        Filters = filters;
        Metrics = metrics ?? new List<ComparisonAlgorithm.Metrics> { ComparisonAlgorithm.Metrics.Jaccard };
        Report = report ?? new ReportConfig(ReportType.Console);
    }
}