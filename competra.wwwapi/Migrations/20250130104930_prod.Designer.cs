﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using competra.wwwapi.Data;

#nullable disable

namespace competra.wwwapi.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20250130104930_prod")]
    partial class prod
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("competra.wwwapi.Models.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("ActivityName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("GroupId");

                    b.ToTable("Activities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActivityName = "Bordtennis",
                            GroupId = 1
                        });
                });

            modelBuilder.Entity("competra.wwwapi.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<DateOnly>("CreatedAt")
                        .HasColumnType("date");

                    b.Property<string>("GroupName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LogoUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CreatedAt = new DateOnly(2023, 1, 1),
                            GroupName = "Experis_aca",
                            LogoUrl = ""
                        });
                });

            modelBuilder.Entity("competra.wwwapi.Models.Match", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("EloChangeP1")
                        .HasColumnType("integer");

                    b.Property<int>("EloChangeP2")
                        .HasColumnType("integer");

                    b.Property<int>("P1Id")
                        .HasColumnType("integer");

                    b.Property<double>("P1Result")
                        .HasColumnType("double precision");

                    b.Property<int>("P2Id")
                        .HasColumnType("integer");

                    b.Property<double>("P2Result")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.ToTable("Matches");
                });

            modelBuilder.Entity("competra.wwwapi.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Password = "$2b$10$jne5qzW/fuDlZrmoNd9HA.eX61UUaVP4A2voVWLDwauZ5FiW437Qm",
                            Username = "John"
                        },
                        new
                        {
                            Id = 2,
                            Password = "$2b$10$jne5qzW/fuDlZrmoNd9HA.eX61UUaVP4A2voVWLDwauZ5FiW437Qm",
                            Username = "Ibz"
                        });
                });

            modelBuilder.Entity("competra.wwwapi.Models.UserActivity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("ActivityId")
                        .HasColumnType("integer");

                    b.Property<int>("Elo")
                        .HasColumnType("integer");

                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("UserActivities");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ActivityId = 1,
                            Elo = 1500,
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            ActivityId = 1,
                            Elo = 1500,
                            UserId = 2
                        });
                });

            modelBuilder.Entity("competra.wwwapi.Models.UserGroup", b =>
                {
                    b.Property<int>("UserId")
                        .HasColumnType("integer");

                    b.Property<int>("GroupId")
                        .HasColumnType("integer");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");

                    b.HasData(
                        new
                        {
                            UserId = 1,
                            GroupId = 1
                        },
                        new
                        {
                            UserId = 2,
                            GroupId = 1
                        });
                });

            modelBuilder.Entity("competra.wwwapi.Models.Activity", b =>
                {
                    b.HasOne("competra.wwwapi.Models.Group", "Group")
                        .WithMany("Activities")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");
                });

            modelBuilder.Entity("competra.wwwapi.Models.Match", b =>
                {
                    b.HasOne("competra.wwwapi.Models.Activity", "Activity")
                        .WithMany("Matches")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Activity");
                });

            modelBuilder.Entity("competra.wwwapi.Models.UserActivity", b =>
                {
                    b.HasOne("competra.wwwapi.Models.Activity", "Activity")
                        .WithMany("UserActivities")
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("competra.wwwapi.Models.User", "User")
                        .WithMany("UserActivities")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("competra.wwwapi.Models.UserGroup", b =>
                {
                    b.HasOne("competra.wwwapi.Models.Group", "Group")
                        .WithMany("UserGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("competra.wwwapi.Models.User", "User")
                        .WithMany("UserGroups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Group");

                    b.Navigation("User");
                });

            modelBuilder.Entity("competra.wwwapi.Models.Activity", b =>
                {
                    b.Navigation("Matches");

                    b.Navigation("UserActivities");
                });

            modelBuilder.Entity("competra.wwwapi.Models.Group", b =>
                {
                    b.Navigation("Activities");

                    b.Navigation("UserGroups");
                });

            modelBuilder.Entity("competra.wwwapi.Models.User", b =>
                {
                    b.Navigation("UserActivities");

                    b.Navigation("UserGroups");
                });
#pragma warning restore 612, 618
        }
    }
}
