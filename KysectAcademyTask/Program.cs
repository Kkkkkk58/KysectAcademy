using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        FileProcessor fileProcessor = new();
        ComparisonResultsTable comparisonResultsTable = fileProcessor.GetComparisonResults();
        string outputFileName = new AppSettingsParser().GetOutputDir(); 
        using StreamWriter writer = new(outputFileName);
        comparisonResultsTable.Write(writer);
    }
}