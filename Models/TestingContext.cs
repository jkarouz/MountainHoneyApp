using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MountainHoneyApp.Models;

public partial class TestingContext : DbContext
{
    public TestingContext()
    {
    }

    public TestingContext(DbContextOptions<TestingContext> options)
        : base(options)
    {
    }


    public virtual DbSet<User> Users { get; set; }
    public virtual DbSet<Userdetail> Userdetails { get; set; }
    public virtual DbSet<MountainHoney> MountainHoneys { get; set; }

    public virtual DbSet<Sunrise> Sunrises { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

     => optionsBuilder.UseSqlServer();
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
     


        modelBuilder.Entity<MountainHoney>(entity =>
        {
            entity.ToTable("MountainHoney", "dbo");

            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });

        modelBuilder.Entity<Sunrise>(entity =>
        {
            entity.ToTable("Sunrise", "dbo");

            entity.Property(e => e.ContactNumber).HasMaxLength(50);
            entity.Property(e => e.DateOnly)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasComputedColumnSql("(CONVERT([varchar](10),[DateOnlyTime],(105)))", false);
            entity.Property(e => e.DateOnlyTime).HasColumnType("datetime");
            entity.Property(e => e.FullName).HasMaxLength(200);
            entity.Property(e => e.Comments).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.Surname).HasMaxLength(50);
        });


        modelBuilder.Entity<Userdetail>(entity =>
        {
            entity.ToTable("userdetails");

            entity.Property(e => e.Email)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Mobile)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.Property(e => e.Password)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
