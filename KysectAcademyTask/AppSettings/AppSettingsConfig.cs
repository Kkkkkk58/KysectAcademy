using KysectAcademyTask.DbInteraction.Configuration;
using KysectAcademyTask.Report;
using KysectAcademyTask.SubmitComparison;
using KysectAcademyTask.Utils.ProgressTracking.ProgressBar;

namespace KysectAcademyTask.AppSettings;

public struct AppSettingsConfig
{
    public SubmitConfig SubmitConfig { get; init; }
    public ReportConfig ReportConfig { get; init; }
    public DbConfig DbConfig { get; init; }
    public ProgressBarConfig ProgressBarConfig { get; init; }

    public AppSettingsConfig(SubmitConfig submitConfig, ReportConfig reportConfig, DbConfig dbConfig,
        ProgressBarConfig progressBarConfig)
    {
        SubmitConfig = submitConfig;
        ReportConfig = reportConfig;
        DbConfig = dbConfig;
        ProgressBarConfig = progressBarConfig;
    }
}