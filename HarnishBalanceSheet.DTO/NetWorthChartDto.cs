using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class NetWorthChartDto
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public decimal Value { get; set; }
    }
}
