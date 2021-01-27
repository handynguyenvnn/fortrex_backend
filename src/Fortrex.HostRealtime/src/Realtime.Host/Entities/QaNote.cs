using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class QaNote
    {
        public int Id { get; set; }
        public string Note { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreateDate { get; set; }
        public int UserId { get; set; }
        public bool IsDelete { get; set; }
    }
}
