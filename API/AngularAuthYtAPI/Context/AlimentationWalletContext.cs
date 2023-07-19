using System;
using System.Collections.Generic;
using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularAuthYtAPI.Context
{
    public partial class AlimentationWalletContext : DbContext
    {
        public AlimentationWalletContext()
        {
        }

        public AlimentationWalletContext(DbContextOptions<AlimentationWalletContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MPayTransferTpe> MPayTransferTpes { get; set; } = null!;

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
            modelBuilder.Entity<MPayTransferTpe>(entity =>
            {
                entity.HasKey(e => new { e.MPayTransferTpeIdDebit, e.PayTransferTpeBankCode })
                    .HasName("PK__m-pay_tr__A0FE553515CC82DB");

                entity.ToTable("m-pay_transfer_tpe");

                entity.Property(e => e.MPayTransferTpeIdDebit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_id_debit");

                entity.Property(e => e.PayTransferTpeBankCode)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("pay_transfer_tpe_bankCode");

                entity.Property(e => e.MPayTransferTpeAffiliationNum)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_affiliation_num");

                entity.Property(e => e.MPayTransferTpeBatchNumCredit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_batch_num_credit");

                entity.Property(e => e.MPayTransferTpeBatchNumDebit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_batch_num_debit");

                entity.Property(e => e.MPayTransferTpeDispoCode)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_dispo_code");

                entity.Property(e => e.MPayTransferTpeDispoDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_dispo_date");

                entity.Property(e => e.MPayTransferTpeIdCredit)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_id_credit");

                entity.Property(e => e.MPayTransferTpeIdOp)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_id_op");

                entity.Property(e => e.MPayTransferTpeIdTrsct)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_id_trsct");

                entity.Property(e => e.MPayTransferTpeSeqNumCredit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_seq_num_credit");

                entity.Property(e => e.MPayTransferTpeSeqNumDebit)
                    .HasMaxLength(3)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_seq_num_debit");

                entity.Property(e => e.MPayTransferTpeStatus)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_status");

                entity.Property(e => e.MPayTransferTpeStatusDate)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("m-pay_transfer_tpe_status_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
