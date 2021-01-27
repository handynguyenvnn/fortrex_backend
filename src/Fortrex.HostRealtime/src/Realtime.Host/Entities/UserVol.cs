using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserVol
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal TotalTrade { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime UpdateOn { get; set; }
    }
}
