namespace HarnishBalanceSheet.Models
{
    public class BalanceSheet
    {
        public int BalanceSheetId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<MetalPosition> Bullion { get; set; }
        public ICollection<Liability> Liabilities { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as BalanceSheet;

            if (item == null) return false;

            return this.BalanceSheetId.Equals(item.BalanceSheetId)
                && this.Date.Equals(item.Date)
                && this.Assets.SequenceEqual(item.Assets)
                && this.Bullion.SequenceEqual(item.Bullion)
                && this.Liabilities.SequenceEqual(item.Liabilities)
                ;
        }
    }
}
