using System.Globalization;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Submit;

public class SubmitInfoProcessor : ISubmitInfoProcessor
{
    private readonly string _rootPath;
    private readonly string _dateTimeFormat;

    public SubmitInfoProcessor(string rootPath, string dateTimeFormat)
    {
        _rootPath = rootPath;
        _dateTimeFormat = dateTimeFormat;
    }

    public SubmitInfo GetSubmitInfo(string submitPath)
    {
        string submitRelativePath = Path.GetRelativePath(_rootPath, submitPath);
        IReadOnlyList<string> subDirectories = new DirectoryPathSplitter(submitRelativePath).SplitDirectories;
        SubmitInfo submitInfo = GetSubmitInfoFromSubDirectories(subDirectories);
        return submitInfo;
    }

    private SubmitInfo GetSubmitInfoFromSubDirectories(IReadOnlyList<string> subDirectories)
    {
        try
        {
            string groupName = GetGroupName(subDirectories);
            string authorName = GetAuthorName(subDirectories);
            string homeworkName = GetHomeWorkName(subDirectories);
            DateTime? submitDate = GetSubmitDate(subDirectories);
            return new SubmitInfo(groupName, authorName, homeworkName, submitDate);
        }
        catch (ArgumentOutOfRangeException e)
        {
            throw new ArgumentException("Wrong directory architecture", e);
        }
    }

    private static string GetGroupName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[0];
    }

    private static string GetAuthorName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[1];
    }

    private static string GetHomeWorkName(IReadOnlyList<string> subDirectories)
    {
        return subDirectories[2];
    }

    private DateTime? GetSubmitDate(IReadOnlyList<string> subDirectories)
    {
        if (subDirectories.Count < 4)
        {
            return null;
        }

        if (!DateTime.TryParseExact(subDirectories[3], _dateTimeFormat, CultureInfo.InvariantCulture,
                DateTimeStyles.None, out DateTime submitDate))
        {
            throw new ArgumentException($"Wrong format of SubmitDate folder name: {subDirectories[3]}");
        }

        return submitDate;
    }

    public string SubmitInfoToDirectoryPath(SubmitInfo submitInfo)
    {
        string path = Path.Combine(_rootPath, submitInfo.GroupName, submitInfo.AuthorName, submitInfo.HomeworkName);
        if (!submitInfo.SubmitDate.HasValue)
            return path;

        string submitDate = submitInfo.SubmitDate.Value.ToString(_dateTimeFormat);
        path = Path.Combine(path, submitDate);

        return path;
    }
}