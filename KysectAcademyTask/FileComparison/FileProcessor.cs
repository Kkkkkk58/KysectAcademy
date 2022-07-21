using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private string[] _fileNames1, _fileNames2;
    private FileLoader _loader;
    private IReadOnlyCollection<ComparisonAlgorithm.Metrics> _metrics;

    public FileProcessor(string directory1, string directory2, FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements, IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        InitFileNames(directory1, directory2, fileRequirements, directoryRequirements);
        InitLoader();
        InitMetrics(metrics);
        if (_fileNames1 is null || _fileNames2 is null || _loader is null || _metrics is null)
        {
            throw new ApplicationException("Unable to initialize FileProcessor");
        }
    }

    private void InitFileNames(string directory1, string directory2, FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements)
    {
        FileNamesGetter fileNamesGetter = new(fileRequirements, directoryRequirements);
        _fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        _fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);
    }

    private void InitMetrics(IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        _metrics = metrics;
    }

    private void InitLoader()
    {
        FileLoader fileLoader1 = new(_fileNames1);
        FileLoader fileLoader2 = new(_fileNames2);
        _loader = new FileLoadersCombiner().Combine(fileLoader1, fileLoader2);
    }

    public ComparisonResultsTable GetComparisonResults()
    {
        ComparisonResultsTable comparisonResultsTable = new();
        foreach (ComparisonAlgorithm.Metrics metric in _metrics)
        {
            ComparisonResultsTable resultsUsingMetrics = CompareFiles(metric);
            comparisonResultsTable.AddTable(resultsUsingMetrics);
        }

        return comparisonResultsTable;
    }

    private ComparisonResultsTable CompareFiles(ComparisonAlgorithm.Metrics metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();

        FileComparer fileComparer = new(_loader, metrics);

        foreach (string fileName1 in _fileNames1)
        {
            foreach (string fileName2 in _fileNames2)
            {
                ComparisonResult comparisonResult = fileComparer.Compare(fileName1, fileName2);
                comparisonResultsTable.AddComparisonResult(comparisonResult);
            }
        }

        return comparisonResultsTable;
    }
}