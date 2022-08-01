using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

namespace KysectAcademyTask.Utils.ProgressTracking.ProgressTracker.Base;

public class ProgressTracker : IProgressTracker
{
    private readonly int _totalWorkUnits;
    public int CompletedWorkUnits { get; private set; }
    public int ProgressInPercents { get => GetPercentage(); }
    private readonly IProgressBar _progressBar;

    public ProgressTracker(IProgressBar progressBar, int totalWorkUnits, int completedWorkUnits = 0)
    {
        _progressBar = progressBar;
        _totalWorkUnits = totalWorkUnits;
        CompletedWorkUnits = completedWorkUnits;
    }

    public void IncreaseProgress()
    {
        ++CompletedWorkUnits;
        _progressBar.Update(_totalWorkUnits, CompletedWorkUnits, ProgressInPercents);
    }

    private int GetPercentage()
    {
        double percentageDouble = (double)CompletedWorkUnits / _totalWorkUnits * 100.0;
        int percentageInt = (int)Math.Round(percentageDouble, 0);
        return percentageInt;
    }
}