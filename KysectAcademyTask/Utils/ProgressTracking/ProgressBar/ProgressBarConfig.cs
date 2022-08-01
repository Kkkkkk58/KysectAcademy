namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar;

public readonly struct ProgressBarConfig
{
    public bool IsEnabled { get; init; }

    public ProgressBarConfig(bool isEnabled)
    {
        IsEnabled = isEnabled;
    }
}