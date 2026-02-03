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
        public List<CoinDto> Coins { get; set; }
        [DataMember]
        public decimal Total { get; set; }
    }
}
