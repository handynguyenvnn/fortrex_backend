using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Dblog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Message { get; set; }
        public DateTime CreateOn { get; set; }
        public int? ReferentId { get; set; }
        public int? Type { get; set; }
    }
}
