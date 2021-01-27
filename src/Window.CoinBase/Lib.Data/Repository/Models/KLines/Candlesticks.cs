using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.KLines
{
    
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
        public DateTime Times { get; set; }
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
}
