using KysectAcademyTask.FileComparison;
using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        CompareFiles();
    }

    private static void CompareFiles()
    {
        try
        {
            AppSettingsConfig config = AppSettingsParser.GetInstance().Config;
            FileProcessor fileProcessor = new(config.FileGetterConfig);
            ComparisonAlgorithm.Metrics comparisonMetrics = config.Metrics;
            ComparisonResultsTable comparisonResultsTable = fileProcessor.GetComparisonResults(comparisonMetrics);
            string outputFileName = config.OutputFilePath;
            using StreamWriter writer = new(outputFileName);
            comparisonResultsTable.Write(writer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while performing file comparison: {e.Message}");
        }
    }
}