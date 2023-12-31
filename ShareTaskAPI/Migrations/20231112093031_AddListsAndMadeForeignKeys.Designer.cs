﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using ShareTaskAPI.Context;

#nullable disable

namespace ShareTaskAPI.Migrations
{
    [DbContext(typeof(MyDbContext))]
    [Migration("20231112093031_AddListsAndMadeForeignKeys")]
    partial class AddListsAndMadeForeignKeys
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("ShareTaskAPI.Entities.List", b =>
                {
                    b.Property<long>("IdList")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idList");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdList"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("description");

                    b.Property<bool>("IsPublic")
                        .HasColumnType("boolean")
                        .HasColumnName("isPublic");

                    b.Property<string>("Linq")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("linq");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("name");

                    b.HasKey("IdList")
                        .HasName("Lists_pkey");

                    b.ToTable("Lists");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.Role", b =>
                {
                    b.Property<long>("IdRole")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idRole");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdRole"));

                    b.Property<string>("Role1")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("Role");

                    b.HasKey("IdRole")
                        .HasName("Roles_pkey");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.User", b =>
                {
                    b.Property<long>("IdUser")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idUser");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdUser"));

                    b.Property<string>("Firstname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("firstname");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean")
                        .HasColumnName("isAdmin");

                    b.Property<string>("Lastname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("lastname");

                    b.Property<string>("Midname")
                        .IsRequired()
                        .HasMaxLength(128)
                        .HasColumnType("character varying(128)")
                        .HasColumnName("midname");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("password");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("username");

                    b.HasKey("IdUser")
                        .HasName("Users_pkey");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.UserList", b =>
                {
                    b.Property<long>("IdUserList")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasColumnName("idUserList");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("IdUserList"));

                    b.Property<long>("IdList")
                        .HasColumnType("bigint")
                        .HasColumnName("idList");

                    b.Property<long>("IdUser")
                        .HasColumnType("bigint")
                        .HasColumnName("idUser");

                    b.HasKey("IdUserList")
                        .HasName("UsersLists_pkey");

                    b.HasIndex("IdList");

                    b.HasIndex("IdUser");

                    b.ToTable("UsersLists");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.UserList", b =>
                {
                    b.HasOne("ShareTaskAPI.Entities.List", "IdListNavigation")
                        .WithMany("UsersLists")
                        .HasForeignKey("IdList")
                        .IsRequired()
                        .HasConstraintName("UsersLists_idList_fkey");

                    b.HasOne("ShareTaskAPI.Entities.User", "IdUserNavigation")
                        .WithMany("UsersLists")
                        .HasForeignKey("IdUser")
                        .IsRequired()
                        .HasConstraintName("UsersLists_idUser_fkey");

                    b.Navigation("IdListNavigation");

                    b.Navigation("IdUserNavigation");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.List", b =>
                {
                    b.Navigation("UsersLists");
                });

            modelBuilder.Entity("ShareTaskAPI.Entities.User", b =>
                {
                    b.Navigation("UsersLists");
                });
#pragma warning restore 612, 618
        }
    }
}
