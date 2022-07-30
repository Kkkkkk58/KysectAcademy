namespace KysectAcademyTask.DbInteraction.Configuration;

internal struct DbConfig
{
    public IReadOnlyDictionary<string, string> ConnectionStrings { get; init; }

    public DbConfig(IReadOnlyDictionary<string, string> connectionStrings)
    {
        ConnectionStrings = connectionStrings;
    }
}