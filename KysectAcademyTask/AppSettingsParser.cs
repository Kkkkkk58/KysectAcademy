using Microsoft.Extensions.Configuration;

namespace KysectAcademyTask;

internal class AppSettingsParser
{
    public FileGetterConfig GetConfig()
    {
        IConfigurationRoot config = GetConfigurationRoot("appsettings.json");
        IConfigurationSection section = config.GetSection(nameof(FileGetterConfig));
        FileGetterConfig fileGetterConfig = section.Get<FileGetterConfig>();

        return fileGetterConfig;
    }

    private IConfigurationRoot GetConfigurationRoot(string jsonFileName)
    {
        return new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile(jsonFileName).Build();
    } 
}