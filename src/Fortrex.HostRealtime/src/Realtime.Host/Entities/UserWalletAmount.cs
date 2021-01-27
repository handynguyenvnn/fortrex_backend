using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserWalletAmount
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount40 { get; set; }
        public decimal Amount60 { get; set; }
        public int PackageId { get; set; }
        public bool IsTransferXrp { get; set; }
        public decimal AmountXrp { get; set; }
    }
}
