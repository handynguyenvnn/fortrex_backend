using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Forexs;
using Lib.Data.Domain.Trade;

namespace Lib.Tasks.Packages
{
    public class ForexSync : ITask
    {
        public ForexSync()
        {
        }
        public void Execute()
        {
            //while (true)
            //{
            //    // System.Threading.Thread.Sleep(1);
            //}
            //var today = DateTime.Now;
            //if (today.DayOfWeek != DayOfWeek.Saturday && today.DayOfWeek != DayOfWeek.Sunday)
            //{
            //    BuildData();
            //}
        }

        private async void BuildData()
        {
            //Task<bool> t1 = SyncPriceMarkets(); //AUD_CAD
            //await System.Threading.Tasks.Task.WhenAll(t1);
        }

        private async Task<bool> SyncPriceMarkets()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
              
                var timenow = DateTime.UtcNow;
                long timestamp = _task.ConvertToUnixTime(timenow);
                //string symbols = "AUD/CAD,CAD/JPY,EUR/AUD,EUR/CAD,EUR/USD,GBP/AUD,GBP/CAD,GBP/JPY,GBP/USD,USD/AUD,USD/CAD";
                string symbols = "AUD/CAD,EUR/AUD,EUR/USD,GBP/USD,USD/AUD";
                //string param = string.Format("symbol={0}", symbols);
                string apikey = "Rs2qK2OdX5xZPhqf77AQ8NkV6mjUy2XP";
                var client = new RestClient(string.Format("https://api.1forge.com/quotes?pairs={0}&api_key={1}", symbols, apikey));
                var request = new RestRequest(Method.GET);

                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                //request.AddParameter("application/x-www-form-urlencoded", param, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var trans = JsonConvert.DeserializeObject<List<DataForex>>(response.Content.Trim());
                if (trans != null)
                {
                    int total_row = trans.Count();
                    if (total_row > 0)
                    {

                        // var latest_tran =  trans.Response[total_row - 1];

                        KlineCandlesticks candlesticks = new KlineCandlesticks();
                        candlesticks.IntervalValue = "";
                        if (((timenow.Second % 5) == 0) || timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5s";
                        }
                        if ((timenow.Second % 10) == 0)
                        {
                            candlesticks.IntervalValue += ",10s";
                        }
                        if ((timenow.Second % 15) == 0)
                        {
                            candlesticks.IntervalValue += ",15s";
                        }
                        if ((timenow.Second % 30) == 0)
                        {
                            candlesticks.IntervalValue += ",30s";
                        }
                        if (timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",1m";
                        }
                        if ((timenow.Minute % 2) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",2m";
                        }
                        if ((timenow.Minute % 3) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",3m";
                        }
                        if ((timenow.Minute % 30) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m,15m,30m";

                        }
                        else if ((timenow.Minute % 15) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m,15m";
                        }
                        else if ((timenow.Minute % 5) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m";
                        }
                        if ((timenow.Hour % 4) == 0 && (timenow.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",1h,2h,4h";
                        }
                        else if ((timenow.Hour % 2) == 0 && (timenow.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",1h,2h";
                        }
                        else if ((timenow.Minute >= 59) && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",1h";
                        }
                        if ((timenow.Hour == 23 && timenow.Minute >= 59) || (timenow.Hour == 0 && timenow.Minute == 0))
                        {
                            candlesticks.IntervalValue += ",D";
                        }
                        //if ((int)timenow.DayOfWeek == 6)
                        //{
                        //    candlesticks.IntervalValue += ",W";
                        //}
                        //int numberOfDays = DateTime.DaysInMonth(timenow.Year, timenow.Month);
                        //if (numberOfDays == timenow.Day)
                        //{
                        //    candlesticks.IntervalValue += ",1M";
                        //}

                        foreach (var item in trans)
                        {
                            //Console.WriteLine("Start: " + item.symbol + " - " + timestamp);
                            var ConversionType = item.s.Split('/');
                            var lastdata = _task.Candlestick_GetBy_Pair_LastTime(item.s.Replace('/', '_'));
                            //if (lastdata == null || ((timestamp > decimal.Parse(lastdata.TimeOpen)) && (timenow.Second == 59 || (timenow.Second % 5) == 0)))
                            if (lastdata == null || ((timestamp * 1000) > decimal.Parse(lastdata.TimeOpen) && (timenow.Second == 59 || (timenow.Second % 5) == 0)))
                            {
                                //candlesticks.Close = item.C;
                                candlesticks.TimeOpen = timestamp.ToString();
                                candlesticks.TimeClose = timestamp.ToString();
                                candlesticks.ConversionType = ConversionType[1];
                                candlesticks.ConversionSymbol = ConversionType[0];

                                candlesticks.PairName = item.s.Replace('/', '_');
                                //candlesticks.Times = timenow;
                                Random rd = new Random();
                                var num = rd.Next(1, 999);
                                //var percentRandom = Math.Round(rd.NextDouble());
                                //percentRandom = Math.Round(percentRandom / 4, 3);
                                //if (num % 2 == 0)
                                //{
                                //    candlesticks.Close = item.p + ((decimal)percentRandom * item.p / 100);
                                //}
                                //else
                                //{
                                //    candlesticks.Close = item.p - ((decimal)percentRandom * item.p / 100);
                                //}
                                candlesticks.Close = item.p;
                                if (lastdata != null)
                                {
                                    if (lastdata.Close > item.p)
                                    {
                                        candlesticks.PriceChangePercent = 1;
                                    }
                                    else
                                    {
                                        candlesticks.PriceChangePercent = -1;
                                    }
                                    candlesticks.High = lastdata.Close;
                                    candlesticks.Low = lastdata.Close;
                                    candlesticks.Open = lastdata.Close;
                                    candlesticks.VolumeFrom = 0;
                                    candlesticks.VolumeTo = num / 3;
                                }
                                else
                                {
                                    candlesticks.PriceChangePercent = 1;
                                    candlesticks.High = item.p;
                                    candlesticks.Low = item.p;
                                    candlesticks.Open = item.p;
                                    candlesticks.VolumeFrom = 0;
                                    candlesticks.VolumeTo = num / 3;
                                }
                                _task.KLinesCandlestick_Ins(candlesticks);
                            }
                            else
                            {
                                //candlesticks.Close = item.C;
                                if (lastdata != null)
                                {
                                    if (lastdata.Close > item.p)
                                    {
                                        candlesticks.PriceChangePercent = 1;
                                    }
                                    else
                                    {
                                        candlesticks.PriceChangePercent = -1;
                                    }
                                }
                                else
                                {
                                    candlesticks.PriceChangePercent = 0;
                                }

                                //candlesticks.Close = item.C;
                                //candlesticks.TimeOpen = timestamp.ToString();
                                //candlesticks.TimeClose = timestamp.ToString();
                                candlesticks.ConversionType = ConversionType[1];
                                candlesticks.ConversionSymbol = ConversionType[0];
                                candlesticks.PairName = item.s.Replace('/', '_');
                                //candlesticks.Times = timenow;
                                Random rd = new Random();
                                var num = rd.Next(1, 999);
                                //var percentRandom = rd.NextDouble();
                                //percentRandom = Math.Round(percentRandom / 4, 3);
                                //if (num % 2 == 0)
                                //{
                                //    candlesticks.Close = item.p + ((decimal)percentRandom * item.p / 100);
                                //}
                                //else
                                //{
                                //    candlesticks.Close = item.p - ((decimal)percentRandom * item.p / 100);
                                //}
                                candlesticks.Close = item.p;
                                candlesticks.TimeOpen = timestamp.ToString();
                                candlesticks.TimeClose = timestamp.ToString();
                                //candlesticks.High = item.b;
                                //candlesticks.Low = item.a;
                                candlesticks.High = candlesticks.Close > lastdata.High ? candlesticks.Close : lastdata.High;
                                candlesticks.Low = candlesticks.Close < lastdata.Low ? candlesticks.Close : lastdata.Low;
                                candlesticks.Open = item.p;
                                candlesticks.VolumeFrom = 0;
                                candlesticks.VolumeTo = num / 3;
                                _task.KLinesCandlestick_Update(candlesticks);
                            }
                        }
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "Error API From Forex");
                return false;
            }
            
        }
        private async Task<bool> SyncPriceMarkets_bak()
        {
            TaskRepository _task = new TaskRepository();
            var timenow = DateTime.UtcNow;
            long timestamp = _task.ConvertToUnixTime(timenow);

            //string symbols = "AUD/CAD,CAD/JPY,EUR/AUD,EUR/CAD,EUR/CHF,EUR/GBP,EUR/JPY,EUR/USD,GBP/AUD,GBP/CAD,GBP/CHF,GBP/JPY,GBP/USD,USD/AUD,USD/CAD,USD/JPY";
            string symbols = "AUD/CAD,CAD/JPY,EUR/AUD,EUR/CAD,EUR/USD,GBP/AUD,GBP/CAD,GBP/JPY,GBP/USD,USD/AUD,USD/CAD";
            string param = string.Format("symbol={0}", symbols);
            var client = new RestClient("https://fcs1.p.rapidapi.com/forex/candle");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "fcs1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "b77a45cb18msh5b32cdfb9df0b28p1600a2jsnba5da0243930");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", param, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            var trans = JsonConvert.DeserializeObject<ResponseData>(response.Content.Trim());
            if (trans != null)
            {
                int total_row = trans.Response.Count();
                if (total_row > 0)
                {

                    // var latest_tran =  trans.Response[total_row - 1];

                    KlineCandlesticks candlesticks = new KlineCandlesticks();
                    candlesticks.IntervalValue = "";
                    if (((timenow.Second % 5) == 0) || timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",5s";
                    }
                    if ((timenow.Second % 10) == 0)
                    {
                        candlesticks.IntervalValue += ",10s";
                    }
                    if ((timenow.Second % 15) == 0)
                    {
                        candlesticks.IntervalValue += ",15s";
                    }
                    if ((timenow.Second % 30) == 0)
                    {
                        candlesticks.IntervalValue += ",30s";
                    }
                    if (timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",1m";
                    }
                    if ((timenow.Minute % 2) == 0 && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",2m";
                    }
                    if ((timenow.Minute % 3) == 0 && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",3m";
                    }
                    if ((timenow.Minute % 30) == 0 && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",5m,15m,30m";

                    }
                    else if ((timenow.Minute % 15) == 0 && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",5m,15m";
                    }
                    else if ((timenow.Minute % 5) == 0 && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",5m";
                    }
                    if ((timenow.Hour % 4) == 0 && (timenow.Minute >= 59))
                    {
                        candlesticks.IntervalValue += ",1h,2h,4h";
                    }
                    else if ((timenow.Hour % 2) == 0 && (timenow.Minute >= 59))
                    {
                        candlesticks.IntervalValue += ",1h,2h";
                    }
                    else if ((timenow.Minute >= 59) && timenow.Second == 59)
                    {
                        candlesticks.IntervalValue += ",1h";
                    }
                    if ((timenow.Hour == 23 && timenow.Minute >= 59) || (timenow.Hour == 0 && timenow.Minute == 0))
                    {
                        candlesticks.IntervalValue += ",D";
                    }
                    //if ((int)timenow.DayOfWeek == 6)
                    //{
                    //    candlesticks.IntervalValue += ",W";
                    //}
                    //int numberOfDays = DateTime.DaysInMonth(timenow.Year, timenow.Month);
                    //if (numberOfDays == timenow.Day)
                    //{
                    //    candlesticks.IntervalValue += ",1M";
                    //}

                    foreach (var item in trans.Response)
                    {
                        //Console.WriteLine("Start: " + item.symbol + " - " + timestamp);
                        var ConversionType = item.symbol.Split('/');
                        var lastdata = _task.Candlestick_GetBy_Pair_LastTime(item.symbol.Replace('/', '_'));
                        //if (lastdata == null || ((timestamp > decimal.Parse(lastdata.TimeOpen)) && (timenow.Second == 59 || (timenow.Second % 5) == 0)))
                        if (lastdata == null || ((timestamp * 1000) > decimal.Parse(lastdata.TimeOpen) && (timenow.Second == 59 || (timenow.Second % 5) == 0)))
                        {

                            //candlesticks.Close = item.C;
                            candlesticks.TimeOpen = timestamp.ToString();
                            candlesticks.TimeClose = timestamp.ToString();
                            candlesticks.ConversionType = ConversionType[1];
                            candlesticks.ConversionSymbol = ConversionType[0];

                            candlesticks.PairName = item.symbol.Replace('/', '_');
                            //candlesticks.Times = timenow;
                            Random rd = new Random();
                            var num = rd.Next(1, 999);
                            var percentRandom = Math.Round(rd.NextDouble());
                            percentRandom = Math.Round(percentRandom / 4, 3);
                            if (num % 2 == 0)
                            {
                                candlesticks.Close = item.C + ((decimal)percentRandom * item.C / 100);
                            }
                            else
                            {
                                candlesticks.Close = item.C - ((decimal)percentRandom * item.C / 100);
                            }
                            if (lastdata != null)
                            {
                                if (lastdata.Close > item.C)
                                {
                                    candlesticks.PriceChangePercent = 1;
                                }
                                else
                                {
                                    candlesticks.PriceChangePercent = -1;
                                }
                                candlesticks.High = lastdata.Close;
                                candlesticks.Low = lastdata.Close;
                                candlesticks.Open = lastdata.Close;
                                candlesticks.VolumeFrom = 0;
                                candlesticks.VolumeTo = num / 3;
                            }
                            else
                            {
                                candlesticks.PriceChangePercent = 1;
                                candlesticks.High = item.C;
                                candlesticks.Low = item.C;
                                candlesticks.Open = item.C;
                                candlesticks.VolumeFrom = 0;
                                candlesticks.VolumeTo = num / 3;
                            }
                            _task.KLinesCandlestick_Ins(candlesticks);
                        }
                        else
                        {
                            //candlesticks.Close = item.C;
                            if (lastdata != null)
                            {
                                if (lastdata.Close > item.C)
                                {
                                    candlesticks.PriceChangePercent = 1;
                                }
                                else
                                {
                                    candlesticks.PriceChangePercent = -1;
                                }
                            }
                            else
                            {
                                candlesticks.PriceChangePercent = 0;
                            }

                            //candlesticks.Close = item.C;
                            //candlesticks.TimeOpen = timestamp.ToString();
                            //candlesticks.TimeClose = timestamp.ToString();
                            candlesticks.ConversionType = ConversionType[1];
                            candlesticks.ConversionSymbol = ConversionType[0];
                            candlesticks.PairName = item.symbol.Replace('/', '_');
                            //candlesticks.Times = timenow;
                            Random rd = new Random();
                            var num = rd.Next(1, 999);
                            var percentRandom = rd.NextDouble();
                            percentRandom = Math.Round(percentRandom / 4, 3);
                            if (num % 2 == 0)
                            {
                                candlesticks.Close = item.C + ((decimal)percentRandom * item.C / 100);
                            }
                            else
                            {
                                candlesticks.Close = item.C - ((decimal)percentRandom * item.C / 100);
                            }
                            candlesticks.TimeOpen = timestamp.ToString();
                            candlesticks.TimeClose = timestamp.ToString();
                            candlesticks.High = item.H;
                            candlesticks.Low = item.L;
                            candlesticks.Open = item.O;
                            candlesticks.VolumeFrom = 0;
                            candlesticks.VolumeTo = num / 3;

                            _task.KLinesCandlestick_Update(candlesticks);
                        }


                    }
                }
            }
            //var timeend = DateTime.UtcNow;
            //long timestampend = _task.ConvertToUnixTime(timeend);
            //Console.WriteLine("--------------");
            //Console.WriteLine("End: " + timestampend);
            //Console.WriteLine("Total time: " + (timestampend - timestamp));
            return true;
        }
        private static void LastPrice()
        {
            string symbols = "AUD/CAD,CAD/JPY,EUR/AUD,EUR/CAD,EUR/CHF,EUR/GBP,EUR/JPY,EUR/USD,GBP/AUD,GBP/CAD,GBP/CHF,GBP/JPY,GBP/USD,USD/AUD,USD/CAD,USD/JPY";
            var client = new RestClient("https://fcs1.p.rapidapi.com/forex/latest");
            var request = new RestRequest(Method.POST);
            request.AddHeader("x-rapidapi-host", "fcs1.p.rapidapi.com");
            request.AddHeader("x-rapidapi-key", "b77a45cb18msh5b32cdfb9df0b28p1600a2jsnba5da0243930");
            request.AddHeader("content-type", "application/x-www-form-urlencoded");
            request.AddParameter("application/x-www-form-urlencoded", string.Format("symbol={0}", symbols), ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);

            Console.WriteLine(JsonConvert.DeserializeObject(response.Content.ToString()));
            Console.WriteLine("-------------------------------------------------------------------------------------------: " + DateTime.Now.Second);
        }
        private async Task<bool> CalculatorData()
        {
            TaskRepository _task = new TaskRepository();
            var timenow = DateTime.UtcNow;
            long timestamp = _task.ConvertToUnixTime(timenow);

            try
            {
                string symbols = "AUD/CAD,CAD/JPY,EUR/AUD,EUR/CAD,EUR/CHF,EUR/GBP,EUR/JPY,EUR/USD,GBP/AUD,GBP/CAD,GBP/CHF,GBP/JPY,GBP/USD,USD/AUD,USD/CAD,USD/JPY";
                string param = string.Format("symbol={0}", symbols);
                var client = new RestClient("https://fcs1.p.rapidapi.com/forex/candle");
                var request = new RestRequest(Method.POST);
                request.AddHeader("x-rapidapi-host", "fcs1.p.rapidapi.com");
                request.AddHeader("x-rapidapi-key", "b77a45cb18msh5b32cdfb9df0b28p1600a2jsnba5da0243930");
                request.AddHeader("content-type", "application/x-www-form-urlencoded");
                request.AddParameter("application/x-www-form-urlencoded", param, ParameterType.RequestBody);
                IRestResponse response = client.Execute(request);

                var trans = JsonConvert.DeserializeObject<ResponseData>(response.Content.Trim());
                if (trans != null)
                {
                    int total_row = trans.Response.Count();
                    if (total_row > 0)
                    {

                        // var latest_tran =  trans.Response[total_row - 1];

                        KlineCandlesticks candlesticks = new KlineCandlesticks();
                        candlesticks.IntervalValue = "";
                        if (((timenow.Second % 5) == 0) || timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5s";
                        }
                        if ((timenow.Second % 10) == 0)
                        {
                            candlesticks.IntervalValue += ",10s";
                        }
                        if ((timenow.Second % 15) == 0)
                        {
                            candlesticks.IntervalValue += ",15s";
                        }
                        if ((timenow.Second % 30) == 0)
                        {
                            candlesticks.IntervalValue += ",30s";
                        }
                        if (timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",1m";
                        }
                        if ((timenow.Minute % 2) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",2m";
                        }
                        if ((timenow.Minute % 3) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",3m";
                        }
                        if ((timenow.Minute % 30) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m,15m,30m";

                        }
                        else if ((timenow.Minute % 15) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m,15m";
                        }
                        else if ((timenow.Minute % 5) == 0 && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",5m";
                        }
                        if ((timenow.Hour % 4) == 0 && (timenow.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",1h,2h,4h";
                        }
                        else if ((timenow.Hour % 2) == 0 && (timenow.Minute >= 59))
                        {
                            candlesticks.IntervalValue += ",1h,2h";
                        }
                        else if ((timenow.Minute >= 59) && timenow.Second == 59)
                        {
                            candlesticks.IntervalValue += ",1h";
                        }
                        if ((timenow.Hour == 23 && timenow.Minute >= 59) || (timenow.Hour == 0 && timenow.Minute == 0))
                        {
                            candlesticks.IntervalValue += ",D";
                        }
                        //if ((int)timenow.DayOfWeek == 6)
                        //{
                        //    candlesticks.IntervalValue += ",W";
                        //}
                        //int numberOfDays = DateTime.DaysInMonth(timenow.Year, timenow.Month);
                        //if (numberOfDays == timenow.Day)
                        //{
                        //    candlesticks.IntervalValue += ",1M";
                        //}

                        foreach (var item in trans.Response)
                        {
                            //Console.WriteLine("Start: " + item.symbol + " - " + timestamp);
                            var ConversionType = item.symbol.Split('/');
                            var lastdata = _task.Candlestick_GetBy_Pair_LastTime(item.symbol.Replace('/', '_'));
                            if (lastdata == null || ((timestamp > decimal.Parse(lastdata.TimeClose)) && (timenow.Second == 59 || (timenow.Second % 5) == 0)))
                            {

                                candlesticks.Close = item.C;
                                candlesticks.TimeOpen = timestamp.ToString();
                                candlesticks.TimeClose = timestamp.ToString();
                                candlesticks.ConversionType = ConversionType[1];
                                candlesticks.ConversionSymbol = ConversionType[0];
                                candlesticks.VolumeFrom = 0;
                                candlesticks.VolumeTo = 0;
                                candlesticks.PairName = item.symbol.Replace('/', '_');
                                //candlesticks.Times = timenow;
                                if (lastdata != null)
                                {
                                    candlesticks.High = lastdata.Close;
                                    candlesticks.Low = lastdata.Open;
                                    candlesticks.Open = lastdata.Close;
                                    if (lastdata.Close > item.C)
                                    {
                                        candlesticks.PriceChangePercent = 1;
                                    }
                                    else
                                    {
                                        candlesticks.PriceChangePercent = -1;
                                    }
                                }
                                else
                                {
                                    candlesticks.High = item.C;
                                    candlesticks.Low = item.C;
                                    candlesticks.Open = item.C;
                                    candlesticks.PriceChangePercent = 1;
                                }
                                _task.KLinesCandlestick_Ins(candlesticks);
                            }
                            else
                            {
                                //candlesticks.Close = item.C;
                                if (lastdata != null)
                                {
                                    if (lastdata.Close > item.C)
                                    {
                                        candlesticks.PriceChangePercent = 1;
                                    }
                                    else
                                    {
                                        candlesticks.PriceChangePercent = -1;
                                    }
                                }
                                else
                                {
                                    candlesticks.PriceChangePercent = 0;
                                }
                                candlesticks.High = item.H;
                                candlesticks.Low = item.L;
                                candlesticks.Open = item.O;
                                //candlesticks.Close = item.C;
                                //candlesticks.TimeOpen = timestamp.ToString();
                                //candlesticks.TimeClose = timestamp.ToString();
                                candlesticks.ConversionType = ConversionType[1];
                                candlesticks.ConversionSymbol = ConversionType[0];
                                candlesticks.PairName = item.symbol.Replace('/', '_');
                                //candlesticks.Times = timenow;
                                Random rd = new Random();
                                var num = rd.Next(1, 999);
                                if (num % 2 == 0)
                                {
                                    var percentRandom = Math.Round(rd.NextDouble() / 4, 3);
                                    //if (percentRandom >= 0.4)
                                    //{
                                    //    percentRandom = 0.458;

                                    //}
                                    candlesticks.High = item.H + (((decimal)percentRandom / 2) * item.H / 100);
                                    candlesticks.Close = item.C + ((decimal)percentRandom * item.C / 100);

                                    //if (candlesticks.High < candlesticks.Low)
                                    //{
                                    //    candlesticks.Low = candlesticks.High;
                                    //}
                                }
                                else
                                {
                                    var percentRandom = Math.Round(rd.NextDouble() / 4, 6);

                                    candlesticks.Low = item.L + (((decimal)percentRandom / 2) * item.L / 100);
                                    candlesticks.Close = item.C - ((decimal)percentRandom * item.C / 100);

                                    //if (candlesticks.High < candlesticks.Low)
                                    //{
                                    //    candlesticks.High = candlesticks.Low;
                                    //}
                                }
                                candlesticks.TimeOpen = timestamp.ToString();
                                candlesticks.TimeClose = timestamp.ToString();

                                candlesticks.VolumeFrom = 0;
                                candlesticks.VolumeTo = 0;

                                _task.KLinesCandlestick_Update(candlesticks);
                            }


                        }
                    }
                }
                var timeend = DateTime.UtcNow;
                long timestampend = _task.ConvertToUnixTime(timeend);
                Console.WriteLine("--------------");
                Console.WriteLine("End: " + timestampend);
                Console.WriteLine("Total time: " + (timestampend - timestamp));
            }
            catch {

            }
            return true;
        }
    }
}
