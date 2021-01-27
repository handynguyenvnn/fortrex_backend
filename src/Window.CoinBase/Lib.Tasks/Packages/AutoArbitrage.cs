using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using RestSharp;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;
using System.Configuration;
using Lib.Domain.KLines;
using System.Net;
using Web.SourceCoin.Common;

namespace Lib.Tasks.Packages
{
    public class AutoArbitrage : ITask
    {
        const string BTC = "BTC";
        const string ETH = "ETH";
        const string LTC = "LTC";
        const string XRP = "XRP";

        public AutoArbitrage()
        {
        }
        public  void Execute()
        {
           CalculatorPackeges();
        }
        public void  CalculatorPackeges()
        {
            //CoinAsync(BTC);
            //CoinAsync(ETH);
           //KLinesCandlestick_by_Binance("BTC_USDT") ;
           //KLinesCandlestick_by_Binance("ETH_USDT")  ;
           //KLinesCandlestick_by_Binance("TRX_USDT") ;
           //KLinesCandlestick_by_Binance("ADA_USDT") ;
           //KLinesCandlestick_by_Binance("ADA_ETH");
           //KLinesCandlestick_by_Binance("BNB_USDT") ;
           //KLinesCandlestick_by_Binance("XLM_USDT");

        }

        public async void CoinAsync(string coin)
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                string hostUrl = string.Format("{0}min-api.cryptocompare.com/data/v2/histominute?fsym={1}&tsym=USD&limit=10", "https://", coin);
                var restClient = new RestClient(hostUrl);
                var request = new RestRequest(Method.GET);
                var response = await restClient.ExecuteTaskAsync(request);
                dynamic result = JsonConvert.DeserializeObject(response.Content);
                var data = result["Data"]["Data"];
                string querySQL = "INSERT INTO [HighchartSync].[dbo].[CoinPriceSync] ([FromCoin], [Open], [Close], [High], [Low], [UpdateTime], [CreateOn]) values ('{0}_{1}', {2}, {3}, {4}, {5}, {6}, '{7}'); ";
                foreach (dynamic item in data)
                {
                    double open = (double)item["open"];
                    double close = (double)item["close"];
                    double high = (double)item["high"];
                    double low = (double)item["low"];
                    int time = (int)item["time"];

                    var _low = low + low * 0.3 / 100;
                    if(high < _low)
                    {
                        high = _low;
                    }

                    int second = 0;
                    string sqlQuery = string.Empty;
                    Random ab = new Random();
                    do
                    {
                        int z = ab.Next(-5, 5);
                        int k = 1;
                        if (z < 0)
                        {
                            k = -1;
                        }

                        var x = ab.NextDouble();
                        var _high = high + high * x * k / 200;
                        Thread.Sleep(5);
                        var _loww = low + low * x * k / 200;

                        double _open = GetRandomNumber(_loww, _high);
                        Thread.Sleep(5);
                        double _close = GetRandomNumber(_loww, _high);
                        sqlQuery += string.Format(querySQL, coin, "USDT", _open, _close, _high, _loww, time, DateTime.Now);

                        Thread.Sleep(2990);
                        second += 3000;
                    }
                    while (second < 30000);

                    try
                    {
                        _task.HighchartSync_InsertData(sqlQuery);
                    }
                    catch { }
                }
            }
            catch
            { }
        }

        public double GetRandomNumber(double v1, double v2)
        {
            if (v1 == 0)
            {
                return v2;
            }
            var min = Math.Min(v1, v2);
            var max = Math.Max(v1, v2);
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
        public void  KLinesCandlestick_CloseByTime(string symbol)
        {
            string Api_Key = "yQO0GAcOzOlvU0ThQzbrPS5ISJeRhh1DNIRW2phjjeeQChmOY";
            TaskRepository _task = new TaskRepository();
            try
            {
                string hostUrl = string.Format("https://fcsapi.com/api-v2/crypto/history?symbol={0}&period=1m&access_key={1}", symbol.Replace("_","/"), Api_Key);
                var restClient = new RestClient(hostUrl);
                var request = new RestRequest(Method.POST);

                request.AddHeader("Content-Type", "application/json");
                //string body = "{\"symbol\": \"" + symbol + "\",\"period\":\"1m\",\"access_key\":\""+ Api_Key +"\"}";
                //if (!string.IsNullOrEmpty(body))
                //{
                //    request.AddJsonBody(body);
                //}
            
                ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            //var response = await restClient.ExecuteTaskAsync(request);
            var response =  restClient.Execute(request);
                var data = JsonConvert.DeserializeObject<KlineCandlesticksApiResponse>(response.Content.ToString());

               

                var lastTimeUpdate = _task.KLinesCandlestick_GetMaxTime(symbol)??0;
                if (data.response.Count()>0)
                {
                    foreach (var item in data.response.Where(p=>p.t> lastTimeUpdate))
                    {
                        KlineCandlesticks candlesticks = new KlineCandlesticks();
                        candlesticks.IntervalValue = "1m";
                        if ((item.tm.Minute % 3) == 0)
                        {
                            candlesticks.IntervalValue += ",3m";
                        }
                        if ((item.tm.Minute % 5) == 0)
                        {
                            candlesticks.IntervalValue += ",5m";
                        }
                        if ((item.tm.Minute % 15) == 0)
                        {
                            candlesticks.IntervalValue += ",15m";
                        }
                        if ((item.tm.Minute % 30) == 0)
                        {
                            candlesticks.IntervalValue += ",30m";
                        }
                        if ((item.tm.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",1h";
                        }
                        if ((item.tm.Hour % 2) == 0 && (item.tm.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",2h";
                        }
                        if ((item.tm.Hour % 4) == 0 && (item.tm.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",4h";
                        }
                        if ((item.tm.Hour == 23 && item.tm.Minute >= 59) || (item.tm.Hour == 0 && item.tm.Minute == 0))
                        {
                            candlesticks.IntervalValue += ",D";
                        }
                        if ((int)item.tm.DayOfWeek == 6)
                        {
                            candlesticks.IntervalValue += ",W";
                        }
                        int numberOfDays = DateTime.DaysInMonth(item.tm.Year, item.tm.Month);
                        if (numberOfDays == item.tm.Day)
                        {
                            candlesticks.IntervalValue += ",1M";
                        }
                        var ConversionType = symbol.Split('_');
                        candlesticks.High = item.h;
                        candlesticks.Low = item.l;
                        candlesticks.Open = item.o;
                        candlesticks.Close = item.c;
                        candlesticks.TimeOpen = item.t.ToString();
                        candlesticks.TimeClose = item.t.ToString();
                        candlesticks.ConversionType = ConversionType[1]; // USDT
                        candlesticks.ConversionSymbol = ConversionType[0]; // FBT
                        candlesticks.VolumeFrom = item.v;
                        candlesticks.VolumeTo = item.v;
                        candlesticks.PairName = symbol;
                        _task.KLinesCandlestick_Ins(candlesticks);

                        
                    }
                }
   

            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(0, ex.Message, "KLinesCandlestick_CloseByTime", 500);
            }
        }
        public void KLinesCandlestick_by_Binance(string symbol)
        {
            
            TaskRepository _task = new TaskRepository();
            try
            {
                BNBClient client = new BNBClient();
                var data = client.Klines(symbol.Replace("_",""),"1m",1);
                if (data !=null)
                {
                    foreach (var item in data)
                    {
                        KlineCandlesticks candlesticks = new KlineCandlesticks();
                        candlesticks.IntervalValue = "1m";
                        //if ((item.tm.Minute % 3) == 0)
                        //{
                        //    candlesticks.IntervalValue += ",3m";
                        //}
                        //if ((item.tm.Minute % 5) == 0)
                        //{
                        //    candlesticks.IntervalValue += ",5m";
                        //}
                        //if ((item.tm.Minute % 15) == 0)
                        //{
                        //    candlesticks.IntervalValue += ",15m";
                        //}
                        //if ((item.tm.Minute % 30) == 0)
                        //{
                        //    candlesticks.IntervalValue += ",30m";
                        //}
                        //if ((item.tm.Minute >= 59))
                        //{
                        //    candlesticks.IntervalValue += ",1h";
                        //}
                        //if ((item.tm.Hour % 2) == 0 && (item.tm.Minute >= 59))
                        //{
                        //    candlesticks.IntervalValue += ",2h";
                        //}
                        //if ((item.tm.Hour % 4) == 0 && (item.tm.Minute >= 59))
                        //{
                        //    candlesticks.IntervalValue += ",4h";
                        //}
                        //if ((item.tm.Hour == 23 && item.tm.Minute >= 59) || (item.tm.Hour == 0 && item.tm.Minute == 0))
                        //{
                        //    candlesticks.IntervalValue += ",D";
                        //}
                        //if ((int)item.tm.DayOfWeek == 6)
                        //{
                        //    candlesticks.IntervalValue += ",W";
                        //}
                        //int numberOfDays = DateTime.DaysInMonth(item.tm.Year, item.tm.Month);
                        //if (numberOfDays == item.tm.Day)
                        //{
                        //    candlesticks.IntervalValue += ",1M";
                        //}
                        var ConversionType = symbol.Split('_');
                        candlesticks.High = (decimal?)item[2];
                        candlesticks.Low = (decimal?)item[3];
                        candlesticks.Open = (decimal?)item[1];
                        candlesticks.Close = (decimal?)item[4];
                        candlesticks.TimeOpen = item[0].ToString();
                        candlesticks.TimeClose = item[6].ToString();
                        candlesticks.ConversionType = ConversionType[1].Equals("USDT")?"USD": ConversionType[1]; // USDT
                        candlesticks.ConversionSymbol = ConversionType[0]; // FBT
                        candlesticks.VolumeFrom = (decimal?)item[5];
                        candlesticks.VolumeTo = (decimal?)item[5];
                        candlesticks.PairName = candlesticks.ConversionSymbol+"_"+ candlesticks.ConversionType;
                        _task.KLinesCandlestick_Ins(candlesticks);


                    }
                }
                
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(0, ex.Message, "KLinesCandlestick_CloseByTime", 500);
            }
        }
    }
}
