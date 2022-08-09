using KysectAcademyTask.Submit;

namespace KysectAcademyTask.ComparisonResult;

public readonly struct SubmitComparisonResult : IComparisonResult
{
    public SubmitInfo SubmitInfo1 { get; }
    public SubmitInfo SubmitInfo2 { get; }
    public double SimilarityRate { get; }
    public ResultSource Source { get; }

    public SubmitComparisonResult(SubmitInfo submitInfo1, SubmitInfo submitInfo2, double similarityRate,
        ResultSource source = ResultSource.NewFileComparison)
    {
        SubmitInfo1 = submitInfo1;
        SubmitInfo2 = submitInfo2;
        SimilarityRate = similarityRate;
        Source = source;
    }


    public override string ToString()
    {
        return $"|{SubmitInfo1}\n|{SubmitInfo2}\n* Received from {Source}\n=> {SimilarityRate:0.##}";
    }
}