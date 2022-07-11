using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileGetterConfig _config;

    public FileProcessor()
    {
        _config = new AppSettingsParser().GetFileGetterConfig();
    }

    public ComparisonResultsTable GetComparisonResults(ComparisonAlgorithm.Metrics metrics)
    {
        return CompareFiles(metrics);
    }

    private ComparisonResultsTable CompareFiles(ComparisonAlgorithm.Metrics metrics)
    {
        ComparisonResultsTable comparisonResultsTable = new();
        string[] fileNames = GetFileNames();
        string[] fileContents = GetFileContents(fileNames);
        // Loop through each pair of files
        for (int i = 0; i < fileNames.Length - 1; ++i)
        {
            for (int j = i + 1; j < fileNames.Length; ++j)
            {
                ComparisonResult comparisonResult = new FileComparer(fileNames[i], fileNames[j], metrics)
                    .Compare(fileContents[i], fileContents[j]);
                comparisonResultsTable.AddComparisonResult(comparisonResult);
            }

            // Setting the processed fileContent to an empty string to let the GarbageCollector
            // get rid of the large string that is not needed anymore
            fileContents[i] = string.Empty;
        }
        return comparisonResultsTable;
    }

    private string[] GetFileNames()
    {
        if (Directory.Exists(_config.FolderName))
        {
            return GetFileNamesFromSuitableCtor();
        }

        throw new DirectoryNotFoundException(_config.FolderName);
    }

    private string[] GetFileNamesFromSuitableCtor()
    {
        if (_config.FileOptions.SearchPattern is null)
        {
            return Directory.GetFiles(_config.FolderName);
        }

        if (_config.FileOptions.SearchOption is not null)
        {
            return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern,
                (SearchOption)_config.FileOptions.SearchOption);
        }

        return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern);
    }

    private string[] GetFileContents(string[] fileNames)
    {
        string[] fileContents = new string[fileNames.Length];
        for (int i = 0; i < fileContents.Length; ++i)
        {
            fileContents[i] = File.ReadAllText(fileNames[i]);
        }
        return fileContents;
    }
}