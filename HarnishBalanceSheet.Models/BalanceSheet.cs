namespace HarnishBalanceSheet.Models
{
    public class BalanceSheet
    {
        public int BalanceSheetId { get; set; }
        public int UserId { get; set; }
        public DateTime Date { get; set; }
        public User User { get; set; }
        public ICollection<Asset> Assets { get; set; }
        public ICollection<Coins> Coins { get; set; }
        public ICollection<Liability> Liabilities { get; set; }
     }
}
