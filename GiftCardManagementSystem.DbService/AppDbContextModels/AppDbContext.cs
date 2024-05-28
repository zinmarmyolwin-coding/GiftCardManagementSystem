using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace GiftCardManagementSystem.Infrastructure.AppDbContextModels;

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

    public virtual DbSet<TblTransactionhistory> TblTransactionhistories { get; set; }

    public virtual DbSet<TblTransactionhistorydetail> TblTransactionhistorydetails { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

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
            entity.HasKey(e => e.GiftCardId).HasName("PRIMARY");

            entity.ToTable("tbl_giftcard");

            entity.HasIndex(e => e.GiftCardNo, "GiftCardNo").IsUnique();

            entity.Property(e => e.GiftCardId).HasColumnName("GiftCardID");
            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.CashbackAmount).HasPrecision(10, 2);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedUserId).HasMaxLength(50);
            entity.Property(e => e.Description).HasMaxLength(500);
            entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            entity.Property(e => e.GiftCardNo).HasMaxLength(50);
            entity.Property(e => e.Title).HasMaxLength(300);
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedUserId).HasMaxLength(50);
        });

        modelBuilder.Entity<TblPaymentmethod>(entity =>
        {
            entity.HasKey(e => e.PaymentMethodId).HasName("PRIMARY");

            entity.ToTable("tbl_paymentmethod");

            entity.Property(e => e.Discount).HasPrecision(5, 2);
            entity.Property(e => e.PaymentMethodCode).HasMaxLength(50);
            entity.Property(e => e.PaymentMethodName).HasMaxLength(100);
        });

        modelBuilder.Entity<TblTransactionhistory>(entity =>
        {
            entity.HasKey(e => e.TranHistoryId).HasName("PRIMARY");

            entity.ToTable("tbl_transactionhistory");

            entity.Property(e => e.CashbackAmount).HasPrecision(10, 2);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.DiscountAmount).HasPrecision(10, 2);
            entity.Property(e => e.NetAmount).HasPrecision(10, 2);
            entity.Property(e => e.PaymentCode).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
            entity.Property(e => e.TranId).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<TblTransactionhistorydetail>(entity =>
        {
            entity.HasKey(e => e.TransactionHistoryDetailId).HasName("PRIMARY");

            entity.ToTable("tbl_transactionhistorydetail");

            entity.Property(e => e.Amount).HasPrecision(10, 2);
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.GiftCardNo).HasMaxLength(50);
            entity.Property(e => e.Status).HasMaxLength(30);
            entity.Property(e => e.ToUserId).HasMaxLength(50);
            entity.Property(e => e.TotalAmount).HasPrecision(10, 2);
            entity.Property(e => e.TranId).HasMaxLength(50);
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(50);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("tbl_user");

            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.CashbackAmount).HasPrecision(10, 2);
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PhoneNo).HasMaxLength(20);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
