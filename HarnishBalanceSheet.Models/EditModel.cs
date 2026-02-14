using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class EditModel
    {
        public BalanceSheet BalanceSheet { get; set; }
        public IEnumerable<AssetCategory> AssetTypes { get; set; }
        public IEnumerable<PreciousMetal> MetalTypes { get; set; }
    }
}
