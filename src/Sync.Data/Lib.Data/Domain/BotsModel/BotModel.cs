using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Domain.BotsModel
{
    public class BotModel
    {
    }
    public class TickerPriceChange
    {
        public string PairName { get; set; }
        public decimal LastPrice { get; set; }
        public int TradeWinPercent { get; set; }
    }
    public class Random_Orders_WinLose
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public decimal RandomPrice { get; set; }
        public bool TypeRandom { get; set; }
        public decimal MatchingPrice { get; set; }
        public bool IsActive { get; set; }
    }
}
