namespace KysectAcademyTask.FileComparison;

public class FileLoadersCombiner
{
    public FileLoader Combine(params FileLoader[] loaders)
    {
        var combinedFiles = loaders
            .SelectMany(loader => loader.Files)
            .ToDictionary(pair => pair.Key, pair => pair.Value);

        return new FileLoader(combinedFiles);
    }
}