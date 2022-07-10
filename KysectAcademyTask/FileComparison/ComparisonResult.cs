namespace KysectAcademyTask.FileComparison;

internal readonly struct ComparisonResult
{
    private string FileName1 { get; }
    private string FileName2 { get; }
    private double SimilarityRate { get; }

    public ComparisonResult(string fileName1, string fileName2, double similarityRate)
    {
        FileName1 = fileName1;
        FileName2 = fileName2;
        SimilarityRate = similarityRate;
    }

    public override string ToString()
    {
        return $"|{FileName1}\n|{FileName2}\n=> {SimilarityRate:0.##}";
    }
}