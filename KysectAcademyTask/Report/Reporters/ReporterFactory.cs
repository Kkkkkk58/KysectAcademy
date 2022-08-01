namespace KysectAcademyTask.Report.Reporters;

public class ReporterFactory
{
    public IReporter GetReporter(ReportConfig config)
    {
        return config.Type switch
        {
            ReportType.Console => new ConsoleReporter(),
            ReportType.Txt => new TxtReporter(config.Path),
            ReportType.Json => new JsonReporter(config.Path),
            _ => throw new NotImplementedException($"Not implemented realization of FactoryMethod for {config.Type}")
        };
    }
}