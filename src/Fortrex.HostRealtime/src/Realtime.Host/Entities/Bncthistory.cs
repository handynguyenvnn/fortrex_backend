using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Bncthistory
    {
        public int Id { get; set; }
        public int Uid { get; set; }
        public decimal Coin { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
