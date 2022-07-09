namespace KysectAcademyTask.FileComparison;

internal class FileComparer
{
    public ComparisonResult Compare(string fileName1, string fileName2)
    {
        return new ComparisonResult(fileName1, fileName2, 98);
    }
}