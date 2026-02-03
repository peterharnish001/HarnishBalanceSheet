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
        public List<AssetEditDto> Assets { get; set; }
        [DataMember]
        public List<LiabilityDto> Liabilities { get; set; }
    }
}
