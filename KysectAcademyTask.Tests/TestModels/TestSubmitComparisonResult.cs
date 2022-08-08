using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.Tests.TestModels;

public struct TestSubmitComparisonResult
{
    public TestSubmitInfo SubmitInfo1 { get; set; }
    public TestSubmitInfo SubmitInfo2 { get; set; }
    public double SimilarityRate { get; set; }
    public ResultSource Source { get; set; }

    public TestSubmitComparisonResult(TestSubmitInfo submitInfo1, TestSubmitInfo submitInfo2, double similarityRate, ResultSource source)
    {
        SubmitInfo1 = submitInfo1;
        SubmitInfo2 = submitInfo2;
        SimilarityRate = similarityRate;
        Source = source;    
    }
}