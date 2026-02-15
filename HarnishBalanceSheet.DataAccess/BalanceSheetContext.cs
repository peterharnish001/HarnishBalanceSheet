using HarnishBalanceSheet.Models;
using Microsoft.EntityFrameworkCore;

namespace HarnishBalanceSheet.DataAccess
{
    public class BalanceSheetContext : DbContext
    {

        public BalanceSheetContext(DbContextOptions<BalanceSheetContext> options) : base(options)
        { }

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
