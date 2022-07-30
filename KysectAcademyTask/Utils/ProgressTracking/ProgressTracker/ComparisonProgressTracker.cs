using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

namespace KysectAcademyTask.Utils.ProgressTracking.ProgressTracker;

internal class ComparisonProgressTracker : Base.ProgressTracker
{
    public ComparisonProgressTracker(IProgressBar progressBar, int totalWorkUnits, int completedWorkUnits = 0) : base(progressBar, totalWorkUnits, completedWorkUnits)
    {
    }
}