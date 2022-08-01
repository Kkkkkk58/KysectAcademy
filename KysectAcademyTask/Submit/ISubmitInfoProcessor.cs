namespace KysectAcademyTask.Submit;

public interface ISubmitInfoProcessor
{
    public SubmitInfo GetSubmitInfo(string submitPath);

    public string SubmitInfoToDirectoryPath(SubmitInfo submitInfo);
}