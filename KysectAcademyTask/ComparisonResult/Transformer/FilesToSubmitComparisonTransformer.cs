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
        ComparisonResultsTable<FileComparisonResult> unitedMetricsTable = UniteMetrics(fileComparisonResults);
        ComparisonResultsTable<FileComparisonResult> maxSimilaritiesTable =
            GetResultsWithMaxSimilarities(unitedMetricsTable);

        return GetCompositionResult(maxSimilaritiesTable);
    }

    private SubmitComparisonResult GetCompositionResult(ComparisonResultsTable<FileComparisonResult> maxSimilaritiesTable)
    {
        double compositionSimilarityRate = maxSimilaritiesTable.Average(result => result.SimilarityRate);

        return new SubmitComparisonResult(_submit1, _submit2, compositionSimilarityRate);
    }


    private ComparisonResultsTable<FileComparisonResult> UniteMetrics(ComparisonResultsTable<FileComparisonResult> fileComparisonResults)
    {
        var unitedMetricsTable = new ComparisonResultsTable<FileComparisonResult>();
        foreach (FileComparisonResult result in fileComparisonResults)
        {
            if (unitedMetricsTable.Any(listedResults => listedResults.FileName1 == result.FileName1 && listedResults.FileName2 == result.FileName2))
                continue;

            double combinedSimilarityRate = fileComparisonResults
                .Where(r => r.FileName1 == result.FileName1 && r.FileName2 == result.FileName2)
                .Average(r => r.SimilarityRate);

            var combinedResult = new FileComparisonResult(result.FileName1, result.FileName2, result.Metrics, combinedSimilarityRate, result.Source);
            unitedMetricsTable.AddComparisonResult(combinedResult);
        }

        return unitedMetricsTable;

    }

    private ComparisonResultsTable<FileComparisonResult> GetResultsWithMaxSimilarities(ComparisonResultsTable<FileComparisonResult> unitedMetricsTable)
    {
        var maxSimilaritiesTable = new ComparisonResultsTable<FileComparisonResult>();

        foreach (FileComparisonResult result in unitedMetricsTable)
        {
            if (maxSimilaritiesTable.Any(listedResults => listedResults.FileName1 == result.FileName1))
                continue;

            FileComparisonResult resultWithMaxSimilarity = unitedMetricsTable
                .Where(r => r.FileName1 == result.FileName1)
                .MaxBy(r => r.SimilarityRate);

            maxSimilaritiesTable.AddComparisonResult(resultWithMaxSimilarity);
        }

        return maxSimilaritiesTable;
    }
}