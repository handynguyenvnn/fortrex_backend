using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class SysnDataTab
    {
        public long Id { get; set; }
        public int? Status { get; set; }
        public string ExtraData { get; set; }
        public DateTime? CreateOn { get; set; }
    }
}
