using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class LogStatus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal BeforeTrx { get; set; }
        public decimal NowTrx { get; set; }
        public string Description { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
