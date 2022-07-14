using KysectAcademyTask.FileComparison;

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
            ComparisonResultsTable comparisonResultsTable = fileProcessor.GetComparisonResults(config.Metrics);
            using StreamWriter writer = new(config.OutputFilePath);
            comparisonResultsTable.Write(writer);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error occurred while performing file comparison: {e.Message}");
        }
    }
}