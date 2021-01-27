using System.Runtime.Serialization;
using System.Collections.Generic;


namespace Lib.Domain.Coins
{
    [DataContract]
    public class DepositListResponse 
    {
        [DataMember(Order = 1)]
        public List<DepositListItem> DepositList { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }
    }
}