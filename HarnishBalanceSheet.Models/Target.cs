using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Target
    {
        public int TargetId { get; set; }
        public int AssetCategoryId { get; set; }
        public decimal Percentage { get; set; }
    }
}
