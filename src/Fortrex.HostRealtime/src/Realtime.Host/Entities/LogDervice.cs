using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class LogDervice
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Ipaddress { get; set; }
        public string UserAgent { get; set; }
        public string CreateOn { get; set; }
        public string Status { get; set; }
    }
}
