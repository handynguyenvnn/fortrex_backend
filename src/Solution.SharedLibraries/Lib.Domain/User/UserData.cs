using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public class UserData
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public decimal PriceCoin { get; set; }
        public decimal MoneyBTC { get; set; }
        public decimal MoneyETH { get; set; }
        public DateTime? CreateDate { get; set; }
        public int Status { get; set; }
        public decimal MoneyTRX { get; set; }
        public int TronId { get; set; }
        public decimal Balance { get; set; }
        public int? StatusEx { get; set; }
        public decimal TotalDeposit { get; set; }
        public bool IsLock { get; set; }
        public string FA2Code { get; set; }
        public string ParrentName { get; set; }
        public string TreeName { get; set; }
    }
}