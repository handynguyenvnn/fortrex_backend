using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserLockPending
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsLock { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
