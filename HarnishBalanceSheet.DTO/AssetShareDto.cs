using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetShareDto
    {
        [DataMember]
        public string Name {  get; set; }
        [DataMember]
        public decimal Total {  get; set; }
        [DataMember]
        public List<AssetDto> Assets { get; set; }

    }
}
