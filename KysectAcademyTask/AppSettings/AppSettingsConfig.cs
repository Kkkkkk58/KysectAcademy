using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.Report;
using KysectAcademyTask.SubmitComparison;

namespace KysectAcademyTask.AppSettings;

internal struct AppSettingsConfig
{
    public SubmitConfig SubmitConfig { get; init; }
    public ReportConfig ReportConfig { get; init; }
    public DbConfig DbConfig { get; init; }

    public AppSettingsConfig(SubmitConfig submitConfig, ReportConfig reportConfig, DbConfig dbConfig)
    {
        SubmitConfig = submitConfig;
        ReportConfig = reportConfig;
        DbConfig = dbConfig;
    }
}