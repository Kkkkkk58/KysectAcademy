namespace KysectAcademyTask.Utils.ProgressTracking.ProgressTracker.Base;

public interface IProgressTracker
{
    public int CompletedWorkUnits { get; }
    public int ProgressInPercents { get; }

    public void IncreaseProgress();
}