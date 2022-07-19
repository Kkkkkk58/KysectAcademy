using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using KysectAcademyTask.FileComparison;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Report.Reporters;

internal class JsonReporter : IReporter
{
    private readonly string _fileName;

    public JsonReporter(string? fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        _fileName = new ExtensionApplier().GetFileNameWithDesiredExtension(fileName, ".json");
    }

    public void MakeReport(ComparisonResultsTable results)
    {
        using var writer = new StreamWriter(_fileName);

        var options = new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
        string jsonOutput = JsonSerializer.Serialize(results, options);
        writer.WriteLine(jsonOutput);
    }
}