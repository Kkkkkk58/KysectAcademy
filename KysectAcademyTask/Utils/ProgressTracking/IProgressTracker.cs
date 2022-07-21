namespace KysectAcademyTask.Utils.ProgressTracking;

internal interface IProgressTracker
{
    public int CompletedWorkUnits { get; }
    public int ProgressInPercents { get; }

    public void IncreaseProgress();
}