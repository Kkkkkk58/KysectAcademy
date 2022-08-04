using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KysectAcademyTask.DataAccess.EfStructures;

internal class DesignTimeFileComparisonDbContextFactory : IDesignTimeDbContextFactory<FileComparisonDbContext>
{
    public FileComparisonDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FileComparisonDbContext>();
        const string connectionString = @"Server=localhost;Database=SubmitComparison;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        Console.WriteLine(connectionString);

        optionsBuilder.EnableSensitiveDataLogging();

        //switch (dataProvider)
        //{
        //    case DataProvider.MsSqlServer:
        //        optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure().MigrationsAssembly("KysectAcademyTask.DataAccess.MsSqlServerMigrations"));
        //        break;
        //    case DataProvider.SqLite:
        //        optionsBuilder.UseSqlite(connectionString, options => options.MigrationsAssembly("KysectAcademyTask.DataAccess.SqLiteMigrations"));
        //        break;
        //    case DataProvider.InMemory:
        //        optionsBuilder.UseInMemoryDatabase(connectionString);
        //        break;
        //    case DataProvider.Cosmos:
        //        optionsBuilder.UseCosmos(connectionString, "SubmitComparison");
        //        break;
        //    case DataProvider.PostgreSql:
        //        optionsBuilder.UseNpgsql(connectionString, options => options.EnableRetryOnFailure().MigrationsAssembly("KysectAcademyTask.DataAccess.PostgreSqlMigrations"));
        //        break;
        //    case DataProvider.MySql:
        //        var connection = new MySqlConnection(connectionString);
        //        optionsBuilder.UseMySql(connection, ServerVersion.AutoDetect(connection), options => options.EnableRetryOnFailure().MigrationsAssembly("KysectAcademyTask.DataAccess.MySqlMigrations"));
        //        break;
        //    default:
        //        throw new NotImplementedException("Not implemented factory method for this provider");
        //}

        return new FileComparisonDbContext(optionsBuilder.Options);
    }
}