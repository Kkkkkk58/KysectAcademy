using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class FileEntityTypeConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasIndex(f => f.Path)
            .IsUnique();

        builder.HasOne(f => f.SubmitNavigation)
            .WithMany(s => s.Files)
            .HasForeignKey(f => f.SubmitId)
            .IsRequired();
    }
}