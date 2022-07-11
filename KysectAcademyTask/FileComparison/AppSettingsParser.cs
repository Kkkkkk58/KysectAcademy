using KysectAcademyTask.FileComparison.FileComparisonAlgorithms;
using Microsoft.Extensions.Configuration;

namespace KysectAcademyTask.FileComparison;

internal class AppSettingsParser
{
    private static readonly IConfigurationRoot Config;

    static AppSettingsParser()
    {
        Config = GetConfigurationRoot("appsettings.json");
    }

    public FileGetterConfig GetFileGetterConfig()
    {
        try
        {
            IConfigurationSection section = Config.GetSection(nameof(FileGetterConfig));
            FileGetterConfig fileGetterConfig = section.Get<FileGetterConfig>();
            return fileGetterConfig;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid FileGetterConfig argument: {e.Message}", e);
        }
    }

    public string GetOutputFile()
    {
        try
        {
            string? outputFile = Config.GetValue<string>("OutputFile");


            if (outputFile is null)
            {
                throw new ArgumentException("Output file was not provided");
            }

            return outputFile;
        }
        catch (InvalidOperationException e)
        {
            throw new ArgumentException($"Invalid OutputFile argument: {e.Message}", e);
        }
    }

    public ComparisonAlgorithm.Metrics GetComparisonMetrics()
    {
        try
        {
            ComparisonAlgorithm.Metrics? metrics = Config.GetValue<ComparisonAlgorithm.Metrics>("Metrics");
            if (metrics is null)
            {
                return ComparisonAlgorithm.Metrics.Jaccard;
            }

            return (ComparisonAlgorithm.Metrics)metrics;
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
}