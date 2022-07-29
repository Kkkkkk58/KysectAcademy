using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class SubmitTypeConfiguration : IEntityTypeConfiguration<Models.Entities.Submit>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Submit> builder)
    {
        builder.Property(s => s.StudentId).IsRequired();
        builder.Property(s => s.Homework).IsRequired();
        builder.Property(s => s.StudentId).IsRequired();
        
        builder.HasOne(s => s.StudentNavigation)
            .WithMany(s => s.Submits)
            .HasForeignKey(s => s.StudentId);

        builder.Navigation(s => s.StudentNavigation).IsRequired();
    }
}