

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

        public override bool Equals(object? obj)
        {
            var item = obj as Asset;

            if (item == null) return false;

            return this.AssetId.Equals(item.AssetId)
                && this.Name.Equals(item.Name)
                && this.AssetPortions.SequenceEqual(item.AssetPortions)
                ;
        }
    }
}
