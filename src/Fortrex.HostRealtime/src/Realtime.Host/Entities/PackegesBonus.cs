using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class PackegesBonus
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Invested { get; set; }
        public bool IsProfit { get; set; }
        public decimal SharePercent { get; set; }
        public decimal SharePrice { get; set; }
        public decimal ShareTotal { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime StartProfitDate { get; set; }
        public bool? IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
        public string Type { get; set; }
        public decimal TempStock { get; set; }
        public decimal TempProfit { get; set; }
    }
}
