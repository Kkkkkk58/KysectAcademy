﻿// <auto-generated />
using System;
using KysectAcademyTask.DataAccess.EfStructures;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KysectAcademyTask.DataAccess.EfStructures.Migrations
{
    [DbContext(typeof(SubmitComparisonDbContext))]
    partial class SubmitComparisonDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.ComparisonResult", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<double>("SimilarityRate")
                        .HasColumnType("float");

                    b.Property<int>("Submit1Id")
                        .HasColumnType("int");

                    b.Property<int>("Submit2Id")
                        .HasColumnType("int");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Submit1Id");

                    b.HasIndex("Submit2Id");

                    b.ToTable("ComparisonResults");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.HomeWork", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(450)");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasFilter("[Name] IS NOT NULL");

                    b.ToTable("HomeWorks");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Students");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Submit", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<DateTime?>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("HomeWorkId")
                        .HasColumnType("int");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.Property<byte[]>("TimeStamp")
                        .IsConcurrencyToken()
                        .ValueGeneratedOnAddOrUpdate()
                        .HasColumnType("rowversion");

                    b.HasKey("Id");

                    b.HasIndex("HomeWorkId");

                    b.HasIndex("StudentId");

                    b.ToTable("Submits");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.ComparisonResult", b =>
                {
                    b.HasOne("KysectAcademyTask.DataAccess.Models.Entities.Submit", "Submit1Navigation")
                        .WithMany("AsSubmit1ComparisonResults")
                        .HasForeignKey("Submit1Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("KysectAcademyTask.DataAccess.Models.Entities.Submit", "Submit2Navigation")
                        .WithMany("AsSubmit2ComparisonResults")
                        .HasForeignKey("Submit2Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("Submit1Navigation");

                    b.Navigation("Submit2Navigation");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Student", b =>
                {
                    b.HasOne("KysectAcademyTask.DataAccess.Models.Entities.Group", "GroupNavigation")
                        .WithMany("Students")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.OwnsOne("KysectAcademyTask.DataAccess.Models.Entities.Owned.Person", "PersonalInformation", b1 =>
                        {
                            b1.Property<int>("StudentId")
                                .HasColumnType("int");

                            b1.Property<string>("FirstName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("FirstName");

                            b1.Property<string>("FullName")
                                .HasColumnType("nvarchar(max)")
                                .HasColumnName("FullName");

                            b1.Property<string>("LastName")
                                .IsRequired()
                                .HasMaxLength(50)
                                .HasColumnType("nvarchar(50)")
                                .HasColumnName("LastName");

                            b1.HasKey("StudentId");

                            b1.ToTable("Students");

                            b1.WithOwner()
                                .HasForeignKey("StudentId");
                        });

                    b.Navigation("GroupNavigation");

                    b.Navigation("PersonalInformation")
                        .IsRequired();
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Submit", b =>
                {
                    b.HasOne("KysectAcademyTask.DataAccess.Models.Entities.HomeWork", "HomeWorkNavigation")
                        .WithMany()
                        .HasForeignKey("HomeWorkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KysectAcademyTask.DataAccess.Models.Entities.Student", "StudentNavigation")
                        .WithMany("Submits")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("HomeWorkNavigation");

                    b.Navigation("StudentNavigation");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Group", b =>
                {
                    b.Navigation("Students");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Student", b =>
                {
                    b.Navigation("Submits");
                });

            modelBuilder.Entity("KysectAcademyTask.DataAccess.Models.Entities.Submit", b =>
                {
                    b.Navigation("AsSubmit1ComparisonResults");

                    b.Navigation("AsSubmit2ComparisonResults");
                });
#pragma warning restore 612, 618
        }
    }
}
