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
               && Equals(submit);
    }

    public bool Equals(TestSubmitInfo other)
    {
        return GroupName.Equals(other.GroupName, StringComparison.OrdinalIgnoreCase)
               && AuthorName.Equals(other.AuthorName, StringComparison.OrdinalIgnoreCase)
               && HomeworkName.Equals(other.HomeworkName, StringComparison.OrdinalIgnoreCase)
               && SubmitDate.Equals(other.SubmitDate);
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