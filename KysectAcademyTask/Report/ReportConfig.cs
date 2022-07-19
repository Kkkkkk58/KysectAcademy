namespace KysectAcademyTask.Report;

public struct ReportConfig
{
    public ReportType Type { get; init; }
    public string? Path { get; init; }

    public ReportConfig(ReportType type, string? path = null)
    {
        Type = type;
        Path = path;
    }
}