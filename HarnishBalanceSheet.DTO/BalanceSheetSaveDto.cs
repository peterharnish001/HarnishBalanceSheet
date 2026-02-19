using System;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class BalanceSheetSaveDto
    {
        [DataMember]
        public IEnumerable<AssetSaveDto> Assets { get; set; }
        [DataMember]
        public IEnumerable<MetalPositionSaveDto> Bullion { get; set; }
        [DataMember]
        public IEnumerable<LiabilityDto> Liabilities { get; set; }
    }
}
