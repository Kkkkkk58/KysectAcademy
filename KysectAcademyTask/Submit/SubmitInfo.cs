﻿namespace KysectAcademyTask.Submit;

public readonly struct SubmitInfo
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

    public override string ToString()
    {
        return
            $"Submit by {AuthorName} from {GroupName} on {HomeworkName}. Submit Date: {(SubmitDate.HasValue ? SubmitDate : "undefined")}";
    }

    public override bool Equals(object obj)
    {
        return obj is SubmitInfo submitInfo
               && Equals(submitInfo);
    }

    public bool Equals(SubmitInfo other)
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

    public static bool operator ==(SubmitInfo lhs, SubmitInfo rhs)
    {
        return rhs.Equals(lhs);
    }

    public static bool operator !=(SubmitInfo lhs, SubmitInfo rhs)
    {
        return !(lhs == rhs);
    }
}