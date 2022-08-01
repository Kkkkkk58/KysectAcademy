using KysectAcademyTask.AppSettings;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        AppSettingsConfig config = AppSettingsParser.GetInstance().Config;
        var app = new SubmitComparisonApp(config);
        app.Run();
    }
}