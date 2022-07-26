namespace KysectAcademyTask.Utils.ProgressTracking;

internal class ConsoleComparisonProgressBar : IProgressBar
{
    public void Update(int workToDo, int workDone, int percentage)
    {
        Console.Clear();
        Console.Write($"\t\t\t{percentage,3}% done. Compared {workDone} / {workToDo} pairs of submits\n");
        Draw(percentage);
    }

    private void Draw(int percentage)
    {
        var pbSetup = new ConsoleProgressBarSetup('[', ']', '#', 0, 101, 1);
        Console.CursorLeft = pbSetup.LeftCursorPos;
        Console.Write(pbSetup.LeftBorderSymbol);
        Console.CursorLeft = pbSetup.RightCursorPos;
        Console.Write(pbSetup.RightBorderSymbol);
        Console.CursorLeft = pbSetup.BeginningCursorPos;
        for (int i = 0; i < percentage; ++i)
        {
            Console.Write(pbSetup.ProgressSymbol);
        }
    }
}