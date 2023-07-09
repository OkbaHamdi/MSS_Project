using System;
using System.Collections.Generic;
using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace AngularAuthYtAPI.ModelsT
{
    public partial class AuthYTDBContext : DbContext
    {
        public AuthYTDBContext()
        {
        }

        public AuthYTDBContext(DbContextOptions<AuthYTDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<MobileService> MobileServices { get; set; } = null!;

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

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
