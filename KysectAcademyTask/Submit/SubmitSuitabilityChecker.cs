using KysectAcademyTask.DbInteraction;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Submit;

public class SubmitSuitabilityChecker
{
    private readonly Filters? _filters;
    private readonly SubmitInfoProcessor _submitInfoProcessor;
    private readonly DbResultsCacheManager _cacheManager;

    public SubmitSuitabilityChecker(Filters? filters, SubmitInfoProcessor submitInfoProcessor,
        DbResultsCacheManager cacheManager)
    {
        _filters = filters;
        _submitInfoProcessor = submitInfoProcessor;
        _cacheManager = cacheManager;
    }

    public bool AreSuitable(SubmitInfo submit1, SubmitInfo submit2)
    {
        return IsAnyAuthorFromWhiteList(submit1, submit2)
               && AreSubmitsFromDifferentAuthors(submit1, submit2)
               && IsSameHomework(submit1, submit2)
               && AreNotEmptyAfterApplyingFilters(submit1, submit2)
               && SatisfyCacheManager(submit1, submit2);
    }

    private bool IsAnyAuthorFromWhiteList(SubmitInfo submit1, SubmitInfo submit2)
    {
        return _filters is null
               || ((Filters)_filters).IsAuthorNameNullOrIsContainedInWhiteList(submit1.AuthorName)
               || ((Filters)_filters).IsAuthorNameNullOrIsContainedInWhiteList(submit2.AuthorName);
    }

    private bool AreSubmitsFromDifferentAuthors(SubmitInfo submit1, SubmitInfo submit2)
    {
        return submit1.AuthorName != submit2.AuthorName;
    }

    private bool IsSameHomework(SubmitInfo submit1, SubmitInfo submit2)
    {
        return string.Equals(submit1.HomeworkName,
            submit2.HomeworkName, StringComparison.CurrentCultureIgnoreCase);
    }

    private bool AreNotEmptyAfterApplyingFilters(SubmitInfo submit1, SubmitInfo submit2)
    {
        IEnumerable<string> fileNames1 = GetSubmitFileNames(submit1);
        IEnumerable<string> fileNames2 = GetSubmitFileNames(submit2);
        return _filters is null
               || fileNames1.Any()
               && fileNames2.Any();
    }

    private bool SatisfyCacheManager(SubmitInfo submit1, SubmitInfo submit2)
    {
        return _cacheManager.RecheckEnabled
               || !_cacheManager.CacheContainsComparisonResult(submit1, submit2);
    }

    private IEnumerable<string> GetSubmitFileNames(SubmitInfo submit)
    {
        string dirName = _submitInfoProcessor.SubmitInfoToDirectoryPath(submit);
        return new FileNamesGetter(_filters?.FileRequirements, _filters?.DirectoryRequirements)
            .GetFileNamesSatisfyingRequirements(dirName);
    }
}