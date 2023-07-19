using System;
using System.Collections.Generic;
using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularAuthYtAPI.Context
{
    public partial class CashoutContext : DbContext
    {
        public CashoutContext()
        {
        }

        public CashoutContext(DbContextOptions<CashoutContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MPayWithdrawalTerminalId> MPayWithdrawalTerminalIds { get; set; } = null!;

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
            modelBuilder.Entity<MPayWithdrawalTerminalId>(entity =>
            {

                entity.ToTable("m-pay_withdrawal_terminal_ids");

                entity.Property(e => e.MPayAtmAffiliation)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_affiliation");

                entity.Property(e => e.MPayAtmBankCode)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_bank_code");

                entity.Property(e => e.MPayAtmBankName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_bank_name");

                entity.Property(e => e.MPayAtmBatchNum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_batch_num");

                entity.Property(e => e.MPayAtmCodeAgence)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_code_agence");

                entity.Property(e => e.MPayAtmCreationDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_creation_date");

                entity.Property(e => e.MPayAtmDispoCode).HasColumnName("m-pay_atm_dispo_code");

                entity.Property(e => e.MPayAtmId)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_id");

                entity.Property(e => e.MPayAtmNumAgent)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_Num_Agent");

                entity.Property(e => e.MPayAtmPaymentType)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_payment_type");

                entity.Property(e => e.MPayAtmPwd)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_pwd");

                entity.Property(e => e.MPayAtmSeqNum)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_seq_num");

                entity.Property(e => e.MPayAtmStatus1).HasColumnName("m-pay_atm_status1");

                entity.Property(e => e.MPayAtmStatus2).HasColumnName("m-pay_atm_status2");

                entity.Property(e => e.MPayAtmStatusDate1)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_status_date1");

                entity.Property(e => e.MPayAtmStatusDate2)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_status_date2");

                entity.Property(e => e.MPayAtmUpdateDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_atm_update_date");
                entity.HasKey(e => new { e.MPayAtmId, e.MPayAtmBankCode });
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
