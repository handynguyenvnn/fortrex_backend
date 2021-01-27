using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class MailUserMining
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Bonus { get; set; }
        public int Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime NextTimeOn { get; set; }
        public bool IsFinish { get; set; }
    }
}
