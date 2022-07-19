namespace KysectAcademyTask.Submit.SubmitFilters;

internal readonly struct SubmitInfoRequirements : IRequirements<SubmitInfo>
{
    public GroupFilter? GroupFilter { get; init; }
    public AuthorFilter? AuthorFilter { get; init;  }
    public HomeworkFilter? HomeworkFilter { get; init; }
    public SubmitDateFilter? SubmitDateFilter { get; init; }

    public SubmitInfoRequirements(GroupFilter? groupFilter, AuthorFilter? authorFilter, HomeworkFilter? homeworkFilter,
        SubmitDateFilter? submitDateFilter)
    {
        GroupFilter = groupFilter;
        AuthorFilter = authorFilter;
        HomeworkFilter = homeworkFilter;
        SubmitDateFilter = submitDateFilter;
    }

    public bool AreSatisfiedBy(SubmitInfo item)
    {
        return IsFilterNullOrSatisfiedBy(GroupFilter, item.GroupName)
               && IsFilterNullOrSatisfiedBy(AuthorFilter, item.AuthorName)
               && IsFilterNullOrSatisfiedBy(HomeworkFilter, item.HomeworkName)
               && IsFilterNullOrSatisfiedBy(SubmitDateFilter, item.SubmitDate);
    }

    private bool IsFilterNullOrSatisfiedBy<T>(Filter<T>? filter, T? value)
    {
        return filter is null
               || (value is null && filter.WhiteList.Count == 0)
               || (value is not null && filter.IsSatisfiedBy(value));
    }

    private bool IsFilterNullOrSatisfiedBy(SubmitDateFilter? filter, DateTime? value)
    {
        return value.HasValue && IsFilterNullOrSatisfiedBy<DateTime>(filter, (DateTime)value)
               || !value.HasValue && filter is not null && filter.WhiteList.Count == 0;
    }
}