using System;
using System.Threading.Tasks;
using TableDependency.SqlClient;
using Microsoft.AspNetCore.SignalR;
using SignalRCore.Web.Repository;
using AspNetCoreSignalR_React.Server.Models;
using TableDependency.SqlClient.Base.Enums;
using TableDependency.SqlClient.Base.EventArgs;
using Lib.Domain.Packages.Trades;
using Web.SourceCoin.Helpers;
using RealtimeRealtimeDatabaseSubscriptionSubscription.Web;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using Lib.Tasks;

namespace RealtimeRealtimeDatabaseSubscriptionSubscription.Hubs
{
   
    public class RealtimeDatabaseSubscription : IDatabaseSubscription
    {
        private bool disposedValue = false;
        public  string pairname { get; set; }
        public List<UserId_ConnectionId_Map> userIds { get; set; }
        //public List<string> _connectionIds { get; set; }
        private readonly IInventoryRepository _repository;
        private readonly IHubContext<RealtimeHub> _hubContext;
        private SqlTableDependency<CandlestickData> _tableDependency_ins;
        private SqlTableDependency<CandlestickData> _tableDependency_upd;
        private SqlTableDependency<SyncTrade> _tableTradingHistory;
        private readonly Helper _helper;
        //private SqlTableDependency<TickerPriceChange> _tableTickerPrice;
        public RealtimeDatabaseSubscription(IInventoryRepository repository
            , IHubContext<RealtimeHub> hubContext)
        {
           Console.WriteLine("Started...");
            _repository = repository;
            _hubContext = hubContext;
            userIds = new List<UserId_ConnectionId_Map>();
            //_connectionIds =  new List<string>();
            _helper = new Helper();
            Task.Run(ServerTime);
        }
       
        private async void ServerTime()
        {
            while (true)
            {
                int second = DateTime.Now.Second;

                //await _hubContext.Clients.Clients(userIds.Select(s=>s.ConnectionId).ToList()).SendAsync("ServerTime", second);
                await _hubContext.Clients.All.SendAsync("ServerTime", second);
                System.Threading.Thread.Sleep(999);
            }
        }
        public void Configure(string connectionString)
        {
            //connectionString = ConnectionStrings.DbConnection;
            //realtime chart
            _tableDependency_ins = new SqlTableDependency<CandlestickData>(connectionString, "CandlestickData", null, null, null,null, DmlTriggerType.Insert);
            _tableDependency_ins.OnChanged += Invesrt_Chart;
            _tableDependency_ins.OnError += TableDependency_Ins_OnError;
            _tableDependency_ins.Start();

            _tableDependency_upd = new SqlTableDependency<CandlestickData>(connectionString, "CandlestickData", null, null, null,null, DmlTriggerType.Update);
            _tableDependency_upd.OnChanged += Update_Chart;
            _tableDependency_upd.OnError += TableDependency_Update_OnError;
            _tableDependency_upd.Start();
            // ticker price
            //_tableTickerPrice = new SqlTableDependency<TickerPriceChange>(connectionString, "TickerPriceChange", null, null, null, null, DmlTriggerType.Update);
            //_tableTickerPrice.OnChanged += SqlTableDependency_TickerPriceChange_Changed;
            //_tableTickerPrice.OnError += TableDependency_OnError;
            //_tableTickerPrice.Start();
            // History trading
            _tableTradingHistory = new SqlTableDependency<SyncTrade>(connectionString, "Trades", null, null, null, null, DmlTriggerType.Update);
            _tableTradingHistory.OnChanged += SqlTableDependency_TradingHistory_Changed;
            _tableTradingHistory.OnError += TableTradingHistory_OnError;
            _tableTradingHistory.Start();
        }

        private void TableDependency_Ins_OnError(object sender, ErrorEventArgs e)
        {
             Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
            //_tableDependency_ins.Stop();
            //_tableDependency_ins.Start();
        }
        private void TableDependency_Update_OnError(object sender, ErrorEventArgs e)
        {
             Console.WriteLine($"SqlTableDependency error: {e.Error.Message}");
            //_tableDependency_upd.Stop();
            //_tableDependency_upd.Start();
        }
        private void TableTradingHistory_OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine($"_tableTradingHistory error: {e.Error.Message}");
            //_tableTradingHistory.Stop();
            //_tableTradingHistory.Start();
        }
        //private void Changed(object sender, RecordChangedEventArgs<CandlestickData> e)
        //{
        //    if (e.ChangeType != ChangeType.Delete)
        //    {
        //        var changedEntity = e.Entity;
        //        var pushClients = userIds.Where(p => p.PairName == (changedEntity.PairName)).Select(s => s.ConnectionId).ToList() ;
        //        if (pushClients!=null)
        //        {
        //            //_hubContext.Clients.All.InvokeAsync(changedEntity.PairName, changedEntity);
        //            //_hubContext.Clients.All.SendAsync("CHART_" + changedEntity.PairName, changedEntity);
        //            switch (e.ChangeType)
        //            {
        //                case ChangeType.Insert:
        //                    //_hubContext.Clients.Clients(pushClients).SendAsync("NEW_CHART_" + changedEntity.PairName, changedEntity);
        //                    _hubContext.Clients.All.SendAsync("NEW_CHART_" + changedEntity.PairName, changedEntity);
        //                    break;
        //                case ChangeType.Update:
        //                    //_hubContext.Clients.Clients(pushClients).SendAsync("CHART_" + changedEntity.PairName, changedEntity);
        //                    _hubContext.Clients.All.SendAsync("CHART_" + changedEntity.PairName, changedEntity);
        //                    break;
        //                default:
        //                    break;
        //            }

        //            //Console.WriteLine("_pairname: " + changedEntity.PairName+" - " + changedEntity.ClosePrice);
        //        }
        //    }

        //}
        private void Invesrt_Chart(object sender, RecordChangedEventArgs<CandlestickData> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Insert:
                    var changedEntity = e.Entity;
                    //var pushClients = userIds.Where(p => p.PairName == (changedEntity.PairName)).Select(s => s.ConnectionId).ToList();
                    //var pushClients = userIds.Select(s => s.ConnectionId).ToList();
                    //_hubContext.Clients.Clients(pushClients).SendAsync("NEW_CHART_" + changedEntity.PairName, changedEntity);
                    //_hubContext.Clients.All.SendAsync("NEW_CHART_" + changedEntity.PairName, JsonConvert.SerializeObject(changedEntity));
                    _hubContext.Clients.All.SendAsync("NEW_CHART_" + changedEntity.PairName, changedEntity);
                    break;
                default:
                    break;
            }
        }
        private void Update_Chart(object sender, RecordChangedEventArgs<CandlestickData> e)
        {
            switch (e.ChangeType)
            {
                case ChangeType.Update:
                    var changedEntity = e.Entity;
                    //var pushClients = userIds.Where(p => p.PairName == (changedEntity.PairName)).Select(s => s.ConnectionId).ToList();
                    //var pushClients = userIds.Select(s => s.ConnectionId).ToList();
                    //_hubContext.Clients.Clients(pushClients).SendAsync("CHART_" + changedEntity.PairName, changedEntity);
                    _hubContext.Clients.All.SendAsync("CHART_" + changedEntity.PairName, changedEntity);
                    break;
                default:
                    break;
            }
        }
        #region Update TradingHistory 
        private void SqlTableDependency_TradingHistory_Changed(object sender, RecordChangedEventArgs<SyncTrade> e)
        {
            try
            {
                if (e.Entity.UserId > 0)
                {

                    // var color = e.Entity.Status == 1 ? "green" : e.Entity.Status == -1 ? "red" : "";

                    e.Entity.Id = e.Entity.Id;
                    e.Entity.MarketName = e.Entity.MarketName;
                    //e.Entity._create_time = e.Entity.CreateOn.ToString("yyyy-dd-MM HH:mm:ss");
                    //e.Entity._amount = _helper.FormatNumber(e.Entity.Amount);
                    //e.Entity._isCall = e.Entity.IsCall ? "<font color='" + color + "'>CALL</font>" : "<font color='" + color + "'>PUT</font>";
                    //e.Entity._iswin = e.Entity.Status == 1 ? "<font color='" + color + "'>WIN</font>" : e.Entity.Status == -1 ? "<font color='" + color + "'>LOSE</font>" : "--";
                    //e.Entity._profit = _helper.FormatNumber(e.Entity.Profit);
                    var connectionIds = userIds.Where(p => p.Userid == (e.Entity.UserId)).Select(s => s.ConnectionId).ToArray();
                    if (connectionIds != null)
                    {
                        switch (e.ChangeType)
                        {
                            case ChangeType.Delete:
                                 _hubContext.Clients.Clients(connectionIds).SendAsync("RemoveTrade", (e.Entity));
                                //_hubContext.Clients.All.SendAsync("RemoveTrade", (e.Entity));
                                break;
                            case ChangeType.Insert:
                                 _hubContext.Clients.Clients(connectionIds).SendAsync("AddTrade", (e.Entity));
                                //_hubContext.Clients.Clients(connectionIds).SendAsync("LastResult", (e.Entity));
                                //_hubContext.Clients.All.SendAsync("AddTrade", (e.Entity));
                                break;
                            case ChangeType.Update:
                                _hubContext.Clients.Clients(connectionIds).SendAsync("ResultTrade", (e.Entity));
                                //_hubContext.Clients.All.SendAsync("ResultTrade", (e.Entity));
                                break;
                        }
                    }

                }
            }
            catch(Exception ex)
            {
                LibraryLog.WriteErrorLog("Result Trade: " + ex.Message + "- " + JsonConvert.SerializeObject(e.Entity));
            }
          

        }

        #endregion
        #region Update TickerPriceChange 
        //private void SqlTableDependency_TickerPriceChange_Changed(object sender, RecordChangedEventArgs<TickerPriceChange> e)
        //{
        //    switch (e.ChangeType)
        //    {
        //        case ChangeType.Update:
        //            TickerPriceChangeResponse ticker = new TickerPriceChangeResponse();
        //            ticker.PairName = e.Entity.PairName;
        //            ticker.AskPrice = e.Entity.AskPrice;
        //            ticker.BidPrice = e.Entity.BidPrice;
        //            ticker.OpenPrice = e.Entity.OpenPrice;
        //            ticker.PriceChangePercent = e.Entity.PriceChangePercent;
        //            ticker.TradeWinPercent = e.Entity.TradeWinPercent;
        //            ticker.FormatDecimal = e.Entity.FormatDecimal;
        //            //_hubContext.Clients.All.SendAsync("TickerPrice", ticker);
        //            _hubContext.Clients.Clients(userIds.Select(s=> s.ConnectionId).ToList()).SendAsync("TickerPrice", ticker);
        //            break;
        //    }
        //}

        #endregion
        #region IDisposable

        ~RealtimeDatabaseSubscription()
        {
            Dispose(false);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency_ins.Stop();
                    _tableDependency_upd.Stop();
                    _tableTradingHistory.Stop();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
