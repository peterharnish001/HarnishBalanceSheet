using HarnishBalanceSheet.Models;
using Microsoft.EntityFrameworkCore;

namespace HarnishBalanceSheet.DataAccess
{
    public class BalanceSheetContext : DbContext
    {

        public BalanceSheetContext(DbContextOptions<BalanceSheetContext> options) : base(options)
        {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Asset>()
                .HasKey(p => p.AssetId);

            modelBuilder.Entity<AssetCategory>()
                .HasKey(p => p.AssetCategoryId);

            modelBuilder.Entity<AssetPortion>()
                .HasKey(p => p.AssetPortionId);

            modelBuilder.Entity<BalanceSheet>()
                .HasKey(p => p.BalanceSheetId);

            modelBuilder.Entity<Liability>()
                .HasKey(p => p.LiabilityId);

            modelBuilder.Entity<MetalPosition>()
                .HasKey(p => p.MetalPositionId);

            modelBuilder.Entity<PreciousMetal>()
                .HasKey(p => p.PreciousMetalId);

            modelBuilder.Entity<Target>()
                .HasKey(p => p.TargetId);

            modelBuilder.Entity<User>()
                .HasKey(p => p.UserId);

            modelBuilder.Entity<NetWorthChartModel>()
                .HasNoKey();

            modelBuilder.Entity<BalanceSheetLinkItem>()
                .HasNoKey();

            modelBuilder.Entity<LiabilityChartItem>()
                .HasNoKey();

            base.OnModelCreating(modelBuilder);
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
        public DbSet<NetWorthChartModel> NetWorthChartModels { get; set; }
        public DbSet<BalanceSheetLinkItem> BalanceSheetLinks { get; set; }
        public DbSet<LiabilityChartItem> LiabilityChart {  get; set; }
    }
}
