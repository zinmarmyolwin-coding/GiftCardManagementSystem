using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GiftCardManagementSystem.DbService.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblAdminuser> TblAdminusers { get; set; }

    public virtual DbSet<TblGiftcard> TblGiftcards { get; set; }

    public virtual DbSet<TblPaymentmethod> TblPaymentmethods { get; set; }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<TblAdminuser>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_adminuser");

            entity.Property(e => e.Password).HasMaxLength(100);
            entity.Property(e => e.UserId).HasMaxLength(40);
            entity.Property(e => e.UserName).HasMaxLength(50);
            entity.Property(e => e.UserRole).HasMaxLength(50);
        });

        modelBuilder.Entity<TblGiftcard>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("tbl_giftcard");

            entity.HasIndex(e => e.GiftCardNo, "GiftCardNo").IsUnique();

            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.GiftCardNo).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(300);
        });

        modelBuilder.Entity<TblPaymentmethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PRIMARY");

            entity.ToTable("tbl_paymentmethod");

            entity.Property(e => e.Discount).HasPrecision(5, 2);
            entity.Property(e => e.PaymentMethodName).HasMaxLength(100);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
