using KysectAcademyTask.DataAccess.EfStructures;

namespace KysectAcademyTask.DbInteraction.Configuration;

public struct DbConfig
{
    public IReadOnlyDictionary<string, string> ConnectionStrings { get; init; }
    public DataProvider DataProvider { get; init; }

    public DbConfig(IReadOnlyDictionary<string, string> connectionStrings, DataProvider dataProvider)
    {
        ConnectionStrings = connectionStrings;
        DataProvider = dataProvider;
    }
}