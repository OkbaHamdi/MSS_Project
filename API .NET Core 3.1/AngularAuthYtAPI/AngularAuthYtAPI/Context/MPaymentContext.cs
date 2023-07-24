using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthYtAPI.Context
{
    public partial class MPaymentContext : DbContext
    {
        public MPaymentContext()
        {
        }

        public MPaymentContext(DbContextOptions<MPaymentContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Bankinfo> Bankinfos { get; set; } = null!;
        public virtual DbSet<MPayTransferTpe> MPayTransferTpes { get; set; } = null!;
        public virtual DbSet<MPayWithdrawalTerminalId> MPayWithdrawalTerminalIds { get; set; } = null!;
        public virtual DbSet<MobileService> MobileServices { get; set; } = null!;
        public virtual DbSet<PayBin> PayBins { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
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
            modelBuilder.Entity<Bankinfo>(entity =>
            {
                entity.ToTable("bankinfo");

                entity.Property(e => e.ServiceProvider)
                    .HasMaxLength(10)
                    .IsUnicode(false);

                entity.Property(e => e.Smsid).HasColumnName("SMSid");

                entity.Property(e => e.Smspwd).HasColumnName("SMSpwd");
            });

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
                entity.HasKey(e => new
                {
                    e.MPayAtmAffiliation,
                    e.MPayAtmBankCode,
                    // Add other properties that form the primary key if needed
                });
            });

            modelBuilder.Entity<MobileService>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("MobileService");

                entity.Property(e => e.BankCode)
                    .HasMaxLength(5)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Id).ValueGeneratedOnAdd();

                entity.Property(e => e.IdEtablissement)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ServiceProvider)
                    .HasMaxLength(3)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<PayBin>(entity =>
            {
                entity.HasKey(e => new { e.BinId, e.BankCode });

                entity.ToTable("PayBin");

                entity.Property(e => e.BinId)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankCode)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.BankName)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CodeApp)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Privilege)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("users");


                entity.Property(e => e.RefreshTokenExpiryTime).HasDefaultValueSql("('0001-01-01T00:00:00.0000000')");
            });

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
