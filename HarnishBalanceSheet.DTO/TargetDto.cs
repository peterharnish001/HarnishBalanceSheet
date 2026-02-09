using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class TargetDto
    {
        [DataMember]
        public int TargetId { get; set; }
        [DataMember]
        public string TargetName { get; set; }
        [DataMember]
        public int AssetCategoryId { get; set; }
        [DataMember]
        public decimal Percentage { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as TargetDto;

            if (item == null) return false;

            return this.TargetName.Equals(item.TargetName) 
                && this.Percentage.Equals(item.Percentage)
                ;
        }
    }
}
