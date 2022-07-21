using System.Collections.Immutable;
using KysectAcademyTask.Submit;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.SubmitComparison;

internal class SubmitGetter
{
    private readonly SubmitConfig _config;

    public SubmitGetter(SubmitConfig config)
    {
        _config = config;
    }

    public IReadOnlyList<SubmitInfo> GetSubmits()
    {
        IReadOnlyCollection<string> allSubmitDirectories =
            new DepthDirectoriesGetter(_config.RootDir, _config.SubmitDirDepth).DepthDirectories;
        return allSubmitDirectories
            .Where(PassesFilters)
            .Select(submitDir =>
                new SubmitInfoProcessor(_config.RootDir, _config.SubmitTimeFormat)
                    .GetSubmitInfo(submitDir))
            .ToImmutableList();
    }

    private bool PassesFilters(string submitDirectory)
    {
        SubmitInfo curSubmitInfo =
            new SubmitInfoProcessor(_config.RootDir, _config.SubmitTimeFormat).GetSubmitInfo(submitDirectory);
        return _config.Filters is null
               || ((Filters)_config.Filters).AreDirectoryRequirementsNullOrSatisfied(submitDirectory)
               && ((Filters)_config.Filters).AreSubmitRequirementsNullOrSatisfied(curSubmitInfo);
    }
}