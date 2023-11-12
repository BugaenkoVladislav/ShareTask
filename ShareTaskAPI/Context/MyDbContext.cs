using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using ShareTaskAPI.Entities;
using Task = ShareTaskAPI.Entities.Task;

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
    public virtual DbSet<UserList> UsersLists { get; set; }
    public virtual DbSet<Task> Tasks { get; set; }
    public virtual DbSet<Role> Roles { get; set; }
    public virtual DbSet<List> Lists { get; set; }
    public virtual DbSet<User> Users { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.IdRole).HasName("Roles_pkey");

            entity.Property(e => e.IdRole)
                .HasColumnName("idRole");
            entity.Property(e => e.Role1)
                .HasMaxLength(256)
                .HasColumnName("Role");
        });
        
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Users_pkey");

            entity.Property(e => e.IdUser)
                .HasColumnName("idUser");
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
        modelBuilder.Entity<List>(entity =>
        {
            entity.HasKey(e => e.IdList).HasName("Lists_pkey");

            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IdCreator).HasColumnName("idCreator");
            entity.Property(e => e.Name).HasColumnName("name").HasMaxLength(256);
            entity.Property(e => e.IsPublic).HasColumnName("isPublic");
            entity.Property(e => e.Linq).HasColumnName("linq");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.IdCreatorNavigation)
                .WithMany(p => p.Lists)
                .HasForeignKey(e => e.IdCreator)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Lists_idCreator_fkey");

        });
        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.IdUserList).HasName("UsersLists_pkey");

            entity.Property(e => e.IdUserList).HasColumnName("idUserList");
            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IdUser).HasColumnName("idUser");
            
            entity.HasOne(d => d.IdListNavigation)
                .WithMany(p => p.UsersLists)
                .HasForeignKey(d => d.IdList)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UsersLists_idList_fkey");
            entity.HasOne(d => d.IdUserNavigation)
                .WithMany(p => p.UsersLists)
                .HasForeignKey(e => e.IdUser)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("UsersLists_idUser_fkey");


        });
        modelBuilder.Entity<Task>(entity =>
        {
            entity.HasKey(e => e.IdTask).HasName("Tasks_pkey");

            entity.Property(e => e.IdList).HasColumnName("idList");
            entity.Property(e => e.IdCreator).HasColumnName("idCreator");
            entity.Property(e => e.NameTask).HasColumnName("nameTask");
            entity.Property(e => e.Description).HasColumnName("description");

            entity.HasOne(d => d.IdListNavigaton)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.IdList)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tasks_idList_fkey");
            entity.HasOne(d => d.IdCreatorNavigaton)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.IdCreator)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tasks_idCreator_fkey");
            entity.HasOne(d => d.IdRoleNavigation)
                .WithMany(p => p.Tasks)
                .HasForeignKey(e => e.IdRole)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("Tasks_idRole_fkey");
        });
    }
}
