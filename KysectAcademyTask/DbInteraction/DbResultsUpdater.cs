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

    public void UpdateResults(ComparisonResultsTable<SubmitComparisonResult> results)
    {
        var resultsToUpdate = new List<DataAccess.Models.Entities.ComparisonResult>();
        var resultsToAdd = new List<DataAccess.Models.Entities.ComparisonResult>();

        foreach (SubmitComparisonResult result in results.Where(r => r.Source == ResultSource.NewFileComparison))
        {
            IQueryable<DataAccess.Models.Entities.ComparisonResult> dbQuery = GetSubmitComparisonResultQuery(result);
            if (dbQuery.Any())
            {
                AddDataToUpdate(dbQuery, result, resultsToUpdate);
            }
            else
            {
                AddDataToAppend(result, resultsToAdd);
            }
        }

        _resultRepo.UpdateRange(resultsToUpdate);
        _resultRepo.AddRange(resultsToAdd);
    }

    private void AddDataToUpdate(IQueryable<DataAccess.Models.Entities.ComparisonResult> dbQuery,
        SubmitComparisonResult result, ICollection<DataAccess.Models.Entities.ComparisonResult> resultsToUpdate)
    {
        DataAccess.Models.Entities.ComparisonResult resultData = dbQuery.Single();
        if (AreResultsSame(resultData, result))
            return;

        resultData.SimilarityRate = result.SimilarityRate;
        resultsToUpdate.Add(resultData);
    }

    private void AddDataToAppend(SubmitComparisonResult result,
        ICollection<DataAccess.Models.Entities.ComparisonResult> resultsToAdd)
    {
        DataAccess.Models.Entities.ComparisonResult resultData = GetDataModelFromNewComparison(result);
        resultsToAdd.Add(resultData);
    }

    private IQueryable<DataAccess.Models.Entities.ComparisonResult> GetSubmitComparisonResultQuery(
        SubmitComparisonResult result)
    {
        DataAccess.Models.Entities.Submit submit1 = ToDataAccessModel(result.SubmitInfo1);
        DataAccess.Models.Entities.Submit submit2 = ToDataAccessModel(result.SubmitInfo2);

        return _resultRepo.GetQueryWithProps(submit1, submit2);
    }

    private DataAccess.Models.Entities.Submit ToDataAccessModel(SubmitInfo submitInfo)
    {
        return _submitRepo
            .GetQueryWithProps(submitInfo.AuthorName, submitInfo.GroupName, submitInfo.HomeworkName,
                submitInfo.SubmitDate)
            .Single();
    }

    private DataAccess.Models.Entities.ComparisonResult GetDataModelFromNewComparison(SubmitComparisonResult result)
    {
        return new DataAccess.Models.Entities.ComparisonResult
        {
            Submit1Navigation = ToDataAccessModel(result.SubmitInfo1),
            Submit2Navigation = ToDataAccessModel(result.SubmitInfo2),
            SimilarityRate = result.SimilarityRate
        };
    }

    private bool AreResultsSame(DataAccess.Models.Entities.ComparisonResult resultData, SubmitComparisonResult result)
    {
        return Math.Abs(resultData.SimilarityRate - result.SimilarityRate) < double.Epsilon;
    }
}