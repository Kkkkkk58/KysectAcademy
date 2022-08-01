namespace KysectAcademyTask.Submit.SubmitFilters;

public readonly struct DirectoryRequirements : IRequirements<string>
{
    public DirectoryFilter DirectoryFilter { get; init; }

    public DirectoryRequirements(DirectoryFilter directoryFilter)
    {
        DirectoryFilter = directoryFilter;
    }

    public bool AreSatisfiedBy(string path)
    {
        return IsFilterNullOrSatisfiedBy(DirectoryFilter, path);
    }

    private bool IsFilterNullOrSatisfiedBy<T>(Filter<T> filter, T value)
    {
        return filter is null
               || (value is null && filter.WhiteList.Count == 0)
               || (value is not null && filter.IsSatisfiedBy(value));
    }
}