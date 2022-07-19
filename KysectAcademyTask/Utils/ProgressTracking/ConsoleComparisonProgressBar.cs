namespace KysectAcademyTask.Utils.ProgressTracking;

internal class ConsoleComparisonProgressBar : IProgressBar
{
    public void Update(int workToDo, int workDone, int percentage)
    {
        Console.Clear();
        Console.Write($"\t\t\t{percentage,3}% done. Compared {workDone} / {workToDo} submits\n");
        Draw(percentage);
    }

    private void Draw(int percentage)
    {
        Console.CursorLeft = 0;
        Console.Write("[");
        Console.CursorLeft = 101;
        Console.Write("]");
        Console.CursorLeft = 1;
        for (int i = 0; i < percentage; ++i)
        {
            Console.Write("#");
        }
    }
}