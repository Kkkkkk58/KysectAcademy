using System.Collections;

namespace KysectAcademyTask.ComparisonResult;

public class ComparisonResultsTable<T> : IReadOnlyCollection<T>
    where T : IComparisonResult
{
    private readonly List<T> _comparisonResults;
    public int Count => _comparisonResults.Count;

    public ComparisonResultsTable()
    {
        _comparisonResults = new List<T>();
    }

    public void AddComparisonResult(T result)
    {
        _comparisonResults.Add(result);
    }

    public void AddTable(ComparisonResultsTable<T> other)
    {
        _comparisonResults.AddRange(other._comparisonResults);
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _comparisonResults.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable)_comparisonResults).GetEnumerator();
    }
}