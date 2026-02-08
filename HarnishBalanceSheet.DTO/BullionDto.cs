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
    }
}
