using KysectAcademyTask.DataAccess.EfStructures;

namespace KysectAcademyTask.DbInteraction.Configuration;

public struct DbConfig
{
    public IReadOnlyDictionary<string, string> ConnectionStrings { get; init; }
    public DataProvider? DataProvider { get; init; }
    public bool Recheck { get; set; }

    public DbConfig(IReadOnlyDictionary<string, string> connectionStrings, DataProvider? dataProvider,
        bool recheck = false)
    {
        ConnectionStrings = connectionStrings;
        DataProvider = dataProvider;
        Recheck = recheck;
    }
}