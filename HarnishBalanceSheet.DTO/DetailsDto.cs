using System;
using System.Collections.Generic;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    public class DetailsDto
    {
        public List<AssetDto> Assets { get; set; }
        public List<LiabilityDto> Liabilities { get; set; }
        public List<AssetShareDto> AssetShares { get; set; }
        public List<CoinDto> Coins { get; set; }
        public List<TargetComparisonDto> TargetComparisons { get; set; }
        public decimal TotalAssets { get; set; }
        public decimal TotalLiabilities { get; set; }
        public decimal NetWorth {  get; set; }
    }
}
