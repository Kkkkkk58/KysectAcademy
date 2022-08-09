using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.DbInteraction.DbModelsToAppModels;
using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction;

public class DbResultsCacheManager
{
    public ComparisonResultsTable<SubmitComparisonResult> Cache { get; init; }
    public bool RecheckEnabled { get; init; }

    public DbResultsCacheManager(IComparisonResultRepo resultRepo, bool recheckEnabled)
    {
        Cache = GetCache(resultRepo);
        RecheckEnabled = recheckEnabled;
    }

    public bool CacheContainsComparisonResult(SubmitInfo submit1, SubmitInfo submit2)
    {
        return Cache.Any(result => result.SubmitInfo1 == submit1 && result.SubmitInfo2 == submit2
                                   || result.SubmitInfo1 == submit2 && result.SubmitInfo2 == submit1);
    }

    private static ComparisonResultsTable<SubmitComparisonResult> GetCache(IComparisonResultRepo resultRepo)
    {
        IEnumerable<DataAccess.Models.Entities.ComparisonResult> results = resultRepo?.GetAll();
        var cache = new ComparisonResultsTable<SubmitComparisonResult>();
        if (results is null)
            return cache;

        var resultTransformer = new ResultFromDbToAppTransformer();
        foreach (DataAccess.Models.Entities.ComparisonResult resultData in results)
        {
            SubmitComparisonResult result = resultTransformer.Transform(resultData);
            cache.AddComparisonResult(result);
        }

        return cache;
    }
}