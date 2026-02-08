using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class LiabilityDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Value { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as LiabilityDto;

            if (item == null) return false;
            return this.Name.Equals(item.Name) && this.Value.Equals(item.Value);
        }
    }
}
