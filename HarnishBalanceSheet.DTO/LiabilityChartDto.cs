using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class LiabilityChartDto
    {
        [DataMember]
        public DateTime Date {  get; set; }
        [DataMember]
        public decimal TotalLiabilities { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as LiabilityChartDto;

            if (item == null) return false;
            return this.Date.Equals(item.Date) && this.TotalLiabilities.Equals(item.TotalLiabilities);
        }
    }
}
