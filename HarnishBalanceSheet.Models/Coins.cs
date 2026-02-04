using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Coins
    {
        public int CoinId { get; set; }
        public int BalanceSheetId { get; set; }
        public int PreciousMetalId { get; set; }
        public decimal NumOunces { get; set; }
        public decimal Value { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
    }
}
