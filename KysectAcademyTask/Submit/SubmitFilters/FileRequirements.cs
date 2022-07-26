namespace KysectAcademyTask.Submit.SubmitFilters;

internal readonly struct FileRequirements : IRequirements<string>
{
    public FileNameFilter FileNameFilter { get; init; }
    public FileExtensionFilter FileExtensionFilter { get; init; }

    public FileRequirements(FileNameFilter fileNameFilter = null, FileExtensionFilter fileExtensionFilter = null)
    {
        FileNameFilter = fileNameFilter;
        FileExtensionFilter = fileExtensionFilter;
    }

    public bool AreSatisfiedBy(string path)
    {
        return IsFilterNullOrSatisfiedBy(FileNameFilter, path)
               && IsFilterNullOrSatisfiedBy(FileExtensionFilter, path);
    }

    private bool IsFilterNullOrSatisfiedBy<T>(Filter<T> filter, T value)
    {
        return filter is null
               || (value is null && filter.WhiteList.Count == 0)
               || (value is not null && filter.IsSatisfiedBy(value));
    }
}