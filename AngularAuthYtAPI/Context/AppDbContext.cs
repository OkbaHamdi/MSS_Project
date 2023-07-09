using AngularAuthYtAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AngularAuthYtAPI.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<BankInfo> BankInfos { get; set; }
        public DbSet<MobileService> MobileServices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>().ToTable("users");
            builder.Entity<BankInfo>().ToTable("bankinfo");
            builder.Entity<MobileService>().ToTable("MobileService");
        }
    }
}
