using KysectAcademyTask.ComparisonResult;

namespace KysectAcademyTask.Report.Reporters;

public class ConsoleReporter<T> : IReporter<T> where T : IComparisonResult
{
    public void MakeReport(ComparisonResultsTable<T> results)
    {
        foreach (T result in results)
        {
            Console.WriteLine(result.ToString());
            Console.WriteLine();
        }
    }
}