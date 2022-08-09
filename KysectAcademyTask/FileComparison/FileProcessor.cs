using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.FileComparison;

public class FileProcessor
{
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;
    private readonly IReadOnlyCollection<ComparisonAlgorithm.Metrics> _metrics;

    public FileProcessor(FileRequirements? fileRequirements, DirectoryRequirements? directoryRequirements,
        IReadOnlyCollection<ComparisonAlgorithm.Metrics> metrics)
    {
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
        _metrics = metrics;
    }

    public ComparisonResultsTable<FileComparisonResult> CompareDirectories(string directory1, string directory2)
    {
        var fileNamesGetter = new FileNamesGetter(_fileRequirements, _directoryRequirements);
        string[] fileNames1 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory1);
        string[] fileNames2 = fileNamesGetter.GetFileNamesSatisfyingRequirements(directory2);


        FileLoader loader = GetCombinedLoader(fileNames1, fileNames2);

        return GetComparisonResults(fileNames1, fileNames2, loader);
    }


    private static FileLoader GetCombinedLoader(IEnumerable<string> fileNames1, IEnumerable<string> fileNames2)
    {
        var fileLoader1 = new FileLoader(fileNames1);
        var fileLoader2 = new FileLoader(fileNames2);
        FileLoader commonLoader = new FileLoadersCombiner().Combine(fileLoader1, fileLoader2);

        return commonLoader;
    }

    private ComparisonResultsTable<FileComparisonResult> GetComparisonResults(IReadOnlyCollection<string> fileNames1,
        IReadOnlyCollection<string> fileNames2, FileLoader loader)
    {
        int numberOfComparisons = GetNumberOfComparisonsWithMetrics(fileNames1, fileNames2);
        var comparisonResultsTable = new ComparisonResultsTable<FileComparisonResult>(numberOfComparisons);
        foreach (ComparisonAlgorithm.Metrics metric in _metrics)
        {
            ComparisonResultsTable<FileComparisonResult> resultsUsingMetrics =
                PerformFilesComparison(fileNames1, fileNames2, loader, metric);
            comparisonResultsTable.AddTable(resultsUsingMetrics);
        }

        return comparisonResultsTable;
    }

    private static ComparisonResultsTable<FileComparisonResult> PerformFilesComparison(
        IReadOnlyCollection<string> fileNames1, IReadOnlyCollection<string> fileNames2,
        FileLoader loader, ComparisonAlgorithm.Metrics metrics)
    {
        int numberOfComparisons = GetNumberOfPairToPairComparisons(fileNames1, fileNames2);
        var comparisonResultsTable = new ComparisonResultsTable<FileComparisonResult>(numberOfComparisons);
        var fileComparer = new FileComparer(loader, metrics);

        foreach (string fileName1 in fileNames1)
        {
            foreach (string fileName2 in fileNames2)
            {
                FileComparisonResult fileComparisonResult = GetComparisonResult(fileName1, fileName2, fileComparer);
                comparisonResultsTable.AddComparisonResult(fileComparisonResult);
            }
        }

        return comparisonResultsTable;
    }

    private static FileComparisonResult GetComparisonResult(string fileName1, string fileName2,
        FileComparer fileComparer)
    {
        FileComparisonResult fileComparisonResult = fileComparer.Compare(fileName1, fileName2);

        return fileComparisonResult;
    }

    private static int GetNumberOfPairToPairComparisons(IReadOnlyCollection<string> fileNames1,
        IReadOnlyCollection<string> fileNames2)
    {
        return fileNames1.Count * fileNames2.Count;
    }

    private int GetNumberOfComparisonsWithMetrics(IReadOnlyCollection<string> fileNames1,
        IReadOnlyCollection<string> fileNames2)
    {
        return GetNumberOfPairToPairComparisons(fileNames1, fileNames2) * _metrics.Count;
    }
}