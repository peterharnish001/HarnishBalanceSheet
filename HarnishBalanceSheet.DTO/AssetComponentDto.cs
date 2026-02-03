using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetComponentDto
    {
        [DataMember]
        public int? AssetComponentId { get; set; }
        [DataMember]
        public string AssetType { get; set; }
        [DataMember]
        public int AssetTypeId { get; set; }
        [DataMember]
        public decimal Fraction {  get; set; }
        [DataMember]
        public decimal Value { get; set; }
    }
}
