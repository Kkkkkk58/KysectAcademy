using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class ComparisonResultTypeConfiguration : IEntityTypeConfiguration<ComparisonResult>
{
    public void Configure(EntityTypeBuilder<ComparisonResult> builder)
    {
        builder.Property(c => c.FileName1).IsRequired();
        builder.Property(c => c.FileName2).IsRequired();
        builder.Property(c => c.Metrics).IsRequired();
        builder.Property(c => c.SimilarityRate).IsRequired();

        builder.HasMany(c => c.Files)
            .WithMany(f => f.ComparisonResults);
    }
}