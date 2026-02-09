using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class BullionSummaryDto
    {
        [DataMember]
        public List<MetalDto> Bullion { get; set; }
        [DataMember]
        public decimal Total { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as BullionSummaryDto;

            if (item == null) return false;

            return this.Bullion.SequenceEqual(item.Bullion)
                ;
        }
    }
}
