using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TransactionCoinMethodPayment
    {
        public int Id { get; set; }
        public string Symbols { get; set; }
        public string Name { get; set; }
        public string Descriptions { get; set; }
    }
}
