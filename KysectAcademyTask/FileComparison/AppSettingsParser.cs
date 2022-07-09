using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using Microsoft.Extensions.Configuration;

namespace KysectAcademyTask.FileComparison;

internal class AppSettingsParser
{
    private static IConfigurationRoot _config;

    static AppSettingsParser()
    {
        _config = GetConfigurationRoot("appsettings.json");
    }

    public FileGetterConfig GetFileGetterConfig()
    {
        try
        {
            IConfigurationSection section = _config.GetSection(nameof(FileGetterConfig));
            FileGetterConfig fileGetterConfig = section.Get<FileGetterConfig>();
            return fileGetterConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid FileGetterConfig argument: {e.Message}");
        }
    }

    public string GetOutputFile()
    {
        try
        {
            string? outputFile = _config.GetValue<string>("OutputFile");


            if (outputFile == null)
            {
                throw new ArgumentException("Output file was not provided");
            }

            return outputFile;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid OutputFile argument: {e.Message}");
        }
    }

    public ComparisonAlgorithm.Metrics GetComparisonMetrics()
    {
        try
        {
            ComparisonAlgorithm.Metrics? metrics = _config.GetValue<ComparisonAlgorithm.Metrics>("Metrics");
            if (metrics is null)
            {
                return ComparisonAlgorithm.Metrics.Jaccard;
            }

            return (ComparisonAlgorithm.Metrics)metrics;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid Metrics argument: {e.Message}");
        }
    }

    private static IConfigurationRoot GetConfigurationRoot(string jsonFileName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(jsonFileName).Build();
    }
}