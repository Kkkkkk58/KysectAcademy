using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace KysectAcademyTask.DataAccess.EfStructures;

internal class FileComparisonDbContextFactory : IDesignTimeDbContextFactory<FileComparisonDbContext>
{
    public FileComparisonDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<FileComparisonDbContext>();
        const string connectionString = @"Server=localhost;Database=FileComparison;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(connectionString, options => options.EnableRetryOnFailure());
        Console.WriteLine(connectionString);
        return new FileComparisonDbContext(optionsBuilder.Options);
    }
}