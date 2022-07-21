using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessorBuilder
{
    private readonly string? _directory1, _directory2;
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;
    private readonly IReadOnlyCollection<ComparisonAlgorithm.Metrics>? _metrics;

    public FileProcessorBuilder(string? directory1 = null, string? directory2 = null,
        FileRequirements? fileRequirements = null, DirectoryRequirements? directoryRequirements = null,
        IReadOnlyCollection<ComparisonAlgorithm.Metrics>? metrics = null)
    {
        _directory1 = directory1;
        _directory2 = directory2;
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
        _metrics = metrics;
    }

    public FileProcessor GetFileProcessorInstance()
    {
        if (_directory1 is null || _directory2 is null || _metrics is null)
        {
            throw new InvalidOperationException(
                "FileProcessor object must have non-null values for paths to directories");
        }

        return new FileProcessor(_directory1, _directory2, _fileRequirements, _directoryRequirements, _metrics);
    }

    public FileProcessorBuilder BuildFileRequirements(FileRequirements? fileRequirements)
    {
        return new FileProcessorBuilder(_directory1, _directory2, fileRequirements, _directoryRequirements, _metrics);
    }

    public FileProcessorBuilder BuildDirectoryRequirements(DirectoryRequirements? directoryRequirements)
    {
        return new FileProcessorBuilder(_directory1, _directory2, _fileRequirements, directoryRequirements, _metrics);
    }

    public FileProcessorBuilder BuildDirectory1(string directory1)
    {
        return new FileProcessorBuilder(directory1, _directory2, _fileRequirements, _directoryRequirements, _metrics);
    }

    public FileProcessorBuilder BuildDirectory2(string directory2)
    {
        return new FileProcessorBuilder(_directory1, directory2, _fileRequirements, _directoryRequirements, _metrics);
    }

    public FileProcessorBuilder BuildMetrics(IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        return new FileProcessorBuilder(_directory1, _directory2, _fileRequirements, _directoryRequirements, metrics);
    }
}