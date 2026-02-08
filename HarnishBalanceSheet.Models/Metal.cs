using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Metal
    {
        public int MetalId { get; set; }
        public int BalanceSheetId { get; set; }
        public int PreciousMetalId { get; set; }
        public string MetalName { get; set; }
        public decimal NumOunces { get; set; }
        public decimal PricePerOunce { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
    }
}
