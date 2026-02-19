using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetComponentSaveDto
    {
        [DataMember]
        public int? AssetComponentId { get; set; }
        [DataMember]
        public int AssetTypeId { get; set; }
        [DataMember]
        public decimal Value { get; set; }
    }
}
