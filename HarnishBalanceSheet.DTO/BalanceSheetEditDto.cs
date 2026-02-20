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
        public DateTime Date {  get; set; }
        [DataMember]
        public List<AssetSaveDto> Assets { get; set; }
        [DataMember]
        public List<LiabilityDto> Liabilities { get; set; }
        [DataMember]
        public List<MetalDto> Bullion { get; set; }
        [DataMember]
        public List<AssetTypeDto> AssetTypes { get; set; }
        [DataMember]
        public List<PreciousMetalDto> PreciousMetals { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as BalanceSheetEditDto;

            if (item == null) return false;

            return this.Assets.SequenceEqual(item.Assets)
                && this.Liabilities.SequenceEqual(item.Liabilities)
                && this.Bullion.SequenceEqual(item.Bullion)
                ;
        }
    }
}
