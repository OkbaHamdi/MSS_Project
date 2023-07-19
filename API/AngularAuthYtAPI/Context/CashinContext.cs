using System;
using System.Collections.Generic;
using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularAuthYtAPI.Context
{
    public partial class CashinContext : DbContext
    {
        public CashinContext()
        {
        }

        public CashinContext(DbContextOptions<CashinContext> options)
            : base(options)
        {
        }

        public virtual DbSet<WTerminalsRefillWallet> WTerminalsRefillWallets { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=DESKTOP-BIJD7H5\\SQLEXPRESS;Database=AuthYTDB;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<WTerminalsRefillWallet>(entity =>
            {
                entity.HasKey(e => new { e.WpTerminalDistributorId, e.WpTerminalBankCode })
                    .HasName("PK_YourTableName");

                entity.ToTable("w_terminals_refill_wallet");

                entity.Property(e => e.WpTerminalDistributorId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_id");

                entity.Property(e => e.WpTerminalBankCode)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_bank_code");

                entity.Property(e => e.WpTerminalDistributorAffiliation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_affiliation");

                entity.Property(e => e.WpTerminalDistributorBatchNum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_batch_num");

                entity.Property(e => e.WpTerminalDistributorCreationDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_creation_date");

                entity.Property(e => e.WpTerminalDistributorDispoCode).HasColumnName("wp_terminal_distributor_dispo_code");

                entity.Property(e => e.WpTerminalDistributorPwd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_pwd");

                entity.Property(e => e.WpTerminalDistributorSeqNum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_seq_num");

                entity.Property(e => e.WpTerminalDistributorStatus1).HasColumnName("wp_terminal_distributor_status1");

                entity.Property(e => e.WpTerminalDistributorStatus2).HasColumnName("wp_terminal_distributor_status2");

                entity.Property(e => e.WpTerminalDistributorStatusDate1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_status_date1");

                entity.Property(e => e.WpTerminalDistributorStatusDate2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_status_date2");

                entity.Property(e => e.WpTerminalDistributorUpdateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("wp_terminal_distributor_update_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
