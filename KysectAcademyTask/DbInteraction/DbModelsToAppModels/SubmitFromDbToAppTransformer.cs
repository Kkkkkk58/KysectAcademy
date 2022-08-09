using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction.DbModelsToAppModels;

public class SubmitFromDbToAppTransformer
{
    public SubmitInfo Transform(DataAccess.Models.Entities.Submit submit)
    {
        string groupName = GetGroupName(submit);
        string authorName = GetAuthorName(submit);
        string homeWorkName = GetHomeWorkName(submit);
        DateTime? submitDate = GetSubmitDate(submit);

        return new SubmitInfo(groupName, authorName, homeWorkName, submitDate);
    }

    private string GetGroupName(DataAccess.Models.Entities.Submit submit)
    {
        return submit.StudentNavigation.GroupNavigation.Name;
    }

    private string GetAuthorName(DataAccess.Models.Entities.Submit submit)
    {
        return submit.StudentNavigation.PersonalInformation.FullName;
    }

    private string GetHomeWorkName(DataAccess.Models.Entities.Submit submit)
    {
        return submit.HomeWorkNavigation.Name;
    }

    private DateTime? GetSubmitDate(DataAccess.Models.Entities.Submit submit)
    {
        return submit.Date;
    }
}