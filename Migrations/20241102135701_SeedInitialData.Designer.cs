﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ProjectManagementApp.Data;

#nullable disable

namespace ProjectManagementApp.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241102135701_SeedInitialData")]
    partial class SeedInitialData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("ProjectManagementApp.Models.Project", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ProjectId"));

                    b.Property<decimal>("Budget")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<string>("ProjectName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ProjectId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("RoleName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            RoleId = 1,
                            RoleName = "Manager"
                        },
                        new
                        {
                            RoleId = 2,
                            RoleName = "Employee"
                        });
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Task", b =>
                {
                    b.Property<int>("TaskId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TaskId"));

                    b.Property<int>("AssignedToId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Priority")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ProjectId")
                        .HasColumnType("int");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Status")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TaskName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TaskId");

                    b.HasIndex("AssignedToId");

                    b.HasIndex("ProjectId");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            PasswordHash = new byte[] { 7, 135, 68, 233, 146, 124, 243, 116, 245, 101, 131, 29, 226, 246, 152, 83, 9, 112, 20, 26, 145, 187, 61, 129, 81, 114, 19, 46, 78, 110, 31, 173, 146, 83, 119, 237, 145, 124, 246, 33, 11, 167, 229, 83, 152, 244, 135, 138, 71, 210, 58, 26, 244, 124, 130, 7, 235, 34, 158, 143, 149, 45, 23, 95 },
                            PasswordSalt = new byte[] { 61, 137, 146, 221, 120, 4, 199, 189, 128, 24, 118, 112, 121, 8, 221, 7, 206, 186, 208, 71, 133, 4, 253, 192, 129, 114, 131, 66, 129, 244, 180, 154, 251, 95, 182, 4, 37, 155, 196, 102, 246, 215, 9, 51, 133, 77, 250, 37, 109, 124, 249, 121, 134, 153, 214, 198, 45, 216, 148, 246, 5, 17, 143, 44, 43, 20, 46, 71, 191, 55, 225, 113, 1, 34, 206, 181, 26, 68, 92, 146, 54, 231, 62, 232, 89, 42, 244, 28, 80, 61, 93, 146, 243, 244, 206, 219, 12, 20, 188, 37, 75, 93, 115, 199, 78, 243, 87, 241, 235, 193, 138, 172, 67, 243, 207, 120, 93, 240, 193, 49, 11, 155, 233, 203, 184, 128, 88, 113 },
                            RoleId = 1,
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Project", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.User", "Owner")
                        .WithMany("ProjectsOwned")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Task", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.User", "AssignedTo")
                        .WithMany("TasksAssigned")
                        .HasForeignKey("AssignedToId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("ProjectManagementApp.Models.Project", "Project")
                        .WithMany("Tasks")
                        .HasForeignKey("ProjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("AssignedTo");

                    b.Navigation("Project");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.User", b =>
                {
                    b.HasOne("ProjectManagementApp.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Project", b =>
                {
                    b.Navigation("Tasks");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("ProjectManagementApp.Models.User", b =>
                {
                    b.Navigation("ProjectsOwned");

                    b.Navigation("TasksAssigned");
                });
#pragma warning restore 612, 618
        }
    }
}
