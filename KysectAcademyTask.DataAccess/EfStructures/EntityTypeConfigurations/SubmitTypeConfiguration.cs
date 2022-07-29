using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class SubmitTypeConfiguration : IEntityTypeConfiguration<Models.Entities.Submit>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Submit> builder)
    {
        builder.HasOne(s => s.StudentNavigation)
            .WithMany(s => s.Submits)
            .HasForeignKey(s => s.StudentId)
            .IsRequired();

        builder.HasOne(s => s.HomeWorkNavigation)
            .WithMany()
            .HasForeignKey(s => s.HomeWorkId)
            .IsRequired();
    }
}