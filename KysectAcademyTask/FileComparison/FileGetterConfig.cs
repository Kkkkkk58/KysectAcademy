namespace KysectAcademyTask.FileComparison;

internal struct FileGetterConfig
{
    public string FolderName { get; init; }
    public FileOptions FileOptions { get; init; }

    public FileGetterConfig(string folderName, FileOptions fileOptions)
    {
        FolderName = folderName;
        FileOptions = fileOptions;
    }
}