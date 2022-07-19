namespace KysectAcademyTask.Submit.SubmitFilters;

internal class FileExtensionFilter : Filter<string>
{
    public FileExtensionFilter(IReadOnlyList<string>? whiteList, IReadOnlyList<string>? blackList)
        : base(whiteList, blackList)
    {
    }

    public FileExtensionFilter() : base() { }

    public override bool IsSatisfiedBy(string path)
    {
        string extension = Path.GetExtension(path);
        return base.IsSatisfiedBy(extension);
    }
}