using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserRoleMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }
    }
}
