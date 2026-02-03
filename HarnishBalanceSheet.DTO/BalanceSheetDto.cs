using System.Runtime.Serialization;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class BalanceSheetDto
    {
        [DataMember]
        public int BalanceSheetId {  get; set; }
        [DataMember]
        public DateTime Date {  get; set; }
    }
}
