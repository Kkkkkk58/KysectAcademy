using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using Microsoft.Extensions.Configuration;

namespace KysectAcademyTask.FileComparison;

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

    private AppSettingsConfig GetConfig()
    {
        FileGetterConfig? fileGetterConfig = GetFileGetterConfig();
        string? outputFilePath = GetOutputFile();
        ComparisonAlgorithm.Metrics? metrics = GetComparisonMetrics();
        return new AppSettingsConfig(fileGetterConfig, outputFilePath, metrics);
    }

    private FileGetterConfig? GetFileGetterConfig()
    {
        ValidateConfigRoot();
        try
        {
            IConfigurationSection section = 
                _configRoot.GetSection(nameof(FileGetterConfig));
            FileGetterConfig? fileGetterConfig = section.Get<FileGetterConfig>();
            return fileGetterConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid FileGetterConfig argument: {e.Message}", e);
        }
    }

    private string? GetOutputFile()
    {
        ValidateConfigRoot();
        try
        {
            string? outputFile = _configRoot.GetValue<string>("OutputFile");
            return outputFile;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid OutputFile argument: {e.Message}", e);
        }
    }

    private ComparisonAlgorithm.Metrics? GetComparisonMetrics()
    {
        ValidateConfigRoot();
        try
        {
            ComparisonAlgorithm.Metrics? metrics = _configRoot.GetValue<ComparisonAlgorithm.Metrics>("Metrics");
            return metrics;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Metrics argument: {e.Message}", e);
        }
    }

    private static IConfigurationRoot GetConfigurationRoot(string jsonFileName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(jsonFileName).Build();
    }

    private void ValidateConfigRoot()
    {
        if (_configRoot is null)
        {
            throw new InvalidOperationException("Config root was not set");
        }
    }
}