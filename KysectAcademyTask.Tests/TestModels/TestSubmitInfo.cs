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

    public override bool Equals(object obj)
    {
        return obj is TestSubmitInfo submit
               && GroupName.Equals(submit.GroupName, StringComparison.OrdinalIgnoreCase)
               && AuthorName.Equals(submit.AuthorName, StringComparison.OrdinalIgnoreCase)
               && HomeworkName.Equals(submit.HomeworkName, StringComparison.OrdinalIgnoreCase)
               && SubmitDate.Equals(submit.SubmitDate);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(GroupName, AuthorName, HomeworkName, SubmitDate);
    }

    public static bool operator ==(TestSubmitInfo lhs, TestSubmitInfo rhs)
    {
        return lhs.Equals(rhs);
    }

    public static bool operator !=(TestSubmitInfo lhs, TestSubmitInfo rhs)
    {
        return !(lhs == rhs);
    }
}