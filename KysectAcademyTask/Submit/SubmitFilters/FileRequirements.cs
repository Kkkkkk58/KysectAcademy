namespace KysectAcademyTask.Submit.SubmitFilters;

public readonly struct FileRequirements : IRequirements<string>
{
    public FileNameFilter FileNameFilter { get; init; }
    public FileExtensionFilter FileExtensionFilter { get; init; }

    public FileRequirements(FileNameFilter fileNameFilter = null, FileExtensionFilter fileExtensionFilter = null)
    {
        FileNameFilter = fileNameFilter;
        FileExtensionFilter = fileExtensionFilter;
    }

    public bool AreSatisfiedBy(string item)
    {
        return IsFilterNullOrSatisfiedBy(FileNameFilter, item)
               && IsFilterNullOrSatisfiedBy(FileExtensionFilter, item);
    }

    private static bool IsFilterNullOrSatisfiedBy<T>(Filter<T> filter, T value)
    {
        return filter is null
               || (value is null && filter.WhiteList.Count == 0)
               || (value is not null && filter.IsSatisfiedBy(value));
    }
}