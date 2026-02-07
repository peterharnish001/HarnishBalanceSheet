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

        public override bool Equals(object? obj)
        {
            var item = obj as BalanceSheetDto;
            
            if (item == null) return false; 
            return this.BalanceSheetId.Equals(item.BalanceSheetId) && this.Date.Equals(item.Date);
        }
    }
}
