using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetEditDto
    {
        [DataMember]
        public int? AssetId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public bool IsPercent { get; set; }
        [DataMember]
        public List<AssetComponentDto> AssetComponents { get; set; }
    }
}
