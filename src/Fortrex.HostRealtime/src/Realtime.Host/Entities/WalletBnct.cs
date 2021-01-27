using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class WalletBnct
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CoinName { get; set; }
        public string CoinSymbol { get; set; }
        public string CoinContract { get; set; }
        public string CoinAddress { get; set; }
        public string CoinPrivateKey { get; set; }
        public string CoinPublicKey { get; set; }
    }
}
