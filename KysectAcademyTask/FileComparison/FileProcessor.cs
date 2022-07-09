namespace KysectAcademyTask.FileComparison;

internal class FileProcessor
{
    private readonly FileGetterConfig _config;
    private FileComparer _comparer;

    public FileProcessor()
    {
        _config = new AppSettingsParser().GetConfig();
        _comparer = new FileComparer();
    }

    internal void CompareFiles()
    {
        string[] fileNames = GetFileNames();
        //foreach (string file in fileNames)
        //{
        //    Console.WriteLine(file);
        //}
    }

    private string[] GetFileNames()
    {
        if (Directory.Exists(_config.FolderName))
            return GetFileNamesFromSuitableCtor();
        throw new DirectoryNotFoundException(_config.FolderName);
    }

    private string[] GetFileNamesFromSuitableCtor()
    {
        if (_config.FileOptions.SearchPattern == null)
            return Directory.GetFiles(_config.FolderName);

        if (_config.FileOptions.SearchOption != null)
        {
            return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern,
                (SearchOption)_config.FileOptions.SearchOption);
        }
        return Directory.GetFiles(_config.FolderName, _config.FileOptions.SearchPattern);
    }
}