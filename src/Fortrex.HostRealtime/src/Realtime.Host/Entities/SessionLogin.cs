using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class SessionLogin
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public DateTime CreateOn { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
