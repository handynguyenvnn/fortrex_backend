using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.Packages
{
    public class CoinPriceSync
    {
        public Int64 Id { get; set; }
        public string FromCoin { get; set; }
        public string ToCoin { get; set; }
        public decimal Price { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
