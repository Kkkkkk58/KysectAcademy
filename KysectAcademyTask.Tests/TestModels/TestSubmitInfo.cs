namespace KysectAcademyTask.Tests.TestModels;

public struct TestSubmitInfo
{
    public string GroupName { get; set; }
    public string AuthorName { get; set; }
    public string HomeworkName { get; set; }
    public DateTime? SubmitDate { get; set; }

    public TestSubmitInfo(string groupName, string authorName, string homeworkName, DateTime? submitDate)
    {
        GroupName = groupName;
        AuthorName = authorName;
        HomeworkName = homeworkName;
        SubmitDate = submitDate;    
    }
}