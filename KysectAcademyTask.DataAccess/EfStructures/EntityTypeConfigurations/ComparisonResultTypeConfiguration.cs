using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class ComparisonResultTypeConfiguration : IEntityTypeConfiguration<ComparisonResult>
{
    public void Configure(EntityTypeBuilder<ComparisonResult> builder)
    {
        builder.Property(result => result.SimilarityRate).IsRequired();

        builder.HasOne(result => result.Submit1Navigation)
            .WithMany(submit1 => submit1.AsSubmit1ComparisonResults)
            .HasForeignKey(result => result.Submit1Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();

        builder.HasOne(result => result.Submit2Navigation)
            .WithMany(submit2 => submit2.AsSubmit2ComparisonResults)
            .HasForeignKey(result => result.Submit2Id)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}