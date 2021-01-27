using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class UserWallet
    {
        public int UserId { get; set; }
        public string WalletBTC { get; set; }
        public string WalletETH { get; set; }
        public string Username { get; set; }
    }
}
