using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class ComparisonResultTypeConfiguration : IEntityTypeConfiguration<ComparisonResult>
{
    public void Configure(EntityTypeBuilder<ComparisonResult> builder)
    {
        builder.Property(c => c.Metrics).IsRequired();
        builder.Property(c => c.SimilarityRate).IsRequired();

        builder.HasOne(c => c.File1Navigation)
            .WithMany(f => f.AsFile1ComparisonResults)
            .HasForeignKey(c => c.File1Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(c => c.File2Navigation)
            .WithMany(f => f.AsFile2ComparisonResults)
            .HasForeignKey(c => c.File2Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}