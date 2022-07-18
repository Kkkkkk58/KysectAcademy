namespace KysectAcademyTask.Utils;

internal class LevelDirectoriesGetter
{
    private readonly List<string> _levelDirectories;
    private readonly int _level;

    public IReadOnlyCollection<string> LevelDirectories
    {
        get => _levelDirectories;
    }

    public LevelDirectoriesGetter(string pathToRootFolder, int level)
    {
        _levelDirectories = new List<string>();
        _level = level;
        FillLevelDirectoriesList(pathToRootFolder, 1);
    }

    private void FillLevelDirectoriesList(string directoryPath, int recursionDepth)
    {
        string[] subDirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);
        if (subDirectories.Length == 0 || recursionDepth == _level)
        {
            _levelDirectories.Add(directoryPath);
        }
        else
        {
            foreach (string subDirectoryPath in subDirectories)
            {
                FillLevelDirectoriesList(subDirectoryPath, recursionDepth + 1);
            }
        }
    }
}