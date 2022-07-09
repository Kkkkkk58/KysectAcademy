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
        IConfigurationSection section = _config.GetSection(nameof(FileGetterConfig));
        FileGetterConfig fileGetterConfig = section.Get<FileGetterConfig>();

        return fileGetterConfig;
    }

    public string GetOutputDir()
    {
        string? outputDir  = _config.GetValue<string>("OutputFile");
        if (outputDir == null)
        {
            throw new ArgumentException("Output file was not provided");
        }

        return outputDir;
    }

    private static IConfigurationRoot GetConfigurationRoot(string jsonFileName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(jsonFileName).Build();
    }
}