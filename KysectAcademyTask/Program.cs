using KysectAcademyTask.AppSettings;

namespace KysectAcademyTask;

public class Program
{
    public static void Main()
    {
        AppSettingsConfig config = new AppSettingsParser().Config;
        var app = new SubmitComparisonApp(config);
        app.Run();
    }
}