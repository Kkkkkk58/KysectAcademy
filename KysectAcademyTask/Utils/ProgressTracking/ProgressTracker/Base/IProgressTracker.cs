namespace KysectAcademyTask.Utils.ProgressTracking.ProgressTracker.Base;

internal interface IProgressTracker
{
    public int CompletedWorkUnits { get; }
    public int ProgressInPercents { get; }

    public void IncreaseProgress();
}