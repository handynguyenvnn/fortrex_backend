using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UsersMarketingBonus
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
    }
}
