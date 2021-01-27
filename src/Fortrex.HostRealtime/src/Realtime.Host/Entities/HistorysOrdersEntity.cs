using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class HistorysOrdersEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Pair { get; set; }
        public string Symbol { get; set; }
        public string Side { get; set; }
        public decimal Amount { get; set; }
        public decimal Filled { get; set; }
        public decimal FilledPercent { get; set; }
        public decimal Total { get; set; }
        public decimal Price { get; set; }
        public decimal Fee { get; set; }
        public DateTime Date { get; set; }
        public int Status { get; set; }
    }
}
