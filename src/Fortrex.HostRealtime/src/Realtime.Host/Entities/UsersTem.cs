using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UsersTem
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string FullName { get; set; }
    }
}
