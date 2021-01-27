using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class CoinList
    {
        public int Id { get; set; }
        public string CoinName { get; set; }
        public string CoinSymbol { get; set; }
        public string CoinContract { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
        public decimal Decimals { get; set; }
        public string TypeCoin { get; set; }
    }
}
