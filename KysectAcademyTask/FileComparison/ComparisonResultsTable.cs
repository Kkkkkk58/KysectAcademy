namespace KysectAcademyTask.FileComparison;

internal class ComparisonResultsTable
{
    private readonly List<ComparisonResult> _comparisonResults;

    public ComparisonResultsTable()
    {
        _comparisonResults = new List<ComparisonResult>();
    }

    public void AddComparisonResult(ComparisonResult result)
    {
        _comparisonResults.Add(result);
    }

    public void Write(StreamWriter writer)
    {
        foreach (ComparisonResult comparisonResult in _comparisonResults)
        {
            writer.WriteLine(comparisonResult.ToString());
            writer.WriteLine();
        }
    }
}