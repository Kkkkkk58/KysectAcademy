using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class StudentTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Navigation(student => student.PersonalInformation).IsRequired();
        builder.OwnsOne(student => student.PersonalInformation,
            navigationBuilder =>
            {
                navigationBuilder.Property(person => person.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName(nameof(Person.FirstName));
                navigationBuilder.Property(person => person.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName(nameof(Person.LastName));
                navigationBuilder.Property(person => person.FullName)
                    .HasColumnName(nameof(Person.FullName));
            });

        builder.HasOne(student => student.GroupNavigation)
            .WithMany(group => group.Students)
            .HasForeignKey(student => student.GroupId);
    }
}