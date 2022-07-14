namespace KysectAcademyTask.FileComparison;

internal readonly struct ComparisonResult
{
    private readonly string _fileName1;
    private readonly string _fileName2;
    private readonly double _similarityRate;

    public ComparisonResult(string fileName1, string fileName2, double similarityRate)
    {
        _fileName1 = fileName1;
        _fileName2 = fileName2;
        _similarityRate = similarityRate;
    }

    public override string ToString()
    {
        return $"|{_fileName1}\n|{_fileName2}\n=> {_similarityRate:0.##}";
    }
}