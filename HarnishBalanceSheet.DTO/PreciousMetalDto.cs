using System;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class PreciousMetalDto
    {
        [DataMember]
        public int PreciousMetalId { get; set; }
        [DataMember]
        public string Name { get; set; } 
    }
}
