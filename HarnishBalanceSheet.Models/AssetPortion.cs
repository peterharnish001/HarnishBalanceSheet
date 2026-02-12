using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class AssetPortion
    {
        public int AssetPortionId { get; set; }       
        public int AssetCategoryId { get; set; }
        public string AssetCategoryName { get; set; }
        public AssetCategory AssetCategory {  get; set; }
        public int AssetId { get; set; }
        public Asset Asset { get; set; }
        public decimal Value { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as AssetPortion;

            if (item == null) return false;

            return this.AssetPortionId.Equals(item.AssetPortionId)
                && this.Value.Equals(item.Value)
                && this.AssetCategoryId.Equals(item.AssetCategoryId)
                ;
        }
    }
}
