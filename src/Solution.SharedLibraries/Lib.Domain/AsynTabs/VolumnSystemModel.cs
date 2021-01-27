using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.AsynTabs
{
    public class VolumnSystemModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int WeekDay { get; set; }
        public int VolumnSystem { get; set; }
        public int IsProcess { get; set; }
        public decimal MasterIB { get; set; }
        public int LevelId { get; set; }
    }

    public class ProcessLevelData
    {
        public int UserId { get; set; }
        public int LevelId { get; set; }
        public int TotalF1 { get; set; }
        public int TotalVolumn { get; set; }
    }

    public class TotalUserTrade
    {
        public int UserId { get; set; }
        public int TotalTrade { get; set; }
    }
    public class TradingLastResult
    {
        public TradingLastResult()
        {
            _Up = 37;
            _Down = 63;
            _1HourAgo = 40;
            _15MinAgo = 60;
        }

        public int _Up { get; set; }
        public int _Down { get; set; }
        public int _1HourAgo { get; set; }
        public int _15MinAgo { get; set; }
    }
}
