namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileGetterConfig _config;
    private readonly FileComparer _comparer;
    private readonly ComparisonResultsTable _comparisonResultsTable;

    public FileProcessor()
    {
        _config = new AppSettingsParser().GetConfig();
        _comparer = new FileComparer();
        _comparisonResultsTable = new ComparisonResultsTable();
    }

    public ComparisonResultsTable GetComparisonResults()
    {
        CompareFiles();
        return _comparisonResultsTable;
    }

    private void CompareFiles()
    {
        string[] fileNames = GetFileNames();
        for (int i = 0; i < fileNames.Length - 1; ++i)
        {
            for (int j = i + 1; j < fileNames.Length; ++j)
            {
                ComparisonResult comparisonResult = new FileComparer().Compare(fileNames[i], fileNames[j]);
                _comparisonResultsTable.AddComparisonResult(comparisonResult);
            }
        }
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
        if (_config.FileOptions.SearchPattern == null)
        {
            return Directory.GetFiles(_config.FolderName);
        }

        if (_config.FileOptions.SearchOption != null)
        {
            return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern,
                (SearchOption)_config.FileOptions.SearchOption);
        }
        return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern);
    }
}