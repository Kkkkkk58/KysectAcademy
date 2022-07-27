using KysectAcademyTask.DataAccess.Models.Entities;
using KysectAcademyTask.DataAccess.Models.Entities.Owned;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

internal class StudentTypeConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.Navigation(s => s.PersonalInformation).IsRequired();
        builder.OwnsOne(s => s.PersonalInformation,
            pd =>
            {
                pd.Property(p => p.FirstName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName(nameof(Person.FirstName));
                pd.Property(p => p.LastName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName(nameof(Person.LastName));
                pd.Property(p => p.FullName)
                    .HasColumnName(nameof(Person.FullName))
                    .HasComputedColumnSql("[FirstName] + ' ' + [LastName]");
            });

        builder.HasOne(s => s.GroupNavigation)
            .WithMany(g => g.Students)
            .HasForeignKey(s => s.GroupId);
    }
}