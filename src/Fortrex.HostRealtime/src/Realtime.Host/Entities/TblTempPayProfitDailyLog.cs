using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TblTempPayProfitDailyLog
    {
        public int Id { get; set; }
        public string Userid { get; set; }
        public string WalletEth { get; set; }
        public DateTime? CreatePay { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalInvest { get; set; }
        public decimal? AmountBeforeaDay { get; set; }
    }
}
