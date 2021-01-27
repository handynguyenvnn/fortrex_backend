using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class CoinTransactionClone
    {
        public int Id { get; set; }
        public int? MethodPayment { get; set; }
        public string Type { get; set; }
        public string Status { get; set; }
        public decimal PriceCoin { get; set; }
        public decimal PriceUsd { get; set; }
        public string AddressWallet { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public string HashCode { get; set; }
        public string TransactionId { get; set; }
    }
}
