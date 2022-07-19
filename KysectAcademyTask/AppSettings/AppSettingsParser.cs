using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;
using KysectAcademyTask.Submit.SubmitFilters;
using Microsoft.Extensions.Configuration;

namespace KysectAcademyTask.AppSettings;

internal class AppSettingsParser
{
    private static AppSettingsParser? _instance;
    private static readonly object Lock = new();

    private readonly IConfigurationRoot _configRoot;

    public AppSettingsConfig Config { get; }

    public static AppSettingsParser GetInstance()
    {
        if (_instance is null)
        {
            lock (Lock)
            {
                _instance ??= new AppSettingsParser();
            }
        }

        return _instance;
    }

    private AppSettingsParser()
    {
        _configRoot = GetConfigurationRoot("appsettings.json");
        Config = GetConfig();
    }

    private IConfigurationRoot GetConfigurationRoot(string jsonFileName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(jsonFileName).Build();
    }

    private AppSettingsConfig GetConfig()
    {
        string? inputDirectory = GetInputDirectory();
        Filters? filters = GetFilters();
        IReadOnlyList<ComparisonAlgorithm.Metrics>? metrics = GetComparisonMetrics();
        ReportConfig? reportConfig = GetReportConfig();
        return new AppSettingsConfig(inputDirectory, filters, metrics, reportConfig);
    }

    private string? GetInputDirectory()
    {
        try
        {
            string? outputFile = _configRoot.GetValue<string>("InputDirectory");
            return outputFile;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid InputDirectory argument: {e.Message}", e);
        }
    }

    private Filters? GetFilters()
    {
        try
        {
            IConfigurationSection section =
                _configRoot.GetSection(nameof(Filters));
            Filters? fileGetterConfig = section.Get<Filters>();
            return fileGetterConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Filters argument: {e.Message}", e);
        }
    }

    private IReadOnlyList<ComparisonAlgorithm.Metrics>? GetComparisonMetrics()
    {
        try
        {
            IReadOnlyList<ComparisonAlgorithm.Metrics>? metrics =
                _configRoot.GetValue<IReadOnlyList<ComparisonAlgorithm.Metrics>?>("Metrics");
            return metrics;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Metrics argument: {e.Message}", e);
        }
    }

    private ReportConfig? GetReportConfig()
    {
        try
        {
            IConfigurationSection section =
                _configRoot.GetSection(nameof(ReportConfig));
            ReportConfig? fileGetterConfig = section.Get<ReportConfig>();
            return fileGetterConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Report argument: {e.Message}", e);
        }
    }
}