using Lib.Domain.Packages.Trades;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Configuration;
using System.Data.SqlClient;
using TableDependency.Enums;
using TableDependency.EventArgs;
using TableDependency.SqlClient;
using Web.SourceCoin.Common;

namespace Web.SourceCoin.Helpers
{
    public class JobInfoRepository : IDisposable
    {
        private readonly static Lazy<JobInfoRepository> _instance = new Lazy<JobInfoRepository>(() => new JobInfoRepository(GlobalHost.ConnectionManager.GetHubContext<JobHub>().Clients));
        public static JobInfoRepository Instance => _instance.Value;
        private SqlTableDependency<HighchartSyncTrade> _tableDependency { get; }
        private SqlTableDependency<TradeVolumeSync> _tableTradeVolume { get; }
        //private SqlTableDependency<RealTimeCandlestickData> _tableRealTimeCandlestickData { get; }
        private int _serverTime { get; }
        public string userid { get; set; }
        //public string pair { get; set; }
        private IHubConnectionContext<dynamic> Clients { get; }
        private readonly string syncConnect = ConfigurationManager.ConnectionStrings["Web.SourceCoin.Sync"].ConnectionString;
        private JobInfoRepository(IHubConnectionContext<dynamic> clients)
        {
            Clients = clients;
            _tableDependency = new SqlTableDependency<HighchartSyncTrade>(syncConnect, "Trades");
            _tableDependency.OnChanged += SqlTableDependency_Changed;
            _tableDependency.OnError += SqlTableDependency_OnError;
            _tableDependency.Start();

            // trade volume
            _tableTradeVolume = new SqlTableDependency<TradeVolumeSync>(syncConnect, "Random_VolumeBuySell");
            _tableTradeVolume.OnChanged += SqlTableDependency_Random_VolumeBuySell_Changed;
            _tableTradeVolume.OnError += SqlTableDependency_OnError;
            _tableTradeVolume.Start();

            //realtime chart
            //_tableRealTimeCandlestickData = new SqlTableDependency<RealTimeCandlestickData>(syncConnect, "CandlestickData");
            //_tableRealTimeCandlestickData.OnChanged += SqlTableDependency_Candlestick_Changed;
            //_tableRealTimeCandlestickData.OnError += SqlTableDependency_OnError;
            //_tableRealTimeCandlestickData.Start();
        }

        private void SqlTableDependency_OnError(object sender, ErrorEventArgs e)
        {
            throw e.Error;
        }

        private void SqlTableDependency_Changed(object sender, RecordChangedEventArgs<HighchartSyncTrade> e)
        {
            if (!string.IsNullOrEmpty(userid))
            {
                if ((e.Entity.UserId > 0 && userid.Equals(HelperCommon.CreateEncryptText(e.Entity.UserId.ToString()))))
                {
                    var _helper = new Helper();
                    var color = e.Entity.Status == 1 ? "green" : e.Entity.Status == -1 ? "red" : "";
                    e.Entity.Id = e.Entity.Id;
                    e.Entity.UserEncrypted = HelperCommon.CreateEncryptText(e.Entity.UserId.ToString());
                    e.Entity.MarketName = e.Entity.MarketName;
                    e.Entity._create_time = e.Entity.CreateOn.ToString("yyyy-MM-dd HH:mm:ss");
                    e.Entity._amount = _helper.FormatNumber(e.Entity.Amount);
                    e.Entity._isCall = e.Entity.IsCall ? "<font color='" + color + "'>BUY</font>" : "<font color='" + color + "'>SELL</font>";
                    e.Entity._iswin = e.Entity.Status == 1 ? "<font color='" + color + "'>WIN</font>" : e.Entity.Status == -1 ? "<font color='" + color + "'>LOSE</font>" : "--";
                    e.Entity._profit = _helper.FormatNumber(e.Entity.Profit);
                    e.Entity._action = "delete";
                    switch (e.ChangeType)
                    {
                        case ChangeType.Delete:
                            //Clients.All.removeTrade(e.Entity);
                            break;

                        case ChangeType.Insert:
                            Clients.All.addTrade(e.Entity);
                            break;

                        case ChangeType.Update:
                            Clients.All.updateTrade(e.Entity);
                            break;
                    }
                }
            }


        }

        //public IEnumerable<HighchartSyncTrade> ShowData()
        //{
        //    var stockModel = new List<HighchartSyncTrade>();
        //    var _helper = new Helper();
        //    if (!string.IsNullOrEmpty(userid))
        //    {
        //        using (var sqlConnection = new SqlConnection(syncConnect))
        //        {
        //            sqlConnection.Open();
        //            using (var sqlCommand = sqlConnection.CreateCommand())
        //            {
        //                sqlCommand.CommandText = "select top 1 * from [dbo].[Trades] order by Id desc ";

        //                using (var sqlDataReader = sqlCommand.ExecuteReader())
        //                {
        //                    while (sqlDataReader.Read())
        //                    {
        //                        int id = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Id"));
        //                        string name = sqlDataReader.GetString(sqlDataReader.GetOrdinal("MarketName"));
        //                        int amount = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Amount"));
        //                        bool isCall = sqlDataReader.GetBoolean(sqlDataReader.GetOrdinal("IsCall"));
        //                        decimal profit = sqlDataReader.GetDecimal(sqlDataReader.GetOrdinal("Profit"));
        //                        int status = sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("Status"));
        //                        DateTime createOn = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("CreateOn"));
        //                        DateTime waitingOn = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("WaitingOn"));
        //                        DateTime completeOn = sqlDataReader.GetDateTime(sqlDataReader.GetOrdinal("CompleteOn"));

        //                        var color = status == 1 ? "green" : status == -1 ? "red" : "";
        //                        var model = new HighchartSyncTrade
        //                        {
        //                            Id = id,
        //                            MarketName = name,
        //                            _create_time = createOn.ToString("yyyy-dd-MM HH:mm:ss"),
        //                            _amount = _helper.FormatNumber(amount),
        //                            _isCall = isCall ? "<font color='" + color + "'>CALL</font>" : "<font color='" + color + "'>PUT</font>",
        //                            _iswin = status == 1 ? "<font color='" + color + "'>WIN</font>" : status == -1 ? "<font color='" + color + "'>LOSE</font>" : "--",
        //                            _profit = _helper.FormatNumber(profit),
        //                            _action = "delete"
        //                        };

        //                        stockModel.Add(model);
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return stockModel;
        //}
        public int ServerTime()
        {

            using (var sqlConnection = new SqlConnection(syncConnect))
            {
                try
                {
                    sqlConnection.Open();
                    using (var sqlCommand = sqlConnection.CreateCommand())
                    {
                        sqlCommand.CommandText = "Select Datepart(second,GETDATE())  as SecondsTime";

                        using (var sqlDataReader = sqlCommand.ExecuteReader())
                        {
                            while (sqlDataReader.Read())
                            {
                                return sqlDataReader.GetInt32(sqlDataReader.GetOrdinal("SecondsTime"));
                            }
                        }
                    }
                }
                catch
                {
                    sqlConnection.Close();
                }
                finally
                {
                    sqlConnection.Close();
                }


            }
            return -1;
        }

        #region VolumeTrade
        private void SqlTableDependency_Random_VolumeBuySell_Changed(object sender, RecordChangedEventArgs<TradeVolumeSync> e)
        {
            string valuechange = e.Entity.VolumeBuy + "-" + e.Entity.VolumeSell;
            switch (e.ChangeType)
            {
                case ChangeType.Update:
                    Clients.All.tradeVolume(valuechange);
                    break;
            }
        }
        #endregion
        #region realtime data chart
        //private void SqlTableDependency_Candlestick_Changed(object sender, RecordChangedEventArgs<RealTimeCandlestickData> e)
        //{
        //    try
        //    {
        //        if (!string.IsNullOrEmpty(pair))
        //        {
        //            if (e.Entity.PairName.Equals(pair))
        //            {
        //                RealTimeCandlestickData stick = new RealTimeCandlestickData();
        //                stick.PairName = e.Entity.PairName;
        //                stick.OpenPrice = e.Entity.OpenPrice;
        //                stick.HighPrice = e.Entity.HighPrice;
        //                stick.LowPrice = e.Entity.LowPrice;
        //                stick.ClosePrice = e.Entity.ClosePrice;
        //                stick.TimeClose = e.Entity.TimeClose;
        //                stick.TimeOpen = e.Entity.TimeOpen;
        //                stick.VolumeTo = e.Entity.VolumeTo;
        //                switch (e.ChangeType)
        //                {
        //                    case ChangeType.Update:
        //                        Clients.All.realtimeCandlestick(stick);
        //                        break;
        //                    case ChangeType.Insert:
        //                        Clients.All.realtimeCandlestick(stick);
        //                        break;
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        throw;
        //    }


        //}
        #endregion

        #region Update TradingHistory 
        //private void SqlTableDependency_TradingHistory_Changed(object sender, RecordChangedEventArgs<HighchartSyncTrade> e)
        //{
        //    if ((e.Entity.UserId > 0 && userid.Equals(HelperCommon.CreateEncryptText(e.Entity.UserId.ToString()))) || 1 == 1)
        //    {
        //        var _helper = new Helper();
        //        var color = e.Entity.Status == 1 ? "green" : e.Entity.Status == -1 ? "red" : "";

        //        e.Entity.Id = e.Entity.Id;
        //        e.Entity.UserEncrypted = HelperCommon.CreateEncryptText(e.Entity.UserId.ToString());
        //        e.Entity.MarketName = e.Entity.MarketName;
        //        e.Entity._create_time = e.Entity.CreateOn.ToString("yyyy-dd-MM HH:mm:ss");
        //        e.Entity._amount = _helper.FormatNumber(e.Entity.Amount);
        //        e.Entity._isCall = e.Entity.IsCall ? "<font color='" + color + "'>CALL</font>" : "<font color='" + color + "'>PUT</font>";
        //        e.Entity._iswin = e.Entity.Status == 1 ? "<font color='" + color + "'>WIN</font>" : e.Entity.Status == -1 ? "<font color='" + color + "'>LOSE</font>" : "--";
        //        e.Entity._profit = _helper.FormatNumber(e.Entity.Profit);
        //        e.Entity._action = "delete";

        //        switch (e.ChangeType)
        //        {
        //            case ChangeType.Delete:
        //                Clients.All.removeTrade(e.Entity);
        //                break;

        //            case ChangeType.Insert:
        //                Clients.All.addTrade(e.Entity);
        //                break;

        //            case ChangeType.Update:
        //                Clients.All.updateTrade(e.Entity);
        //                break;
        //        }
        //    }

        //}

        #endregion




        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    _tableDependency.Stop();
                    _tableTradeVolume.Stop();
                    // _tableRealTimeCandlestickData.Stop();
                }
                disposedValue = true;
            }
        }
        ~JobInfoRepository()
        {
            Dispose(false);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
            SqlDependency.Stop(this.syncConnect);
        }
        #endregion
    }
}