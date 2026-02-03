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
        public List<AssetDto> Assets { get; set; }
        [DataMember]
        public List<LiabilityDto> Liabilities { get; set; }
        [DataMember]
        public List<AssetShareDto> AssetShares { get; set; }
        [DataMember]
        public CoinSummaryDto Coins { get; set; }
        [DataMember]
        public List<TargetComparisonDto> TargetComparisons { get; set; }
        [DataMember]
        public decimal TotalAssets { get; set; }
        [DataMember]
        public decimal TotalLiabilities { get; set; }
        [DataMember]
        public decimal NetWorth {  get; set; }
    }
}
