namespace KysectAcademyTask.Utils.ProgressTracking.ProgressBar.ConsoleProgressBar.Base;

internal readonly struct ConsoleProgressBarSetup
{
    public char LeftBorderSymbol { get; }
    public char RightBorderSymbol { get; }
    public char ProgressSymbol { get; }
    public int LeftCursorPos { get; }
    public int RightCursorPos { get; }
    public int BeginningCursorPos { get; }

    public ConsoleProgressBarSetup(char leftBorderSymbol, char rightBorderSymbol, char progressSymbol,
        int leftCursorPos, int rightCursorPos, int beginningCursorPos)
    {
        LeftBorderSymbol = leftBorderSymbol;
        RightBorderSymbol = rightBorderSymbol;
        ProgressSymbol = progressSymbol;
        LeftCursorPos = leftCursorPos;
        RightCursorPos = rightCursorPos;
        BeginningCursorPos = beginningCursorPos;
    }
}