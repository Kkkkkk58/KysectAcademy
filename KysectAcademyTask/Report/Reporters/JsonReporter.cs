using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Unicode;
using KysectAcademyTask.ComparisonResult;
using KysectAcademyTask.Utils;

namespace KysectAcademyTask.Report.Reporters;

public class JsonReporter<T> : IReporter<T> where T : IComparisonResult
{
    private readonly string _fileName;

    public JsonReporter(string fileName)
    {
        if (fileName is null)
        {
            throw new ArgumentNullException(nameof(fileName));
        }

        _fileName = new ExtensionApplier().GetFileNameWithDesiredExtension(fileName, ".json");
    }

    public void MakeReport(ComparisonResultsTable<T> results)
    {
        using var writer = new StreamWriter(_fileName);
        JsonSerializerOptions options = GetSerializerOptions();
        string jsonOutput = JsonSerializer.Serialize(results, options);
        writer.WriteLine(jsonOutput);
    }

    private static JsonSerializerOptions GetSerializerOptions()
    {
        return new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
            WriteIndented = true,
            Converters =
            {
                new JsonStringEnumConverter()
            }
        };
    }
}