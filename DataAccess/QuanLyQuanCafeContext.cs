﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using BusinessObject.Object;

#nullable disable

namespace DataAccess
{
    public partial class QuanLyQuanCafeContext : DbContext
    {
        public QuanLyQuanCafeContext()
        {
        }

        public QuanLyQuanCafeContext(DbContextOptions<QuanLyQuanCafeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; }
        public virtual DbSet<Bill> Bills { get; set; }
        public virtual DbSet<BillInfo> BillInfos { get; set; }
        public virtual DbSet<Food> Foods { get; set; }
        public virtual DbSet<FoodCategory> FoodCategories { get; set; }
        public virtual DbSet<TableFood> TableFoods { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=(local);uid=sa;pwd=sa;database=QuanLyQuanCafe");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Account>(entity =>
            {
                entity.HasKey(e => e.UserName)
                    .HasName("PK__Account__C9F2845770DB48D5");

                entity.ToTable("Account");

                entity.Property(e => e.UserName).HasMaxLength(100);

                entity.Property(e => e.DisplayName)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("(N'Kter')");

                entity.Property(e => e.PassWord)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("((0))");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.ToTable("Bill");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.DateCheckIn)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DateCheckOut).HasColumnType("datetime");

                entity.Property(e => e.Discount).HasColumnName("discount");

                entity.Property(e => e.IdTable).HasColumnName("idTable");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("money")
                    .HasColumnName("totalPrice");

                entity.HasOne(d => d.IdTableNavigation)
                    .WithMany(p => p.Bills)
                    .HasForeignKey(d => d.IdTable)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Bill__idTable__38996AB5");
            });

            modelBuilder.Entity<BillInfo>(entity =>
            {
                entity.ToTable("BillInfo");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Count).HasColumnName("count");

                entity.Property(e => e.IdBill).HasColumnName("idBill");

                entity.Property(e => e.IdFood).HasColumnName("idFood");

                entity.HasOne(d => d.IdBillNavigation)
                    .WithMany(p => p.BillInfos)
                    .HasForeignKey(d => d.IdBill)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillInfo__idBill__398D8EEE");

                entity.HasOne(d => d.IdFoodNavigation)
                    .WithMany(p => p.BillInfos)
                    .HasForeignKey(d => d.IdFood)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__BillInfo__idFood__3A81B327");
            });

            modelBuilder.Entity<Food>(entity =>
            {
                entity.ToTable("Food");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("(N'Chưa đặt tên')");

                entity.Property(e => e.Price)
                    .HasColumnType("money")
                    .HasColumnName("price");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Foods)
                    .HasForeignKey(d => d.IdCategory)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Food__idCategory__3B75D760");
            });

            modelBuilder.Entity<FoodCategory>(entity =>
            {
                entity.ToTable("FoodCategory");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("(N'Chưa đặt tên')");
            });

            modelBuilder.Entity<TableFood>(entity =>
            {
                entity.ToTable("TableFood");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("name")
                    .HasDefaultValueSql("(N'Bàn chưa có tên')");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("status")
                    .HasDefaultValueSql("(N'Trống')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
