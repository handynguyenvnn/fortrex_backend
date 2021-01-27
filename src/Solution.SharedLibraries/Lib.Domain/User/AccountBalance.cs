using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    [DataContract()]
    public class AccountBalance
    {
        [DataMember(Name = "Balance", Order = 0)]
        public decimal Balance { get; set; }
        [DataMember(Name = "BalanceFormat", Order = 0)]
        public string BalanceFormat { 
            get { return string.Format("{0:n}", Balance); }
        }
        [DataMember(Name = "WalletName", Order = 0)]
        public string WalletName { get; set; }
        [DataMember(Name = "WalletCode", Order = 0)]
        public string WalletCode { get; set; }
        [DataMember(Name = "WalletDefault", Order = 0)]
        public bool WalletDefault { get; set; }
    }
}
