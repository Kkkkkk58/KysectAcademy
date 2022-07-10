using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        try
        {
            FileProcessor fileProcessor = new();
            ComparisonAlgorithm.Metrics comparisonMetrics = new AppSettingsParser().GetComparisonMetrics();
            ComparisonResultsTable comparisonResultsTable = fileProcessor.GetComparisonResults(comparisonMetrics);
            string outputFileName = new AppSettingsParser().GetOutputFile();
            using StreamWriter writer = new(outputFileName);
            comparisonResultsTable.Write(writer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while performing: {e.Message}");
        }
    }
}