using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Submit.SubmitFilters;

public class DirectoryFilter : Filter<string>
{
    public DirectoryFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public DirectoryFilter() : base() { }

    public override bool IsSatisfiedBy(string value)
    {
        IReadOnlyList<string> splitPath = new DirectoryPathSplitter(value).SplitDirectories;
        return (WhiteList.Count == 0 || splitPath.Any(x => WhiteList.Contains(x)))
               && !splitPath.Any(x => BlackList.Contains(x));
    }
}