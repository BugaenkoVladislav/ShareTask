﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using TaskService;

#nullable disable

namespace TaskService.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20240224124959_AddTask")]
    partial class AddTask
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("TaskService.Entities.LoginPassword", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("LoginPasswords");
                });

            modelBuilder.Entity("TaskService.Entities.Profession", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Professions");
                });

            modelBuilder.Entity("TaskService.Entities.Task", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("IdCreator")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProfession")
                        .HasColumnType("bigint");

                    b.Property<long>("IdTeam")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("IdCreator");

                    b.HasIndex("IdProfession");

                    b.HasIndex("IdTeam");

                    b.ToTable("Tasks");
                });

            modelBuilder.Entity("TaskService.Entities.Team", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Teams");
                });

            modelBuilder.Entity("TaskService.Entities.TeamUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("IdTeam")
                        .HasColumnType("bigint");

                    b.Property<long>("IdUser")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("IdTeam");

                    b.HasIndex("IdUser");

                    b.ToTable("TeamsUsers");
                });

            modelBuilder.Entity("TaskService.Entities.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long>("IdLoginPassword")
                        .HasColumnType("bigint");

                    b.Property<long>("IdProfession")
                        .HasColumnType("bigint");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("IdLoginPassword")
                        .IsUnique();

                    b.HasIndex("IdProfession");

                    b.HasIndex("Phone")
                        .IsUnique();

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TaskService.Entities.Task", b =>
                {
                    b.HasOne("TaskService.Entities.User", "Creator")
                        .WithMany()
                        .HasForeignKey("IdCreator")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskService.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("IdProfession")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskService.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Creator");

                    b.Navigation("Profession");

                    b.Navigation("Team");
                });

            modelBuilder.Entity("TaskService.Entities.TeamUser", b =>
                {
                    b.HasOne("TaskService.Entities.Team", "Team")
                        .WithMany()
                        .HasForeignKey("IdTeam")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskService.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("IdUser")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Team");

                    b.Navigation("User");
                });

            modelBuilder.Entity("TaskService.Entities.User", b =>
                {
                    b.HasOne("TaskService.Entities.LoginPassword", "LoginPassword")
                        .WithMany()
                        .HasForeignKey("IdLoginPassword")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("TaskService.Entities.Profession", "Profession")
                        .WithMany()
                        .HasForeignKey("IdProfession")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LoginPassword");

                    b.Navigation("Profession");
                });
#pragma warning restore 612, 618
        }
    }
}
