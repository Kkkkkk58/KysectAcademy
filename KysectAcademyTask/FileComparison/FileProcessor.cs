namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileGetterConfig _config;
    private readonly ComparisonResultsTable _comparisonResultsTable;

    public FileProcessor()
    {
        _config = new AppSettingsParser().GetFileGetterConfig();
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
        string[] fileContents = new string[fileNames.Length];

        for (int i = 0; i < fileNames.Length - 1; ++i)
        {
            for (int j = i + 1; j < fileNames.Length; ++j)
            {
                WriteFileContents(fileNames, fileContents, i, j);
                ComparisonResult comparisonResult = new FileComparer(fileNames[i], fileNames[j])
                    .Compare(fileContents[i], fileContents[j]);
                _comparisonResultsTable.AddComparisonResult(comparisonResult);
            }

            fileContents[i] = string.Empty;
        }
    }

    private void WriteFileContents(string[] fileNames, string[] fileContents, int i, int j)
    {
        WriteFileContent(fileNames[i], fileContents, i);
        WriteFileContent(fileNames[j], fileContents, j);
    }

    private void WriteFileContent(string fileName, string[] fileContents, int i)
    {
        if (fileContents[i] == null)
        {
            fileContents[i] = File.ReadAllText(fileName);
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