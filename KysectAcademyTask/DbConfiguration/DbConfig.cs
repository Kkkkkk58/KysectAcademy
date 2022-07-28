namespace KysectAcademyTask.DbConfiguration;

internal struct DbConfig
{
    public bool RecheckEnabled { get; init; } = false;
    public IReadOnlyDictionary<string, string> ConnectionStrings { get; init; }

    public DbConfig(IReadOnlyDictionary<string, string> connectionStrings, bool recheckEnabled = false)
    {
        ConnectionStrings = connectionStrings;
        RecheckEnabled = recheckEnabled;
    }
}