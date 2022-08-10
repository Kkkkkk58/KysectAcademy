namespace KysectAcademyTask.Submit.SubmitFilters;

public class AuthorFilter : Filter<string>
{
    public AuthorFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public AuthorFilter() : base() { }

    public override bool IsSatisfiedBy(string value)
    {
        return !BlackList.Contains(value);
    }
}