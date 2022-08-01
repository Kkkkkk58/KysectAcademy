using KysectAcademyTask.FileComparison;

namespace KysectAcademyTask.Report.Reporters;

public class ConsoleReporter : IReporter
{
    public void MakeReport(ComparisonResultsTable results)
    {
        foreach (ComparisonResult result in results)
        {
            Console.WriteLine(result.ToString());
            Console.WriteLine();
        }
    }
}