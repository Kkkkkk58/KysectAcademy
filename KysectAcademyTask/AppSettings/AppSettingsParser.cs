using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using KysectAcademyTask.Report;
using KysectAcademyTask.Submit.SubmitFilters;
using KysectAcademyTask.SubmitComparison;
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
        string inputDirectory = GetInputDirectory();
        Filters? filters = GetFilters();
        IReadOnlyList<ComparisonAlgorithm.Metrics> metrics = GetComparisonMetrics();
        string submitTimeFormat = GetSubmitTimeFormat();
        int submitDirDepth = GetSubmitDirDepth();
        SubmitConfig submitConfig = new(inputDirectory, filters, metrics, submitTimeFormat, submitDirDepth);
        ReportConfig reportConfig = GetReportConfig();
        return new AppSettingsConfig(submitConfig, reportConfig);
    }

    private string GetInputDirectory()
    {
        try
        {
            string? inputDirectory = _configRoot.GetValue<string>("InputDirectory");
            if (inputDirectory is null)
            {
                throw new ArgumentNullException(nameof(inputDirectory));
            }

            return inputDirectory;
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
            Filters? filtersConfig = section.Get<Filters>();
            return filtersConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Filters argument: {e.Message}", e);
        }
    }

    private IReadOnlyList<ComparisonAlgorithm.Metrics> GetComparisonMetrics()
    {
        try
        {
            IReadOnlyList<ComparisonAlgorithm.Metrics> metrics =
                _configRoot.GetValue<IReadOnlyList<ComparisonAlgorithm.Metrics>?>("Metrics")
                ?? new List<ComparisonAlgorithm.Metrics> { ComparisonAlgorithm.Metrics.Jaccard };
            return metrics;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Metrics argument: {e.Message}", e);
        }
    }

    private string GetSubmitTimeFormat()
    {
        try
        {
            string submitTimeFormat = _configRoot.GetValue<string>("SubmitTimeFormat")
                                      ?? "yyyyMMddHHmmss";
            return submitTimeFormat;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid SubmitTimeFormat argument: {e.Message}", e);
        }
    }

    private int GetSubmitDirDepth()
    {
        try
        {
            int submitDirDepth = _configRoot.GetValue<int?>("SubmitDirDepth") ?? 5;
            return submitDirDepth;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid SubmitDirDepth argument: {e.Message}", e);
        }
    }

    private ReportConfig GetReportConfig()
    {
        try
        {
            IConfigurationSection section =
                _configRoot.GetSection(nameof(ReportConfig));
            ReportConfig reportConfig = section.Get<ReportConfig?>()
                                        ?? new ReportConfig(ReportType.Console);
            return reportConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid ReportConfig argument: {e.Message}", e);
        }
    }
}