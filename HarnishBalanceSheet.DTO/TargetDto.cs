using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class TargetDto
    {
        [DataMember]
        public int TargetId { get; set; }
        [DataMember] public string TargetName { get; set; }
        [DataMember]
        public decimal Percentage { get; set; }
    }
}
