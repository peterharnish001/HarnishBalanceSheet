using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace HarnishBalanceSheet.DTO
{
    [DataContract]
    public class TargetComparisonDto
    {
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public decimal Target {  get; set; }
        [DataMember]
        public decimal Actual {  get; set; }
        [DataMember]
        public decimal Difference { get; set; }
    }
}
