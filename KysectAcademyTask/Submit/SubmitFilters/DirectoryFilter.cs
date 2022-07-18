using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Submit.SubmitFilters;

internal class DirectoryFilter : Filter<string>
{
    public DirectoryFilter(IReadOnlyList<string>? whiteList = null, IReadOnlyList<string>? blackList = null)
        : base(whiteList, blackList) { }
    public DirectoryFilter() : base() { }

    public override bool IsSatisfiedBy(string path)
    {
        IReadOnlyList<string> splitPath = new DirectoryPathSplitter(path).SplitDirectories;
        return (WhiteList.Count == 0 || splitPath.Any(x => WhiteList.Contains(x)))
               && !splitPath.Any(x => BlackList.Contains(x));
    }
}