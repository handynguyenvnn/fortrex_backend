using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Domain.Trade
{

    public enum InterventionSystem
    {
        FAIR = 1,
        CROWD_WIN = 2,
        PREDETERMINED = 3,
        SMALL_WIN = 4
    }


    public partial class Candlesticks
    {

        public string TimeOpen { get; set; }
        public string TimeClose { get; set; }
        public decimal? Open { get; set; }

        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public decimal? VolumeFrom { get; set; }

        public decimal? VolumeTo { get; set; }

        public decimal? Close { get; set; }

        //public string ConversionType { get; set; }

        //public string ConversionSymbol { get; set; }

        public string PairName { get; set; }
    }
    public partial class KlineCandlesticks
    {
        public long? Id { get; set; }
        public string TimeOpen { get; set; }
        public string TimeClose { get; set; }
        public decimal? Open { get; set; }

        public decimal? High { get; set; }

        public decimal? Low { get; set; }

        public decimal? VolumeFrom { get; set; }

        public decimal? VolumeTo { get; set; }

        public decimal? Close { get; set; }

        public string ConversionType { get; set; }

        public string ConversionSymbol { get; set; }

        public string PairName { get; set; }
        public string IntervalValue { get; set; }
        public decimal PriceChangePercent { get; set; }
        public long PreviousCandleId { get; set; }
        public string Times { get; set; }
    }
    public partial class KlineCandlesticksInterval
    {
        public decimal Id { get; set; }
        public string IntervalValue { get; set; }
    }
    public partial class KlineCandlesticksApiResponse
    {
        public bool status { get; set; }
        public string code { get; set; }
        public string msg { get; set; }
        public List<CandlesticksResponse> response { get; set; }
    }
    public partial class CandlesticksResponse
    {
        public decimal o { get; set; }
        public decimal h { get; set; }
        public decimal l { get; set; }
        public decimal c { get; set; }
        public decimal? v { get; set; }
        public decimal t { get; set; }
        public DateTime tm { get; set; }
    }
    public partial class OpeningOrderGroupName
    {
        public string MarketName { get; set; }
    }
    public partial class OpeningOrders
    {
        public string MarketName { get; set; }
        public bool ISCALL { get; set; }
        public decimal AMOUNT { get; set; }
        /// <summary>
        /// 0: không can thiệp giá, 1: thắng, 2: Thua
        /// </summary>
        public int IsWin { get; set; }
    }

    public class RootTradeModel
    {
        public int Id { get; set; }
        public string PairName { get; set; }
        public decimal LeftAmount { get; set; }
        public decimal RightAmount { get; set; }
        public int UserLeft { get; set; }
        public int UserRignt { get; set; }
        public decimal TradeLeft { get; set; }
        public decimal TradeRight { get; set; }
    }

    public class RobotModel
    {
        public decimal OpenPrice { get; set; }
        public decimal LastPrice { get; set; }
    }
}
