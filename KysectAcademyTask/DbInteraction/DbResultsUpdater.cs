using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.Submit;

namespace KysectAcademyTask.DbInteraction;

public class DbResultsUpdater
{
    private readonly IComparisonResultRepo _resultRepo;
    private readonly ISubmitRepo _submitRepo;

    public DbResultsUpdater(IComparisonResultRepo resultRepo, ISubmitRepo submitRepo)
    {
        _resultRepo = resultRepo;
        _submitRepo = submitRepo;
    }

    public void SaveNewLeaveOld(ComparisonResultsTable<SubmitComparisonResult> results)
    {
        var resultsToAdd = new List<DataAccess.Models.Entities.ComparisonResult>();

        foreach (SubmitComparisonResult result in results)
        {
            IQueryable<DataAccess.Models.Entities.ComparisonResult> dbQuery = GetSubmitComparisonResultQuery(result);
            if (dbQuery.Any()) 
                continue;

            var resultData = new DataAccess.Models.Entities.ComparisonResult
            {
                Submit1Navigation = ToDataAccessModel(result.SubmitInfo1),
                Submit2Navigation = ToDataAccessModel(result.SubmitInfo2),
                SimilarityRate = result.SimilarityRate
            };
            resultsToAdd.Add(resultData);
        }

        _resultRepo.AddRange(resultsToAdd);
    }

    public void SaveNewUpdateChanged(ComparisonResultsTable<SubmitComparisonResult> results)
    {
        var resultsToUpdate = new List<DataAccess.Models.Entities.ComparisonResult>();
        var resultsToAdd = new List<DataAccess.Models.Entities.ComparisonResult>();

        foreach (SubmitComparisonResult result in results)
        {
            IQueryable<DataAccess.Models.Entities.ComparisonResult> dbQuery = GetSubmitComparisonResultQuery(result);
            if (dbQuery.Any())
            {
                DataAccess.Models.Entities.ComparisonResult resultData = dbQuery.Single();
                if (Math.Abs(resultData.SimilarityRate - result.SimilarityRate) < double.Epsilon)
                    continue;

                resultData.SimilarityRate = result.SimilarityRate;
                resultsToUpdate.Add(resultData);
            }
            else
            {
                var resultData = new DataAccess.Models.Entities.ComparisonResult
                {
                    Submit1Navigation = ToDataAccessModel(result.SubmitInfo1),
                    Submit2Navigation = ToDataAccessModel(result.SubmitInfo2),
                    SimilarityRate = result.SimilarityRate
                };
                resultsToAdd.Add(resultData);
            }
        }

        _resultRepo.UpdateRange(resultsToUpdate);
        _resultRepo.AddRange(resultsToAdd);
    }

    private IQueryable<DataAccess.Models.Entities.ComparisonResult> GetSubmitComparisonResultQuery(SubmitComparisonResult result)
    {
        DataAccess.Models.Entities.Submit submit1 = ToDataAccessModel(result.SubmitInfo1);
        DataAccess.Models.Entities.Submit submit2 = ToDataAccessModel(result.SubmitInfo2);

        return _resultRepo.GetQueryWithProps(submit1, submit2);
    }

    private DataAccess.Models.Entities.Submit ToDataAccessModel(SubmitInfo submitInfo)
    {
        return _submitRepo.GetQueryWithProps(submitInfo.AuthorName, submitInfo.GroupName, submitInfo.HomeworkName,
            submitInfo.SubmitDate).Single();
    }
}