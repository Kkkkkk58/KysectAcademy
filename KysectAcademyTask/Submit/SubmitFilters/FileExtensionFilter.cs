namespace KysectAcademyTask.Submit.SubmitFilters;

public class FileExtensionFilter : Filter<string>
{
    public FileExtensionFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public FileExtensionFilter() : base() { }

    public override bool IsSatisfiedBy(string value)
    {
        string extension = Path.GetExtension(value);
        return base.IsSatisfiedBy(extension);
    }
}