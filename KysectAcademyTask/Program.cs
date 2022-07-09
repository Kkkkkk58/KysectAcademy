namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        FileProcessor fileProcessor = new();
        fileProcessor.CompareFiles();
        //new ComparisonResultsTable(fileProcessor).Show();
    }
}