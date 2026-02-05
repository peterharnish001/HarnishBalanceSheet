using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Details
    {
        public BalanceSheet BalanceSheet { get; set; }
        public IEnumerable<string> AssetTypes { get; set; }
        public IEnumerable<Target> Targets { get; set; }
    }
}
