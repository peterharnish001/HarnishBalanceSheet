using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetDto
    {
        [DataMember]
        public int? AssetId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Value { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as AssetDto;

            if (item == null) return false;
            return this.Name.Equals(item.Name) 
                && (this.AssetId == null || this.AssetId.Equals(item.AssetId))
                ;
        }
    }
}
