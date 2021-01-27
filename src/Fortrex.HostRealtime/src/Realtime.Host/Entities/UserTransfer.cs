using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserTransfer
    {
        public int Id { get; set; }
        public int FromUid { get; set; }
        public int ToUid { get; set; }
        public decimal Amount { get; set; }
        public int Status { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? ApplyOn { get; set; }
        public decimal ResFee { get; set; }
        public decimal ResAmount { get; set; }
        public string Type { get; set; }
    }
}
