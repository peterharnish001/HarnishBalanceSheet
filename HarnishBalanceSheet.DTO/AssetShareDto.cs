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
        public decimal Total
        {
            get
            {
                return this.AssetComponents.Select(x => x.Value).Sum();
            }
        }
        [DataMember]
        public List<AssetComponentDto> AssetComponents { get; set; }

    }
}
