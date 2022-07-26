using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;
    private readonly IReadOnlyCollection<ComparisonAlgorithm.Metrics> _metrics;

    public FileProcessor(FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements, IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
        _metrics = metrics;
    }

    public ComparisonResultsTable CompareDirectories(string directory1, string directory2)
    {
        FileNamesGetter fileNamesGetter = new(_fileRequirements, _directoryRequirements);
        string[] fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        string[] fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);

        FileLoader loader = GetCombinedLoader(fileNames1, fileNames2);

        return GetComparisonResults(fileNames1, fileNames2, loader);
    }


    private FileLoader GetCombinedLoader(string[] fileNames1, string[] fileNames2)
    {
        FileLoader fileLoader1 = new(fileNames1);
        FileLoader fileLoader2 = new(fileNames2);
        FileLoader commonLoader = new FileLoadersCombiner().Combine(fileLoader1, fileLoader2);

        return commonLoader;
    }

    private ComparisonResultsTable GetComparisonResults(string[] fileNames1, string[] fileNames2, FileLoader loader)
    {
        ComparisonResultsTable comparisonResultsTable = new();
        foreach (ComparisonAlgorithm.Metrics metric in _metrics)
        {
            ComparisonResultsTable resultsUsingMetrics = PerformFilesComparison(fileNames1, fileNames2, loader, metric);
            comparisonResultsTable.AddTable(resultsUsingMetrics);
        }

        return comparisonResultsTable;
    }

    private ComparisonResultsTable PerformFilesComparison(string[] fileNames1, string[] fileNames2, FileLoader loader,
        ComparisonAlgorithm.Metrics metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();

        FileComparer fileComparer = new(loader, metrics);

        foreach (string fileName1 in fileNames1)
        {
            foreach (string fileName2 in fileNames2)
            {
                ComparisonResult comparisonResult = fileComparer.Compare(fileName1, fileName2);
                comparisonResultsTable.AddComparisonResult(comparisonResult);
            }
        }

        return comparisonResultsTable;
    }
}