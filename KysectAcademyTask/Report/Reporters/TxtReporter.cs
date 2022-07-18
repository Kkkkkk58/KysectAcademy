using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Report.Reporters;

internal class TxtReporter : IReporter
{
    private readonly string _fileName;
    public TxtReporter(string? fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }
        _fileName = new ExtensionApplier().GetFileNameWithDesiredExtension(fileName, ".txt");
    }

    public void MakeReport(ComparisonResultsTable results)
    {
        using StreamWriter writer = new(_fileName);
        foreach (ComparisonResult result in results)
        {
            writer.WriteLine(result.ToString());
            writer.WriteLine();
        }
    }
}