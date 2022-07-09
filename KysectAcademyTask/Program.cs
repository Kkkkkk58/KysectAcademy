using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        FileProcessor fileProcessor = new();
        ComparisonResultsTable comparisonResultsTable = fileProcessor.GetComparisonResults();
        comparisonResultsTable.Show();
    }
}