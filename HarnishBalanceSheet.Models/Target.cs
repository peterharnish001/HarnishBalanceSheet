using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Target
    {
        public int TargetId { get; set; }
        public int UserId { get; set; }
        public int AssetCategoryId { get; set; }
        public string AssetCategoryName { get; set; }
        public decimal Percentage { get; set; }
        public User User { get; set; }
        public AssetCategory AssetCategory { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as Target;

            if (item == null) return false;

            return this.AssetCategoryId.Equals(item.AssetCategoryId)
                && this.Percentage.Equals(item.Percentage)
                ;
        }
    }
}
