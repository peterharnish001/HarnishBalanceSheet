using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class DetailsDto
    {
        [DataMember]
        public DateTime Date { get; set; }
        [DataMember]
        public List<AssetDto> Assets { get; set; }
        [DataMember]
        public IEnumerable<LiabilityDto> Liabilities { get; set; }
        [DataMember]
        public IEnumerable<AssetShareDto> AssetShares { get; set; }
        [DataMember]
        public BullionSummaryDto BullionSummary { get; set; }
        [DataMember]
        public List<TargetComparisonDto> TargetComparisons { get; set; }
        [DataMember]
        public decimal TotalAssets { get; set; }
        [DataMember]
        public decimal TotalLiabilities { get; set; }
        [DataMember]
        public decimal NetWorth {  get; set; }
        [DataMember]
        public IEnumerable<string> AssetTypes { get; set; }
        [DataMember]
        public IEnumerable<TargetDto> Targets { get; set; }

        public override bool Equals(object? obj)
        {
            var item = obj as DetailsDto;

            if (item == null) return false;

            return this.Date.Equals(item.Date)
                && this.Assets.SequenceEqual(item.Assets)
                && this.Liabilities.SequenceEqual(item.Liabilities)
                && this.BullionSummary.Equals(item.BullionSummary)
                && this.AssetTypes.SequenceEqual(item.AssetTypes)
                ;
        }
       
    }
}
