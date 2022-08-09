using KysectAcademyTask.Submit;

namespace KysectAcademyTask.ComparisonResult.Transformer;

public class FilesToSubmitComparisonTransformer
{
    private readonly SubmitInfo _submit1, _submit2;

    public FilesToSubmitComparisonTransformer(SubmitInfo submit1, SubmitInfo submit2)
    {
        _submit1 = submit1;
        _submit2 = submit2;
    }

    public SubmitComparisonResult Transform(ComparisonResultsTable<FileComparisonResult> fileComparisonResults)
    {
        ComparisonResultsTable<FileComparisonResult> unitedMetricsTable =
            GetResultsWithUnitedMetrics(fileComparisonResults);

        ComparisonResultsTable<FileComparisonResult> maxSimilaritiesTable =
            GetResultsWithMaxSimilarities(unitedMetricsTable);

        return GetCompositionResult(maxSimilaritiesTable);
    }

    private SubmitComparisonResult GetCompositionResult(
        ComparisonResultsTable<FileComparisonResult> maxSimilaritiesTable)
    {
        double compositionSimilarityRate = maxSimilaritiesTable.Average(result => result.SimilarityRate);

        return new SubmitComparisonResult(_submit1, _submit2, compositionSimilarityRate);
    }


    private ComparisonResultsTable<FileComparisonResult> GetResultsWithUnitedMetrics(
        ComparisonResultsTable<FileComparisonResult> fileComparisonResults)
    {
        int resultingNumberOfFields = GetNumberOfFieldsWithUnitedMetrics(fileComparisonResults);
        var unitedMetricsTable = new ComparisonResultsTable<FileComparisonResult>(resultingNumberOfFields);

        foreach (FileComparisonResult result in fileComparisonResults)
        {
            if (unitedMetricsTable.Any(listedResults =>
                    listedResults.FileName1 == result.FileName1 && listedResults.FileName2 == result.FileName2))
            {
                continue;
            }

            double combinedSimilarityRate = GetCombinedMetricsRate(fileComparisonResults, result);

            var combinedResult = new FileComparisonResult(result.FileName1, result.FileName2, result.Metrics,
                combinedSimilarityRate, result.Source);

            unitedMetricsTable.AddComparisonResult(combinedResult);
        }

        return unitedMetricsTable;
    }

    private int GetNumberOfFieldsWithUnitedMetrics(ComparisonResultsTable<FileComparisonResult> fileComparisonResults)
    {
        int numberOfMetrics = GetNumberOfMetrics(fileComparisonResults);
        return fileComparisonResults.Count / numberOfMetrics;
    }

    private static int GetNumberOfMetrics(ComparisonResultsTable<FileComparisonResult> fileComparisonResults)
    {
        return fileComparisonResults
            .Select(result => result.Metrics)
            .Distinct()
            .Count();
    }

    private static double GetCombinedMetricsRate(ComparisonResultsTable<FileComparisonResult> fileComparisonResults,
        FileComparisonResult result)
    {
        return fileComparisonResults
            .Where(r => r.FileName1 == result.FileName1 && r.FileName2 == result.FileName2)
            .Average(r => r.SimilarityRate);
    }

    private ComparisonResultsTable<FileComparisonResult> GetResultsWithMaxSimilarities(
        ComparisonResultsTable<FileComparisonResult> unitedMetricsTable)
    {
        int resultingNumberOfFields = GetNumberOfFieldsWithMaxSimilarities(unitedMetricsTable);
        var maxSimilaritiesTable = new ComparisonResultsTable<FileComparisonResult>(resultingNumberOfFields);

        foreach (FileComparisonResult result in unitedMetricsTable)
        {
            if (maxSimilaritiesTable.Any(listedResults => listedResults.FileName1 == result.FileName1))
                continue;

            FileComparisonResult resultWithMaxSimilarity = GetResultWithMaxSimilarity(unitedMetricsTable, result);

            maxSimilaritiesTable.AddComparisonResult(resultWithMaxSimilarity);
        }

        return maxSimilaritiesTable;
    }

    private static int GetNumberOfFieldsWithMaxSimilarities(
        ComparisonResultsTable<FileComparisonResult> unitedMetricsTable)
    {
        return unitedMetricsTable
            .Select(result => result.FileName1)
            .Distinct()
            .Count();
    }

    private static FileComparisonResult GetResultWithMaxSimilarity(
        ComparisonResultsTable<FileComparisonResult> unitedMetricsTable, FileComparisonResult result)
    {
        return unitedMetricsTable
            .Where(r => r.FileName1 == result.FileName1)
            .MaxBy(r => r.SimilarityRate);
    }
}