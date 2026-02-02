namespace HarnishBalanceSheet.Models
{
    public class BalanceSheet
    {
        public int BalanceSheetId { get; set; }
        public DateTime Date { get; set; }
        public List<Asset> Assets { get; set; }
        public List<Liability> Liabilities { get; set; }
     }
}
