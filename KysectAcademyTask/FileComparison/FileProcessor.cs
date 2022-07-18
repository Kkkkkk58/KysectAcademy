using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private string[] _fileNames1, _fileNames2;
    private FileLoader _loader;

    public FileProcessor(string directory1, string directory2, FileRequirements? fileRequirements, DirectoryRequirements? directoryRequirements)
    {
        InitFileNames(directory1, directory2, fileRequirements, directoryRequirements);
        InitLoader();
        if (_fileNames1 is null || _fileNames2 is null || _loader is null)
        {
            throw new ApplicationException("Unable to initialize FileProcessor");
        }
    }

    private void InitFileNames(string directory1, string directory2, FileRequirements? fileRequirements, DirectoryRequirements? directoryRequirements)
    {
        FileNamesGetter fileNamesGetter = new(fileRequirements, directoryRequirements);
        _fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        _fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);
    }

    private void InitLoader()
    {
        FileLoader fileLoader1 = new(_fileNames1);
        FileLoader fileLoader2 = new(_fileNames2);
        fileLoader1.CombineWithOtherLoaders(fileLoader2);
        _loader = fileLoader1;
    }

    public ComparisonResultsTable GetComparisonResults(IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();
        foreach (ComparisonAlgorithm.Metrics metric in metrics)
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