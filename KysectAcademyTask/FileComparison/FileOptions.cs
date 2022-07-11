namespace KysectAcademyTask.FileComparison;

internal struct FileOptions
{
    public string? SearchPattern { get; init; }
    public SearchOption? SearchOption { get; init; }

    public FileOptions(string? searchPattern, SearchOption? searchOption)
    {
        SearchPattern = searchPattern;
        SearchOption = searchOption;
    }
}