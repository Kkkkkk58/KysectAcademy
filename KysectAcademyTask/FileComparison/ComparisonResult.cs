using System.Globalization;

namespace KysectAcademyTask.FileComparison;

public struct ComparisonResult
{
    public string FileName1 { get; }
    public string FileName2 { get; }
    public double SimilarityRate { get; }

    public ComparisonResult(string fileName1, string fileName2, double similarityRate)
    {
        FileName1 = fileName1;
        FileName2 = fileName2;
        SimilarityRate = similarityRate;
    }

    public override string ToString()
    {
        return $"{FileName1} is {SimilarityRate.ToString(CultureInfo.InvariantCulture)} similar to {FileName2}";
    }
}