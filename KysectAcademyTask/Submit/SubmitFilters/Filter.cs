namespace KysectAcademyTask.Submit.SubmitFilters;

internal class Filter<T>
{
    public IReadOnlyList<T> WhiteList { get; init; }

    public IReadOnlyList<T> BlackList { get; init; }

    public Filter(IReadOnlyList<T>? whiteList = null, IReadOnlyList<T>? blackList = null)
    {
        WhiteList = whiteList ?? new List<T>();
        BlackList = blackList ?? new List<T>();
        CheckForIntersections();
    }

    private void CheckForIntersections()
    {
        if (WhiteList
                .Intersect(BlackList)
                .ToList().Count > 0)
        {
            throw new ArgumentException("Provided the same values in WhiteList and BlackList");
        }
    }

    public virtual bool IsSatisfiedBy(T value)
    {
        return (WhiteList.Count == 0 || WhiteList.Contains(value))
               && !BlackList.Contains(value);
    }
}