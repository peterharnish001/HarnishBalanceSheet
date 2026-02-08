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
        public int PreciousMetalId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Ounces { get; set; }
        [DataMember]
        public decimal PricePerOunce { get; set; }
        [DataMember]
        public decimal TotalPrice { get; set; }
    }
}
