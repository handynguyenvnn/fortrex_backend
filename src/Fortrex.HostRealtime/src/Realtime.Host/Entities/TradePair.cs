using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TradePair
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public string Fsym { get; set; }
        public string Tsym { get; set; }
        public decimal? FeeTrade { get; set; }
        public bool? IsActive { get; set; }
        public bool? IsDelete { get; set; }
    }
}
