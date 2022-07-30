using KysectAcademyTask.Utils.ProgressTracking.ProgressBar.Base;

namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar.Base;

public class ConsoleProgressBar : IProgressBar
{
    private readonly string _displayMessage;

    public ConsoleProgressBar(string displayMessage)
    {
        _displayMessage = displayMessage;
    }

    public void Update(int workToDo, int workDone, int percentage)
    {
        Console.Clear();
        Console.Write(_displayMessage, percentage, workDone, workToDo);
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