namespace KysectAcademyTask.Submit;

internal readonly struct SubmitInfo
{
    public string GroupName { get; init; }
    public string AuthorName { get; init; }
    public string HomeworkName { get; init; }
    public DateTime? SubmitDate { get; init; }

    public SubmitInfo(string groupName, string authorName, string homeworkName, DateTime? submitDate)
    {
        GroupName = groupName;
        AuthorName = authorName;
        HomeworkName = homeworkName;
        SubmitDate = submitDate;
    }


}