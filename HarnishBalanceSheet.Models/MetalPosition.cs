using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class MetalPosition
    {
        public int MetalPositionId { get; set; }
        public int BalanceSheetId { get; set; }
        public int PreciousMetalId { get; set; }
        public decimal NumOunces { get; set; }
        public decimal PricePerOunce { get; set; }
        public BalanceSheet BalanceSheet { get; set; }
        public PreciousMetal PreciousMetal { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as MetalPosition;

            if (item == null) return false;

            return this.MetalPositionId.Equals(item.MetalPositionId)
                && this.NumOunces.Equals(item.NumOunces)
                && this.PricePerOunce.Equals(item.PricePerOunce)
                && this.PreciousMetalId.Equals(item.PreciousMetalId)
                ;
        }
    }
}
