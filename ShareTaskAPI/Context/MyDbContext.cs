using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShareTaskAPI.Entities;

namespace ShareTaskAPI.Context;

public partial class MyDbContext : DbContext
{
    public MyDbContext()
    {
    }

    public MyDbContext(DbContextOptions<MyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<List> Lists { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Entities.Task> Tasks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersList> UsersLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("Lists_pkey");

            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IsPublic).HasColumnName("isPublic");
            entity.Property(e => e.Name)
                .HasMaxLength(512)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("Roles_pkey");

            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Role1)
                .HasMaxLength(256)
                .HasColumnName("Role");
        });

        modelBuilder.Entity<Entities.Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("Tasks_pkey");

            entity.Property(e => e.IdTask).HasColumnName("idTask");
            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IdRole).HasColumnName("idRole");
            entity.Property(e => e.Task1).HasColumnName("Task");

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_idList_fkey");

            entity.HasOne(d => d.IdRoleNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdRole)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_idRole_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Tasks)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Tasks_IdUser_fkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pkey");

            entity.Property(e => e.IdUser)
                .HasDefaultValueSql("nextval('\"Users_IdUser_seq\"'::regclass)")
                .HasColumnName("idUser");
            entity.Property(e => e.Balance).HasColumnName("balance");
            entity.Property(e => e.Firstname)
                .HasMaxLength(128)
                .HasColumnName("firstname");
            entity.Property(e => e.IsAdmin).HasColumnName("isAdmin");
            entity.Property(e => e.Lastname)
                .HasMaxLength(128)
                .HasColumnName("lastname");
            entity.Property(e => e.Midname)
                .HasMaxLength(128)
                .HasColumnName("midname");
            entity.Property(e => e.Password)
                .HasMaxLength(256)
                .HasColumnName("password");
            entity.Property(e => e.Username)
                .HasMaxLength(256)
                .HasColumnName("username");
        });

        modelBuilder.Entity<UsersList>(entity =>
        {
            entity.HasKey(e => e.IdUsersLists).HasName("UsersLists_pkey");

            entity.Property(e => e.IdUsersLists).HasColumnName("idUsersLists");
            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IdUser).HasColumnName("idUser");

            entity.HasOne(d => d.IdListNavigation).WithMany(p => p.UsersLists)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsersLists_idList_fkey");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.UsersLists)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("UsersLists_idUser_fkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
