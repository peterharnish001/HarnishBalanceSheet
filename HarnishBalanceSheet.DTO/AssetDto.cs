using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsPercent { get; set; }
        [DataMember]
        public IEnumerable<AssetComponentDto> AssetComponents { get; set; }
    }
}
