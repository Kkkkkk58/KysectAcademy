using Microsoft.EntityFrameworkCore;
using MySqlConnector;

namespace KysectAcademyTask.DataAccess.EfStructures;

public class FileComparisonDbContextFactory
{
    public FileComparisonDbContext GetDbContext(DataProvider dataProvider, string connectionString)
    {
        DbContextOptionsBuilder<FileComparisonDbContext> optionsBuilder = GetOptionsBuilder(dataProvider, connectionString);
        return new FileComparisonDbContext(optionsBuilder.Options);
    }

    private DbContextOptionsBuilder<FileComparisonDbContext> GetOptionsBuilder(DataProvider dataProvider, string connectionString)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FileComparisonDbContext>();
        optionsBuilder.EnableSensitiveDataLogging();

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

        return optionsBuilder;
    }
}