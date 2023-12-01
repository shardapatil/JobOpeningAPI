﻿// <auto-generated />
using System;
using JobOpeningsAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JobOpeningsAPI.Migrations
{
    [DbContext(typeof(JobContext))]
    partial class JobContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JobOpeningsAPI.Models.Department", b =>
                {
                    b.Property<int>("DeptId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DeptId"));

                    b.Property<string>("DeptTitle")
                        .HasMaxLength(150)
                        .IsUnicode(false)
                        .HasColumnType("varchar(150)")
                        .HasColumnName("title");

                    b.HasKey("DeptId");

                    b.ToTable("Department");
                });

            modelBuilder.Entity("JobOpeningsAPI.Models.Job", b =>
                {
                    b.Property<int>("JobId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("JobId"));

                    b.Property<int?>("DepartmentDeptId")
                        .HasColumnType("int");

                    b.Property<DateTime>("JobClosingDate")
                        .HasColumnType("datetime")
                        .HasColumnName("closingDate");

                    b.Property<string>("JobCode")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(10)
                        .IsUnicode(false)
                        .HasColumnType("varchar(10)")
                        .HasColumnName("code")
                        .HasDefaultValueSql("CONCAT('JOB-', NEXT VALUE FOR JobCodeSequence)");

                    b.Property<string>("JobDescription")
                        .IsRequired()
                        .IsUnicode(false)
                        .HasColumnType("varchar(max)")
                        .HasColumnName("description");

                    b.Property<DateTime>("JobPostedDate")
                        .HasColumnType("datetime")
                        .HasColumnName("postedDate");

                    b.Property<string>("JobTitle")
                        .IsRequired()
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("title");

                    b.Property<int?>("LocationId")
                        .HasColumnType("int");

                    b.HasKey("JobId");

                    b.HasIndex("DepartmentDeptId");

                    b.HasIndex("LocationId");

                    b.ToTable("Job");
                });

            modelBuilder.Entity("JobOpeningsAPI.Models.Location", b =>
                {
                    b.Property<int>("LocationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("LocationId"));

                    b.Property<string>("LocationCity")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("city");

                    b.Property<string>("LocationCountry")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("country");

                    b.Property<string>("LocationState")
                        .HasMaxLength(50)
                        .IsUnicode(false)
                        .HasColumnType("varchar(50)")
                        .HasColumnName("state");

                    b.Property<string>("LocationTitle")
                        .HasMaxLength(100)
                        .IsUnicode(false)
                        .HasColumnType("varchar(100)")
                        .HasColumnName("title");

                    b.Property<int>("LocationZipCode")
                        .HasColumnType("int")
                        .HasColumnName("zip");

                    b.HasKey("LocationId");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("JobOpeningsAPI.Models.Job", b =>
                {
                    b.HasOne("JobOpeningsAPI.Models.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentDeptId");

                    b.HasOne("JobOpeningsAPI.Models.Location", "Location")
                        .WithMany()
                        .HasForeignKey("LocationId");

                    b.Navigation("Department");

                    b.Navigation("Location");
                });
#pragma warning restore 612, 618
        }
    }
}
