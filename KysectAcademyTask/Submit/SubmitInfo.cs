namespace KysectAcademyTask.Submit;

internal readonly struct SubmitInfo
{
    public string GroupName { get; }
    public string AuthorName { get; }
    public string HomeworkName { get; }
    public DateTime? SubmitDate { get; }

    public SubmitInfo(string groupName, string authorName, string homeworkName, DateTime? submitDate)
    {
        GroupName = groupName;
        AuthorName = authorName;
        HomeworkName = homeworkName;
        SubmitDate = submitDate;
    }
}