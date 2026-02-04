using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class AssetPortion
    {
        public int AssetPortionId { get; set; }       
        public int AssetCategoryId { get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public decimal Value { get; set; }
    }
}
