using System;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class MetalPositionSaveDto
    {
        [DataMember]
        public int? MetalPositionId { get; set; }
        [DataMember]
        public int PreciousMetalId { get; set; }
        [DataMember]
        public decimal NumOunces { get; set; }
        [DataMember]
        public decimal PricePerOunce { get; set; }
    }
}
