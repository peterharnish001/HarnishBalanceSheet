using HarnishBalanceSheet.Models;
using Microsoft.EntityFrameworkCore;

namespace HarnishBalanceSheet.DataAccess
{
    public class BalanceSheetContext : DbContext
    {

        public BalanceSheetContext(DbContextOptions<BalanceSheetContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>().ToTable("Asset");
            modelBuilder.Entity<AssetCategory>().ToTable("AssetCategory");
            modelBuilder.Entity<AssetPortion>().ToTable("AssetPortion");
            modelBuilder.Entity<BalanceSheet>().ToTable("BalanceSheet");
            modelBuilder.Entity<Liability>().ToTable("Liability");
            modelBuilder.Entity<MetalPosition>().ToTable("MetalPosition");
            modelBuilder.Entity<PreciousMetal>().ToTable("PreciousMetal");
            modelBuilder.Entity<Target>().ToTable("Target");
            modelBuilder.Entity<User>().ToTable("User");
        }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetCategory> AssetCategories { get; set; }
        public DbSet<AssetPortion> AssetPortions { get; set; }
        public DbSet<BalanceSheet> BalanceSheets { get; set; }
        public DbSet<Liability> Liabilities { get; set; }
        public DbSet<MetalPosition> MetalPositions { get; set; }
        public DbSet<PreciousMetal> PreciousMetals { get; set; }
        public DbSet<Target> Targets { get; set; }
        public DbSet<User> Users { get; set; }      
    }
}
