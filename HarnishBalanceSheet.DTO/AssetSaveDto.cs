using System;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetSaveDto
    {
        [DataMember]
        public int? AssetId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public bool IsPercent { get; set; }
        [DataMember]
        public IEnumerable<AssetComponentSaveDto> AssetComponents { get; set; }
    }
}
