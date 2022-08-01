using KysectAcademyTask.DataAccess.Repos.Interfaces;
using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.DbInteraction;

public class DbResultsUpdater
{
    private readonly IComparisonResultRepo _resultRepo;
    private readonly IFileEntityRepo _fileRepo;

    public DbResultsUpdater(IComparisonResultRepo resultRepo, IFileEntityRepo fileRepo)
    {
        _resultRepo = resultRepo;
        _fileRepo = fileRepo;
    }

    public void SaveNew(ComparisonResultsTable results)
    {
        var resultsToAdd = new List<DataAccess.Models.Entities.ComparisonResult>();

        foreach (ComparisonResult result in results)
        {
            AddResultIfNotPresent(result, resultsToAdd);
        }

        _resultRepo.AddRange(resultsToAdd);
    }

    private void AddResultIfNotPresent(ComparisonResult result,
        List<DataAccess.Models.Entities.ComparisonResult> resultsToAdd)
    {
        if (_resultRepo.GetQueryWithProps(result.FileName1, result.FileName2, result.Metrics.ToString()).Any())
            return;

        int file1Id = _fileRepo.GetQueryWithProps(result.FileName1).Single().Id;
        int file2Id = _fileRepo.GetQueryWithProps(result.FileName2).Single().Id;

        var comparisonResultData = new DataAccess.Models.Entities.ComparisonResult
        {
            File1Id = file1Id,
            File2Id = file2Id,
            Metrics = result.Metrics.ToString(),
            SimilarityRate = result.SimilarityRate
        };
        resultsToAdd.Add(comparisonResultData);
    }
}