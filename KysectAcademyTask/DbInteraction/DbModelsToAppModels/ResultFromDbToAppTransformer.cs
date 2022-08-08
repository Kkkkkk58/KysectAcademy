using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction.DbModelsToAppModels;

public class ResultFromDbToAppTransformer
{
    public SubmitComparisonResult Transform(DataAccess.Models.Entities.ComparisonResult result)
    {
        SubmitInfo submitInfo1 = GetSubmitInfo(result.Submit1Navigation);
        SubmitInfo submitInfo2 = GetSubmitInfo(result.Submit2Navigation);

        return new SubmitComparisonResult(submitInfo1, submitInfo2, result.SimilarityRate, ResultSource.Database);
    }

    private SubmitInfo GetSubmitInfo(DataAccess.Models.Entities.Submit submit)
    {
        return new SubmitFromDbToAppTransformer().Transform(submit);
    }
}