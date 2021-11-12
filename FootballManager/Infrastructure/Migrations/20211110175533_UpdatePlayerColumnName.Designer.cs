﻿// <auto-generated />
using System;
using Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20211110175533_UpdatePlayerColumnName")]
    partial class UpdatePlayerColumnName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.League", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Leagues");
                });

            modelBuilder.Entity("Domain.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("FreeAgent")
                        .HasColumnType("bit");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int>("TeamId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TeamId")
                        .IsUnique();

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("Domain.Player", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("FreeAgent")
                        .HasColumnType("bit");

                    b.Property<double>("Market_Value")
                        .HasColumnType("float");

                    b.Property<int?>("PlayerAttributeId")
                        .HasColumnType("int");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.Property<int?>("TeamId")
                        .HasColumnType("int");

                    b.Property<int>("TeamIdPlayer")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerAttributeId");

                    b.HasIndex("ProfileId");

                    b.HasIndex("TeamId");

                    b.ToTable("Players");
                });

            modelBuilder.Entity("Domain.PlayerAttribute", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("OVR")
                        .HasColumnType("int");

                    b.Property<int>("Potential")
                        .HasColumnType("int");

                    b.Property<int?>("TraitsId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TraitsId");

                    b.ToTable("Attributes");
                });

            modelBuilder.Entity("Domain.Profile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Profile");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double>("Budget")
                        .HasColumnType("float");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("PlayerId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PlayerId");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("Domain.Traits", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("ExtraOvr")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.ToTable("Traits");
                });

            modelBuilder.Entity("Domain.Manager", b =>
                {
                    b.HasOne("Domain.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("Domain.Team", null)
                        .WithOne("Manager")
                        .HasForeignKey("Domain.Manager", "TeamId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Domain.Player", b =>
                {
                    b.HasOne("Domain.PlayerAttribute", "PlayerAttribute")
                        .WithMany()
                        .HasForeignKey("PlayerAttributeId");

                    b.HasOne("Domain.Profile", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");

                    b.HasOne("Domain.Team", null)
                        .WithMany("Players")
                        .HasForeignKey("TeamId");

                    b.Navigation("PlayerAttribute");

                    b.Navigation("Profile");
                });

            modelBuilder.Entity("Domain.PlayerAttribute", b =>
                {
                    b.HasOne("Domain.Traits", "Traits")
                        .WithMany()
                        .HasForeignKey("TraitsId");

                    b.Navigation("Traits");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.HasOne("Domain.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerId");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("Domain.Team", b =>
                {
                    b.Navigation("Manager");

                    b.Navigation("Players");
                });
#pragma warning restore 612, 618
        }
    }
}
