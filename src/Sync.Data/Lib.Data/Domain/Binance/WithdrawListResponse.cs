using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Lib.Domain.Coins
{
    [DataContract]
    public class WithdrawListResponse 
    {
        [DataMember(Order = 1)]
        public List<WithdrawListItem> WithdrawList { get; set; }

        [DataMember(Order = 2)]
        public bool Success { get; set; }
    }
}