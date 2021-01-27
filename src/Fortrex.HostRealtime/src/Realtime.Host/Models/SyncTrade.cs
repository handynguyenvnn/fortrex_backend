
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages.Trades
{
    public class SyncTrade
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEncrypted { get; set; }
        public string MarketName { get; set; }
        public decimal BeginAmount { get; set; }
        public decimal EndAmount { get; set; }
        public string OpeningPrice { get; set; }
        public string ClosingPrice { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal Amount { get; set; }
        public bool IsCall { get; set; }
        public bool IsDelete { get; set; }
        public decimal Profit { get; set; }
        public int Status { get; set; }
        public int ByType { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime WaitingOn { get; set; }
        public DateTime CompleteOn { get; set; }
        public string PairName { get; set; }
        public string symbol { get; set; }
        public string CreateTimeStr { get; set; }
        public string CompleteOnStr { get; set; }
        public bool IsDemo { get; set; }

        public string _amount { get; set; }
        public string _iswin { get; set; }
        public string _isCall { get; set; }
        public string _profit { get; set; }
        public string _action { get; set; }
        public string _create_time { get; set; }
    }
    //[JsonObject(MemberSerialization.OptOut)]
    [DataContract()]
    public class ResponseTradings
    {
        [DataMember(Name = "Id", Order = 7)]
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserEncrypted { get; set; }
        //[JsonProperty("PairName")]
        //[JsonProperty(PropertyName = "PairName")]

        [DataMember(Name = "MarketName", Order = 0)]
        public string MarketName { get; set; }
        //[DataMember(Name = "BeginAmount", Order = 9)]
        public decimal BeginAmount { get; set; }
        //[DataMember(Name = "EndAmount", Order = 10)]
        public decimal EndAmount { get; set; }
        [DataMember(Name = "OpeningPrice", Order = 2)]

        public string OpeningPrice { get; set; }
        [DataMember(Name = "ClosingPrice", Order = 3)]

        public string ClosingPrice { get; set; }
        //[DataMember(Name = "CurrentPrice", Order = 4)]
        public decimal CurrentPrice { get; set; }
        
        [DataMember(Name = "Amount", Order = 5)]
        public decimal Amount { get; set; }
        
        [DataMember(Name = "Total", Order = 6)]
        public decimal Total { get; set; }
        
        [DataMember(Name = "Type", Order = 7)]
        public bool IsCall { get; set; }
        public bool IsDelete { get; set; }
        
        [DataMember(Name = "Filled", Order = 8)]
        public decimal Profit { get; set; }
        
        [DataMember(Name = "StatusName", Order = 10)]
        public string StatusName { get; set; }

        [DataMember(Name = "Status", Order = 9)]
        public int Status { get; set; }

        public DateTime CreateOn { get; set; }
        public DateTime WaitingOn { get; set; }
        public DateTime CompleteOn { get; set; }
        [DataMember(Name = "PairName", Order = 1)]
        public string PairName { get; set; }
        public string symbol { get; set; }
        [DataMember(Name = "CreateTime", Order = 11)]
        public string CreateTimeStr { get; set; }
        [DataMember(Name = "CompleteTime", Order = 12)]
        public string CompleteOnStr { get; set; }
        [DataMember(Name = "IsDemo", Order = 13)]
        public bool IsDemo { get; set; }

        public string _amount { get; set; }
        public string _iswin { get; set; }
        public string _isCall { get; set; }
        public string _profit { get; set; }
        public string _action { get; set; }
        public string _create_time { get; set; }
    }
    public class ResponseBookOrder
    {
        public ResponseBookOrder()
        {
            Result = -1;
            CurrentPrice = 0;
        }
        public int Result { get; set; }
        public decimal CurrentPrice { get; set; }
    }
    public class ResponsePrice
    {
        public decimal OPEN { get; set; }
        public decimal HIGH { get; set; }
        public decimal LOW { get; set; }
        public decimal CLOSE { get; set; }
        public decimal TIMES { get; set; }
        public decimal LASTTIME { get; set; }
        public decimal VolumeFrom { get; set; }
        public decimal VolumeTo { get; set; }
        public decimal PriceChangePercent { get; set; }
    }

    public class HighchartPrice
    {
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
    }
    public class Candlesticks
    {
        public string PairName { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public decimal Times { get; set; }
        public decimal TimeOpen { get; set; }
        public decimal TimeClose { get; set; }
        public decimal LastTimes { get; set; }
        public decimal VolumeFrom { get; set; }
        public decimal VolumeTo { get; set; }
        public decimal PriceChangePercent { get; set; }
    }
    public class RealTimeCandlestickData
    {
        public RealTimeCandlestickData()
        {
            PairName = "";
            OpenPrice = 0;
            HighPrice = 0;
            LowPrice = 0;
            TimeOpen = 0;
            ClosePrice = 0;
            TimeClose = 0;
            VolumeTo = 0;
        }

        public string PairName { get; set; }
        public decimal OpenPrice { get; set; }
        public decimal HighPrice { get; set; }
        public decimal LowPrice { get; set; }
        public decimal ClosePrice { get; set; }
        public decimal TimeOpen { get; set; }
        public decimal TimeClose { get; set; }
        public decimal VolumeTo { get; set; }
    }
    public class TickerPriceChange
    {
        public string PairSymbol { get; set; }
        public string PairName { get; set; }
        public decimal BidPrice { get; set; }
        public decimal AskPrice { get; set; }
        public decimal OpenPrice { get; set; }
        public int TradeWinPercent { get; set; }
        public decimal PriceChangePercent { get; set; }
        public string MarketType { get; set; }
        public int FormatDecimal { get; set; }

    }
  
    [DataContract()]
    public class TickerPriceChangeResponse
    {
        [DataMember(Name = "PairName", Order = 0)]
        public string PairName { get; set; }
        [DataMember(Name = "BidPrice", Order = 2)]
        public decimal BidPrice { get; set; }
        [DataMember(Name = "AskPrice", Order = 3)]
        public decimal AskPrice { get; set; }
        [DataMember(Name = "OpenPrice", Order = 1)]
        public decimal OpenPrice { get; set; }
        [DataMember(Name = "TradeWinPercent", Order = 4)]
        public int TradeWinPercent { get; set; }
        [DataMember(Name = "PriceChangePercent", Order = 5)]
        public decimal PriceChangePercent { get; set; }
        [DataMember(Name = "FormatDecimal", Order = 6)]
        public int FormatDecimal { get; set; }

    }
    public class User_PairName_Mapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PairName { get; set; }
    }
    public class TradeVolumeSync
    {
        public int VolumeBuy { get; set; }
        public int VolumeSell { get; set; }
    }
    public class Stock
    {
      //  private readonly decimal _price;

        public string Symbol { get; set; }

        public decimal DayOpen { get;  set; }

        public decimal DayLow { get;  set; }

        public decimal DayHigh { get;  set; }

        public decimal LastChange { get;  set; }

        public decimal Change { get;  set; }
       
        public decimal PercentChange { get; set; }
       
        public decimal Price { get; set; }
  
    }
    public class UserId_ConnectionId_Map
    {
        public int Userid { get; set; }
        public string ConnectionId { get; set; }
    }
}
