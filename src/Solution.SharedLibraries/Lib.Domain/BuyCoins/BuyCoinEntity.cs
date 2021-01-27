using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.BuyCoins
{
    public class BuyCoinEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal NumberCoin { get; set; }
        public decimal OriginUSD { get; set; }
        public decimal PriceUSD { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public int Status { get; set; }
        public string Transaction { get; set; }
    }
}
