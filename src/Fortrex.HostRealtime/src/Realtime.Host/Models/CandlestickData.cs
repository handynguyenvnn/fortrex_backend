using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AspNetCoreSignalR_React.Server.Models
{
    public class CandlestickData
    {
        public int Id { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal VolumeFrom { get; set; }
        public decimal VolumeTo { get; set; }
        public decimal ClosePrice { get; set; }
        public string ConversionType { get; set; }
        public string ConversionSymbol { get; set; }
        public string PairName { get; set; }
        public long TimeClose { get; set; }
        public long TimeOpen { get; set; }
    }
}
