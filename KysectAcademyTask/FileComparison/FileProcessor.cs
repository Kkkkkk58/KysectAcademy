using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;
    private readonly IReadOnlyCollection<ComparisonAlgorithm.Metrics> _metrics;
    private string[] _fileNames1, _fileNames2;
    private FileLoader _loader;

    public FileProcessor(FileRequirements? fileRequirements,
        DirectoryRequirements? directoryRequirements, IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
        _metrics = metrics;
    }

    public ComparisonResultsTable Compare(string directory1, string directory2)
    {
        PrepareForComparison(directory1, directory2);
        return GetComparisonResults();
    }

    private void PrepareForComparison(string dirName1, string dirName2)
    {
        SetFileNames(dirName1, dirName2);
        SetLoader();
    }

    private void SetFileNames(string directory1, string directory2)
    {
        FileNamesGetter fileNamesGetter = new(_fileRequirements, _directoryRequirements);
        _fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        _fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);
    }

    private void SetLoader()
    {
        FileLoader fileLoader1 = new(_fileNames1);
        FileLoader fileLoader2 = new(_fileNames2);
        _loader = new FileLoadersCombiner().Combine(fileLoader1, fileLoader2);
    }

    private ComparisonResultsTable GetComparisonResults()
    {
        ComparisonResultsTable comparisonResultsTable = new();
        foreach (ComparisonAlgorithm.Metrics metric in _metrics)
        {
            ComparisonResultsTable resultsUsingMetrics = PerformFilesComparison(metric);
            comparisonResultsTable.AddTable(resultsUsingMetrics);
        }

        return comparisonResultsTable;
    }

    private ComparisonResultsTable PerformFilesComparison(ComparisonAlgorithm.Metrics metrics)
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