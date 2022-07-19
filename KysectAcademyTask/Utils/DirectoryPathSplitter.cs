namespace KysectAcademyTask.Utils;

internal class DirectoryPathSplitter
{
    public IReadOnlyList<string> SplitDirectories
    {
        get => _splitDirectories;
    }

    private readonly string[] _splitDirectories;

    public DirectoryPathSplitter(string path)
    {
        _splitDirectories = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
    }
}