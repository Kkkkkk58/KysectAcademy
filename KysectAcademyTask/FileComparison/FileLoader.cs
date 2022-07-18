namespace KysectAcademyTask.FileComparison;

internal class FileLoader
{
    private Dictionary<string, string> _files;

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

    public void CombineWithOtherLoaders(params FileLoader[] fileLoaders)
    {
        var loaders = new FileLoader[fileLoaders.Length + 1];
        loaders[0] = this;
        fileLoaders.CopyTo(loaders, 1);
        _files = loaders
            .SelectMany(dict => dict._files)
            .ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    private Dictionary<string, string> GetAllContents(string[] fileNames)
    {
        return fileNames
            .ToDictionary(fileName => fileName, File.ReadAllText);
    }
}