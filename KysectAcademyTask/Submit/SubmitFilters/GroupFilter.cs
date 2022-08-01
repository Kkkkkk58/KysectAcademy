namespace KysectAcademyTask.Submit.SubmitFilters;

public class GroupFilter : Filter<string>
{
    public GroupFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public GroupFilter() : base() { }
}