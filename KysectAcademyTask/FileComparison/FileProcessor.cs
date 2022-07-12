using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileGetterConfig _config;

    public FileProcessor(FileGetterConfig config)
    {
        _config = config;
    }

    public ComparisonResultsTable GetComparisonResults(ComparisonAlgorithm.Metrics metrics)
    {
        return CompareFiles(metrics);
    }

    private ComparisonResultsTable CompareFiles(ComparisonAlgorithm.Metrics metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();
        string[] fileNames = GetFileNames();

        FileLoader fileLoader = new(fileNames);
        FileComparer fileComparer = new(fileLoader, metrics);
        // Loop through each pair of files
        for (int i = 0; i < fileNames.Length - 1; ++i)
        {
            for (int j = i + 1; j < fileNames.Length; ++j)
            {
                ComparisonResult comparisonResult = fileComparer.Compare(fileNames[i], fileNames[j]);
                comparisonResultsTable.AddComparisonResult(comparisonResult);
            }

            fileLoader.FreeFileContent(fileNames[i]);
        }

        return comparisonResultsTable;
    }

    private string[] GetFileNames()
    {
        if (Directory.Exists(_config.FolderName))
        {
            return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern,
                _config.FileOptions.SearchOption);
        }

        throw new DirectoryNotFoundException(_config.FolderName);
    }
}