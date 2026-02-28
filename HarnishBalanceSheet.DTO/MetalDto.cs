using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class MetalDto
    {
        [DataMember]
        public int? MetalPositionId { get; set; }
        [DataMember]
        public int PreciousMetalId { get; set; }
        [DataMember]
        public string MetalName { get; set; }
        [DataMember]
        public decimal NumOunces { get; set; }
        [DataMember]
        public decimal PricePerOunce { get; set; }
        [DataMember]
        public decimal TotalPrice { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as MetalDto;

            if (item == null) return false;
            return this.MetalName.Equals(item.MetalName) 
                && this.NumOunces.Equals(item.NumOunces)
                && this.PricePerOunce.Equals(item.PricePerOunce)
                && this.MetalPositionId.Equals(item.MetalPositionId)
                ;
        }
    }
}
