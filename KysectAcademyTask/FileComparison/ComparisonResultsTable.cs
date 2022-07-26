using System.Collections;

namespace KysectAcademyTask.FileComparison;

internal class ComparisonResultsTable : IReadOnlyCollection<ComparisonResult>
{
    private readonly List<ComparisonResult> _comparisonResults;
    public int Count => _comparisonResults.Count;

    public ComparisonResultsTable()
    {
        _comparisonResults = new List<ComparisonResult>();
    }

    public void AddComparisonResult(ComparisonResult result)
    {
        _comparisonResults.Add(result);
    }

    public void AddTable(ComparisonResultsTable other)
    {
        _comparisonResults.AddRange(other._comparisonResults);
    }

    public IEnumerator<ComparisonResult> GetEnumerator()
    {
        return _comparisonResults.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_comparisonResults).GetEnumerator();
    }
}