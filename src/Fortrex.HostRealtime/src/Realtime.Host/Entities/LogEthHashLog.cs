using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class LogEthHashLog
    {
        public int Id { get; set; }
        public decimal UserId { get; set; }
        public string TxHash { get; set; }
        public int Timestamp { get; set; }
    }
}
