

namespace HarnishBalanceSheet.Models
{
    public class Asset
    {
        public int AssetId { get; set; }
        public string Name { get; set; }
        public int BalanceSheetId { get; set; }
        public bool IsPercent { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
        public ICollection<AssetPortion> AssetPortions { get; set; }
    }
}
