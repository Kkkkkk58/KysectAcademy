using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class SubmitTypeConfiguration : IEntityTypeConfiguration<Models.Entities.Submit>
{
    public void Configure(EntityTypeBuilder<Models.Entities.Submit> builder)
    {
        builder.HasOne(submit => submit.StudentNavigation)
            .WithMany(student => student.Submits)
            .HasForeignKey(submit => submit.StudentId)
            .IsRequired();

        builder.HasOne(submit => submit.HomeWorkNavigation)
            .WithMany()
            .HasForeignKey(submit => submit.HomeWorkId)
            .IsRequired();
    }
}