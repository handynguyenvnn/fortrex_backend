using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class WalletErc20ContractAddressMapping
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public string CoinSymbol { get; set; }
        public string CoinContract { get; set; }
        public string CoinAddress { get; set; }
        public string Type { get; set; }
    }
}
