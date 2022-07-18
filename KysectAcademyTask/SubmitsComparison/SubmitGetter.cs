using System.Collections.Immutable;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.SubmitsComparison;

internal class SubmitGetter
{
    private readonly string _rootDir;
    private readonly Filters? _filters;

    public SubmitGetter(string rootDir, Filters? filters)
    {
        _rootDir = rootDir;
        _filters = filters;
    }

    public IReadOnlyList<SubmitInfo> GetSubmits()
    {
        IReadOnlyCollection<string> allSubmitDirectories = new LevelDirectoriesGetter(_rootDir, 5).LevelDirectories;
        return allSubmitDirectories
            .Where(PassesFilters)
            .Select(submitDir =>
                new SubmitInfoProcessor().GetSubmitInfo(_rootDir, submitDir, "yyyyMMddHHmmss"))
            .ToImmutableList();
    }

    private bool PassesFilters(string submitDirectory)
    {
        SubmitInfo curSubmitInfo =
            new SubmitInfoProcessor().GetSubmitInfo(_rootDir, submitDirectory, "yyyyMMddHHmmss");
        return _filters is null
               || ((Filters)_filters).AreDirectoryRequirementsNullOrSatisfied(submitDirectory)
               && ((Filters)_filters).AreSubmitRequirementsNullOrSatisfied(curSubmitInfo);
    }
}