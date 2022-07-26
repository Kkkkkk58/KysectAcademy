namespace KysectAcademyTask.Submit;

internal interface ISubmitInfoProcessor
{
    public SubmitInfo GetSubmitInfo(string submitPath);

    public string SubmitInfoToDirectoryPath(SubmitInfo submitInfo);
}