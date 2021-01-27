using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models.TronCoins
{
    public class TronCoin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Key { get; set; }
        public string Address { get; set; }
        public decimal Balance { get; set; }
    }
}
