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
        public string AssetCategory { get; set; }
        [DataMember]
        public int AssetTypeId { get; set; }
        [DataMember]
        public decimal Fraction {  get; set; }
        [DataMember]
        public decimal Value { get; set; }
        [DataMember]
        public string AssetName { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as AssetComponentDto;

            if (item == null) return false;
            return this.AssetCategory.Equals(item.AssetCategory) 
                && this.AssetComponentId.Equals(item.AssetComponentId)
                && this.Value.Equals(item.Value)
                ;
        }
    }
}
