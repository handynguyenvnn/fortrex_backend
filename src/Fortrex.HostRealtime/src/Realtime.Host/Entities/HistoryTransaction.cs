using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class HistoryTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal? Amount { get; set; }
        public int? FromUserId { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public int Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string CoinBaseTransactionId { get; set; }
        public decimal? DailyRoi { get; set; }
    }
}
