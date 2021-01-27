using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserWallet
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string WalletAddress { get; set; }
        public decimal Amount { get; set; }
        public decimal LastAmount { get; set; }
        public string WalletType { get; set; }
    }
}
