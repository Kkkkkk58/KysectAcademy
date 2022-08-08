using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace KysectAcademyTask.DataAccess.EfStructures;

public class SubmitComparisonDbContextFactory
{
    public SubmitComparisonDbContext GetDbContext(DataProvider dataProvider, string connectionString)
    {
        DbContextOptionsBuilder<SubmitComparisonDbContext> optionsBuilder = GetOptionsBuilder(dataProvider, connectionString);
        return new SubmitComparisonDbContext(optionsBuilder.Options);
    }

    private DbContextOptionsBuilder<SubmitComparisonDbContext> GetOptionsBuilder(DataProvider dataProvider, string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SubmitComparisonDbContext>();

        switch (dataProvider)
        {
            case DataProvider.MsSqlServer:
                optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
                break;
            case DataProvider.SqLite:
                optionsBuilder.UseSqlite(connectionString);
                break;
            case DataProvider.InMemory:
                optionsBuilder.UseInMemoryDatabase(connectionString);
                break;
            case DataProvider.Cosmos:
                optionsBuilder.UseCosmos(connectionString, "SubmitComparison");
                break;
            case DataProvider.PostgreSql:
                optionsBuilder.UseNpgsql(connectionString, options => options.EnableRetryOnFailure());
                break;
            case DataProvider.MySql:
                var connection = new MySqlConnection(connectionString);
                optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection), options => options.EnableRetryOnFailure());
                break;
            default:
                throw new NotImplementedException("Not implemented factory method for this provider");
        }

        optionsBuilder.EnableSensitiveDataLogging()
            .UseLazyLoadingProxies();

        return optionsBuilder;
    }
}