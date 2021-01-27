using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserIgnore
    {
        public int UserId { get; set; }
        public DateTime IgnoreDate { get; set; }
    }
}
