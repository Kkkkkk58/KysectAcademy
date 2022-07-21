using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.Utils;

internal class FileNamesGetter
{
    private readonly FileRequirements? _fileRequirements;
    private readonly DirectoryRequirements? _directoryRequirements;

    public FileNamesGetter(FileRequirements? fileRequirements = null,
        DirectoryRequirements? directoryRequirements = null)
    {
        _fileRequirements = fileRequirements;
        _directoryRequirements = directoryRequirements;
    }

    public string[] GetFileNamesSatisfyingRequirements(string directoryPath)
    {
        if (!Directory.Exists(directoryPath))
        {
            throw new DirectoryNotFoundException(directoryPath);
        }

        return Directory
                .GetFiles(directoryPath, "*", SearchOption.AllDirectories)
                .Where(fileName =>
                    (_directoryRequirements is null
                     || ((DirectoryRequirements)_directoryRequirements).AreSatisfiedBy(fileName))
                    && (_fileRequirements is null
                        || ((FileRequirements)_fileRequirements).AreSatisfiedBy(fileName)))
                .ToArray();
    }
}