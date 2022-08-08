using KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;
using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace KysectAcademyTask.DataAccess.EfStructures;

public sealed partial class SubmitComparisonDbContext : DbContext
{
    public SubmitComparisonDbContext(DbContextOptions<SubmitComparisonDbContext> options)
        : base(options)
    {
        Database.EnsureCreated();

        SaveChangesFailed += (sender, args) =>
        {
            Console.WriteLine($"An exception occurred! {args.Exception.Message}");
            throw new DbUpdateException(args.Exception.Message);
        };
    }

    public DbSet<ComparisonResult> ComparisonResults { get; set; } = null!;
    public DbSet<Group> Groups { get; set; } = null!;
    public DbSet<Student> Students { get; set; } = null!;
    public DbSet<Models.Entities.Submit> Submits { get; set; } = null!;
    public DbSet<HomeWork> HomeWorks { get; set; } = null!;


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ConfigureEntities(modelBuilder);

        OnModelCreatingPartial(modelBuilder);
    }

    private void ConfigureEntities(ModelBuilder modelBuilder)
    {
        new ComparisonResultTypeConfiguration().Configure(modelBuilder.Entity<ComparisonResult>());
        new GroupTypeConfiguration().Configure(modelBuilder.Entity<Group>());
        new HomeWorkTypeConfiguration().Configure(modelBuilder.Entity<HomeWork>());
        new StudentTypeConfiguration().Configure(modelBuilder.Entity<Student>());
        new SubmitTypeConfiguration().Configure(modelBuilder.Entity<Models.Entities.Submit>());
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}