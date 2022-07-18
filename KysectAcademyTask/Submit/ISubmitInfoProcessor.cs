namespace KysectAcademyTask.Submit;

internal interface ISubmitInfoProcessor
{
    public SubmitInfo GetSubmitInfo(string rootPath, string submitPath, string dateTimeFormat);

    public string SubmitInfoToDirectoryPath(SubmitInfo submitInfo, string rootPath, string dateTimeFormat);
}