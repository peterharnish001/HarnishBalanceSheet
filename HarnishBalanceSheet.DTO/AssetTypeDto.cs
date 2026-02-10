using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class AssetTypeDto
    {
        [DataMember]
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as AssetTypeDto;

            if (item == null) return false;
            return this.Name.Equals(item.Name)
                ;
        }
    }
}
