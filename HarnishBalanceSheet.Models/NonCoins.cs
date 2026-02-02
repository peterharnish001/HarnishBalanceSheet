using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class NonCoins : Asset
    {
        public string Name { get; set; }
        public List<AssetPortion> AssetPortions { get; set; }

    }
}
