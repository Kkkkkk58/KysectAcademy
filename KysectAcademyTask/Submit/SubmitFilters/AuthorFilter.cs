namespace KysectAcademyTask.Submit.SubmitFilters;

internal class AuthorFilter : Filter<string>
{
    public AuthorFilter(IReadOnlyList<string>? whiteList = null, IReadOnlyList<string>? blackList = null)
    : base(whiteList, blackList) {}

    public AuthorFilter() : base() {}

    public override bool IsSatisfiedBy(string value)
    {
        return !BlackList.Contains(value);
    }
}