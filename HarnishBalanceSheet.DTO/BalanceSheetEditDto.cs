using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class BalanceSheetEditDto
    {
        [DataMember]
        public int? BalanceSheetId { get; set; }
        [DataMember]
        public List<AssetDto> Assets { get; set; }
        [DataMember]
        public List<LiabilityDto> Liabilities { get; set; }
        [DataMember]
        public List<MetalDto> Bullion { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as BalanceSheetEditDto;

            if (item == null) return false;

            return this.BalanceSheetId.Equals(item.BalanceSheetId)
                && this.Assets.SequenceEqual(item.Assets)
                && this.Liabilities.SequenceEqual(item.Liabilities)
                && this.Bullion.SequenceEqual(item.Bullion)
                ;
        }
    }
}
