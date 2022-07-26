using KysectAcademyTask.Submit.SubmitFilters;

namespace KysectAcademyTask.Submit;

internal class SubmitSuitabilityChecker
{
    private readonly Filters? _filters;

    public SubmitSuitabilityChecker(Filters? filters)
    {
        _filters = filters;
    }

    public bool AreSuitable(SubmitInfo submit1, SubmitInfo submit2)
    {
        return IsAnyAuthorFromWhiteList(submit1, submit2)
               && AreSubmitsFromDifferentAuthors(submit1, submit2)
               && IsSameHomework(submit1, submit2);
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
}