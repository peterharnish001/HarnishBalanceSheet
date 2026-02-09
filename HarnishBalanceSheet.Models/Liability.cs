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

        public override bool Equals(object? obj)
        {
            var item = obj as Liability;

            if (item == null) return false;

            return this.LiabilityId.Equals(item.LiabilityId)
                && this.Name.Equals(item.Name)
                && this.Value.Equals(item.Value)
                ;
        }
    }
}
