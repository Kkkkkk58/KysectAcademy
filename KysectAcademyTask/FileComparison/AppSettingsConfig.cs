using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

internal struct AppSettingsConfig
{
    public FileGetterConfig FileGetterConfig { get; init; }
    public string OutputFilePath { get; init; }
    public ComparisonAlgorithm.Metrics Metrics { get; init; }

    public AppSettingsConfig(FileGetterConfig? fileGetterConfig, string? outputFilePath,
        ComparisonAlgorithm.Metrics? metrics)
    {
        FileGetterConfig = fileGetterConfig ?? throw new ArgumentNullException(nameof(fileGetterConfig));
        OutputFilePath = outputFilePath ?? throw new ArgumentNullException(nameof(outputFilePath));
        Metrics = metrics ?? ComparisonAlgorithm.Metrics.Jaccard;
    }
}