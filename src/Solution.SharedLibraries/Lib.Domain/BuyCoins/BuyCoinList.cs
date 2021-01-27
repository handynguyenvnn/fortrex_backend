using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.BuyCoins
{
    public class BuyCoinList
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Username { get; set; }
        public decimal NumberCoin { get; set; }
        public decimal PriceUSD { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Status { get; set; }
        public decimal BEHToUSD { get; set; }
        public string StatusName { get; set; }
        public string ApproveByName { get; set; }
        public DateTime? ApproveDate { get; set; }
        public string Transaction { get; set; }
    }
}
