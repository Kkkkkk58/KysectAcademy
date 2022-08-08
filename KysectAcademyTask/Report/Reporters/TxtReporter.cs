using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Report.Reporters;

public class TxtReporter<T> : IReporter<T> where T : IComparisonResult
{
    private readonly string _fileName;

    public TxtReporter(string fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        _fileName = new ExtensionApplier().GetFileNameWithDesiredExtension(fileName, ".txt");
    }

    public void MakeReport(ComparisonResultsTable<T> results)
    {
        using StreamWriter writer = new(_fileName);
        foreach (T result in results)
        {
            writer.WriteLine(result.ToString());
            writer.WriteLine();
        }
    }
}