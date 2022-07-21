using System.Globalization;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Submit;

internal class SubmitInfoProcessor : ISubmitInfoProcessor
{
    public SubmitInfo GetSubmitInfo(string rootPath, string submitPath, string dateTimeFormat)
    {
        string submitRelativePath = Path.GetRelativePath(rootPath, submitPath);
        IReadOnlyList<string> subDirectories = new DirectoryPathSplitter(submitRelativePath).SplitDirectories;
        SubmitInfo submitInfo = GetSubmitInfoFromSubDirectories(subDirectories, dateTimeFormat);
        return submitInfo;
    }

    private SubmitInfo GetSubmitInfoFromSubDirectories(IReadOnlyList<string> subDirectories,
        string dateTimeFormat)
    {
        try
        {
            string groupName = GetGroupName(subDirectories);
            string authorName = GetAuthorName(subDirectories);
            string homeworkName = GetHomeWorkName(subDirectories);
            DateTime? submitDate = GetSubmitDate(subDirectories, dateTimeFormat);
            return new SubmitInfo(groupName, authorName, homeworkName, submitDate);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentException("Wrong directory architecture", e);
        }
    }

    private string GetGroupName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[0];
    }

    private string GetAuthorName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[1];
    }

    private string GetHomeWorkName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[2];
    }

    private DateTime? GetSubmitDate(IReadOnlyList<string> subDirectories, string dateTimeFormat)
    {
        if (subDirectories.Count < 4)
        {
            return null;
        }

        if (!DateTime.TryParseExact(subDirectories[3], dateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime submitDate))
        {
            throw new ArgumentException($"Wrong format of SubmitDate folder name: {subDirectories[3]}");
        }

        return submitDate;
    }

    public string SubmitInfoToDirectoryPath(SubmitInfo submitInfo, string rootPath, string dateTimeFormat)
    {
        string path = Path.Combine(rootPath, submitInfo.GroupName, submitInfo.AuthorName, submitInfo.HomeworkName);
        if (submitInfo.SubmitDate.HasValue)
        {
            string submitDate = submitInfo.SubmitDate.Value.ToString(dateTimeFormat);
            path = Path.Combine(path, submitDate);
        }

        return path;
    }
}