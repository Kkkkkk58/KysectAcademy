namespace KysectAcademyTask.Submit.SubmitFilters;

internal class HomeworkFilter : Filter<string>
{
    public HomeworkFilter(IReadOnlyList<string>? whiteList, IReadOnlyList<string>? blackList)
        : base(whiteList, blackList)
    {
    }

    public HomeworkFilter() : base() { }
}