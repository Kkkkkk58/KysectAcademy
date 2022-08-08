using KysectAcademyTask.ComparisonResult;

namespace KysectAcademyTask.Report.Reporters;

public class ReporterFactory
{
    public IReporter<T> GetReporter<T>(ReportConfig config) where T : IComparisonResult
    {
        return config.Type switch
        {
            ReportType.Console => new ConsoleReporter<T>(),
            ReportType.Txt => new TxtReporter<T>(config.Path),
            ReportType.Json => new JsonReporter<T>(config.Path),
            _ => throw new NotImplementedException($"Not implemented realization of FactoryMethod for {config.Type}")
        };
    }
}