namespace KysectAcademyTask.Submit.SubmitFilters;

public class FileNameFilter : Filter<string>
{
    public FileNameFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public FileNameFilter() : base() { }

    public override bool IsSatisfiedBy(string value)
    {
        string fileName = Path.GetFileName(value);
        return base.IsSatisfiedBy(fileName);
    }
}