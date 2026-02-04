using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Liability
    {
        public int LiabilityId { get; set; }
        public int BalanceSheetId { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
    }
}
