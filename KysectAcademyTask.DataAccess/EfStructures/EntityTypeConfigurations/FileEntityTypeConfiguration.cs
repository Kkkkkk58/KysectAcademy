using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class FileEntityTypeConfiguration : IEntityTypeConfiguration<FileEntity>
{
    public void Configure(EntityTypeBuilder<FileEntity> builder)
    {
        builder.HasKey(f => f.Path);
        builder.Property(f => f.Path).IsRequired();

        builder.HasOne(f => f.SubmitNavigation)
            .WithMany(s => s.Files)
            .HasForeignKey(f => f.SubmitId);
        builder.Navigation(f => f.SubmitNavigation).IsRequired();
    }
}