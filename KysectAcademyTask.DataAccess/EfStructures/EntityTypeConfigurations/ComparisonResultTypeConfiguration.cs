using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class ComparisonResultTypeConfiguration : IEntityTypeConfiguration<ComparisonResult>
{
    public void Configure(EntityTypeBuilder<ComparisonResult> builder)
    {
        builder.Property(c => c.SimilarityRate).IsRequired();

        builder.HasOne(c => c.Submit1Navigation)
            .WithMany(f => f.AsSubmit1ComparisonResults)
            .HasForeignKey(c => c.Submit1Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(c => c.Submit2Navigation)
            .WithMany(f => f.AsSubmit2ComparisonResults)
            .HasForeignKey(c => c.Submit2Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}