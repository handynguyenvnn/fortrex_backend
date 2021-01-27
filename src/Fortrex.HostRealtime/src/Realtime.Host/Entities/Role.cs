using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
