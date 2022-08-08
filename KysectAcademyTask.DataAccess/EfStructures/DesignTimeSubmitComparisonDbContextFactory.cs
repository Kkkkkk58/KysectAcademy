using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KysectAcademyTask.DataAccess.EfStructures;

internal class DesignTimeSubmitComparisonDbContextFactory : IDesignTimeDbContextFactory<SubmitComparisonDbContext>
{
    public SubmitComparisonDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SubmitComparisonDbContext>();
        const string connectionString = @"Server=localhost;Database=SubmitComparison;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        Console.WriteLine(connectionString);

        optionsBuilder.EnableSensitiveDataLogging();

        return new SubmitComparisonDbContext(optionsBuilder.Options);
    }
}