namespace KysectAcademyTask.FileComparison;

internal class FileLoader
{
    private readonly Dictionary<string, string> _files;

    public FileLoader(string[] fileNames)
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

    public void FreeFileContent(string fileName)
    {
        if (!_files.ContainsKey(fileName))
        {
            throw new ArgumentException("No such file among listed fileNames");
        }

        _files.Remove(fileName);
    }

    private Dictionary<string, string> GetAllContents(string[] fileNames)
    {
        return fileNames
            .ToDictionary(fileName => fileName, File.ReadAllText);
    }
}