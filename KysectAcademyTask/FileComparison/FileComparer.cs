namespace KysectAcademyTask.FileComparison;

internal class FileComparer
{
    private readonly string _fileName1;
    private readonly string _fileName2;

    public FileComparer(string fileName1, string fileName2)
    {
        _fileName1 = fileName1;
        _fileName2 = fileName2;
    }

    public ComparisonResult Compare(string fileContent1, string fileContent2)
    {
        double similarityRate = new ComparisonAlgorithm().GetSimilarityRate(fileContent1, fileContent2);
        return new ComparisonResult(_fileName1, _fileName2, similarityRate);
    }

}