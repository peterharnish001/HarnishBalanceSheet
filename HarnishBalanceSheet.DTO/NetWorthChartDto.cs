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
        public decimal NetWorth { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as NetWorthChartDto;

            if (item == null) return false;

            return this.Date.Equals(item.Date) && this.NetWorth.Equals(item.NetWorth);
        }
    }
}
