using System.Runtime.Serialization;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class SetTargetDto
    {
        [DataMember]
        public int AssetCategoryId { get; set; }
        [DataMember]
        public decimal Percentage { get; set; }
    }
}
