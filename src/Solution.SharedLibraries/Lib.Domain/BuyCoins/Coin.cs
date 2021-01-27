using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.BuyCoins
{
    public class Coin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal MoneyBTC { get; set; }
        public decimal MoneyETH { get; set; }
        public decimal MoneyBEH { get; set; }
        public decimal NumberCoin { get; set; }
        public decimal Amount { get; set; }
        public bool? EnableSellCoin { get; set; }
        public string SellCoinDay { get; set; }   
        public decimal PriceBTCToBEH { get; set; }
        public decimal PriceEHToBEH { get; set; }
        public decimal BEHToUSD { get; set; }
        public string DayNow { get; set; }
    }
}
