namespace KysectAcademyTask.Submit.SubmitFilters;

internal class SubmitDateFilter : Filter<DateTime>
{
    public SubmitDateFilter(IReadOnlyList<DateTime>? whiteList, IReadOnlyList<DateTime>? blackList)
        : base(whiteList, blackList)
    {
    }

    public SubmitDateFilter() : base() { }
}