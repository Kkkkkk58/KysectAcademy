namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar;

internal class ConsoleComparisonProgressBar : Base.ConsoleProgressBar
{
    public ConsoleComparisonProgressBar() : base("\t\t\t{0,3}% done. Compared {1} / {2} pairs of submits\n")
    {
    }
}