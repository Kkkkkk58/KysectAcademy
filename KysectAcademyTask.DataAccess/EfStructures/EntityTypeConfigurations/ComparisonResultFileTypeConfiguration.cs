using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class ComparisonResultFileTypeConfiguration : IEntityTypeConfiguration<ComparisonResultFile>
{
    public void Configure(EntityTypeBuilder<ComparisonResultFile> builder)
    {
        builder.HasKey(cf => new { cf.ComparisonResultId, cf.FileId });

        builder.HasOne(cf => cf.ComparisonResultNavigation)
            .WithMany(c => c.ComparisonResultFiles)
            .HasForeignKey(cf => cf.ComparisonResultId);

        builder.HasOne(cf => cf.FileNavigation)
            .WithMany(f => f.ComparisonResultFiles)
            .HasForeignKey(f => f.FileId);
    }
}