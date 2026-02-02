using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Coins : Asset
    {
        public int PreciousMetalId { get; set; }
        public decimal NumOunces { get; set; }
        public decimal Value { get; set; }
    }
}
