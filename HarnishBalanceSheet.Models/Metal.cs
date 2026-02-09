using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.Models
{
    public class Metal
    {
        public int MetalId { get; set; }
        public int BalanceSheetId { get; set; }
        public int PreciousMetalId { get; set; }
        public string MetalName { get; set; }
        public decimal NumOunces { get; set; }
        public decimal PricePerOunce { get; set; }
        public BalanceSheet BalanceSheet { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as Metal;

            if (item == null) return false;

            return this.MetalId.Equals(item.MetalId)
                && this.NumOunces.Equals(item.NumOunces)
                && this.PricePerOunce.Equals(item.PricePerOunce)
                && this.PreciousMetalId.Equals(item.PreciousMetalId)
                ;
        }
    }
}
