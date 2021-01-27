using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TblTempPayProfitDaily
    {
        public int Id { get; set; }
        public int Userid { get; set; }
        public int Status { get; set; }
        public string WalletEth { get; set; }
        public DateTime? CreatePay { get; set; }
        public decimal? Amount { get; set; }
        public decimal? TotalInvest { get; set; }
        public decimal? AmountBeforeaDay { get; set; }
        public string Txhash { get; set; }
        public bool? ApprovedbyAdmin { get; set; }
        public string ByUser { get; set; }
        public string Descriptions { get; set; }
        public decimal? AmountUsd { get; set; }
    }
}
