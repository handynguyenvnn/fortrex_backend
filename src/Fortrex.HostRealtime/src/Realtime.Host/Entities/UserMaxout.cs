using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserMaxout
    {
        public int UserId { get; set; }
        public DateTime MaxOutDate { get; set; }
    }
}
