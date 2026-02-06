using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class CoinSummaryDto
    {
        [DataMember]
        public List<BullionDto> Metals { get; set; }
        [DataMember]
        public decimal Total { get; set; }
    }
}
