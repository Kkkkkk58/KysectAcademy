namespace KysectAcademyTask.Utils;

internal class DepthDirectoriesGetter
{
    private readonly List<string> _depthDirectories;
    private readonly int _depth;

    public IReadOnlyCollection<string> DepthDirectories
    {
        get => _depthDirectories;
    }

    public DepthDirectoriesGetter(string pathToRootFolder, int depth)
    {
        _depthDirectories = new List<string>();
        _depth = depth;
        FillDepthDirectoriesList(pathToRootFolder, 1);
    }

    private void FillDepthDirectoriesList(string directoryPath, int recursionDepth)
    {
        string[] subDirectories = Directory.GetDirectories(directoryPath, "*", SearchOption.TopDirectoryOnly);
        if (subDirectories.Length == 0 || recursionDepth == _depth)
        {
            _depthDirectories.Add(directoryPath);
        }
        else
        {
            foreach (string subDirectoryPath in subDirectories)
            {
                FillDepthDirectoriesList(subDirectoryPath, recursionDepth + 1);
            }
        }
    }
}