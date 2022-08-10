using KysectAcademyTask.DataAccess.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace KysectAcademyTask.DataAccess.EfStructures.EntityTypeConfigurations;

public class HomeWorkTypeConfiguration : IEntityTypeConfiguration<HomeWork>
{
    public void Configure(EntityTypeBuilder<HomeWork> builder)
    {
        builder.HasIndex(homeWork => homeWork.Name)
            .IsUnique();
    }
}