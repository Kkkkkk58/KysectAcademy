using KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;
using KysectAcademyTask.DataAccess.Models.Entities;

using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.EfStructures;

public sealed partial class FileComparisonDbContext : DbContext
{
    public FileComparisonDbContext(DbContextOptions<FileComparisonDbContext> options)
        : base(options)
    {
        Database.Migrate();

        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message}");
            throw new DbUpdateException(args.Exception.Message);
        };
    }

    public DbSet<ComparisonResult> ComparisonResults { get; set; } = null!;
    public DbSet<FileEntity> Files { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Models.Entities.Submit> Submits { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntities(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        new ComparisonResultTypeConfiguration().Configure(modelBuilder.Entity<ComparisonResult>());
        new FileEntityTypeConfiguration().Configure(modelBuilder.Entity<FileEntity>());
        new ComparisonResultFileTypeConfiguration().Configure(modelBuilder.Entity<ComparisonResultFile>());
        new GroupTypeConfiguration().Configure(modelBuilder.Entity<Group>());
        new StudentTypeConfiguration().Configure(modelBuilder.Entity<Student>());
        new SubmitTypeConfiguration().Configure(modelBuilder.Entity<Models.Entities.Submit>());
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}