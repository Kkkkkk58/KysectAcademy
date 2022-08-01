namespace KysectAcademyTask.Submit.SubmitFilters;

public class HomeworkFilter : Filter<string>
{
    public HomeworkFilter(IReadOnlyList<string> whiteList, IReadOnlyList<string> blackList)
        : base(whiteList, blackList)
    {
    }

    public HomeworkFilter() : base() { }
}