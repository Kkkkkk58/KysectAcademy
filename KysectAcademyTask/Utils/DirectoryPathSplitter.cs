namespace KysectAcademyTask.Utils;

internal class DirectoryPathSplitter
{
    private readonly string[] _splitDirectories;

    public DirectoryPathSplitter(string path)
    {
        _splitDirectories = path.Split(Path.DirectorySeparatorChar, StringSplitOptions.RemoveEmptyEntries);
    }

    public IReadOnlyList<string> SplitDirectories
    {
        get => _splitDirectories;
    }
}