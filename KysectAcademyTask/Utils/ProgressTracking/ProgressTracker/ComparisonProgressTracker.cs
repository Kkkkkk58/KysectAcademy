using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

namespace KysectAcademyTask.Utils.ProgressTracking.ProgressTracker;

public class ComparisonProgressTracker : Base.ProgressTracker
{
    public ComparisonProgressTracker(IProgressBar progressBar, int totalWorkUnits, int completedWorkUnits = 0)
        : base(progressBar, totalWorkUnits, completedWorkUnits)
    {
    }
}