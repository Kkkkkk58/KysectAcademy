namespace KysectAcademyTask.FileComparison;

internal class FileLoader
{
    public IReadOnlyDictionary<string, string> Files
    {
        get => _files;
    }
    private readonly Dictionary<string, string> _files;

    public FileLoader(Dictionary<string, string> files)
    {
        _files = files;
    }

    public FileLoader(IEnumerable<string> fileNames)
    {
        _files = GetAllContents(fileNames);
    }
    
    public string GetFileContent(string fileName)
    {
        if (!_files.TryGetValue(fileName, out string? fileContent))
        {
            throw new ArgumentException("No such file among listed fileNames");
        }

        return fileContent;
    }

    private Dictionary<string, string> GetAllContents(IEnumerable<string> fileNames)
    {
        return fileNames
            .ToDictionary(fileName => fileName, File.ReadAllText);
    }
}