using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.BuyCoins
{
    public class BuyCoin
    {
        public BuyCoin()
        {
            UserId = 0;
            NumberCoin = 0;
            PriceUSD = 0;
            CreateDate = DateTime.Now;
            UpdateDate = DateTime.Now;
            BEHToUSD = 0;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal NumberCoin { get; set; }
        public decimal PriceUSD { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public decimal BEHToUSD { get; set; }
        public string Transaction { get; set; }
        public bool? EnableSellCoin { get; set; }
        public string SellCoinDay { get; set; }
        public int MethodPaymentId { get; set; }
    }
}
