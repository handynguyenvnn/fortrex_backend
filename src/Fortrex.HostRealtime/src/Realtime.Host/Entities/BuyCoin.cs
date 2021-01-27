using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class BuyCoin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal NumberCoin { get; set; }
        public decimal OriginUsd { get; set; }
        public decimal PriceUsd { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? UpdateDate { get; set; }
        public int Status { get; set; }
        public string Transaction { get; set; }
        public int? ApproveBy { get; set; }
        public DateTime? ApproveDate { get; set; }
        public int? MethodPaymentId { get; set; }
    }
}
