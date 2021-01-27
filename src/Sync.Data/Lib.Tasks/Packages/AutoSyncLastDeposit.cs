using System;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Web.SourceCoin.Common;
using System.Threading;
using System.Linq;
using Lib.Domain;

namespace Lib.Tasks.Packages
{
    public class AutoSyncLastDeposit : ITask
    {
        public AutoSyncLastDeposit()
        {
        }
        public void Execute()
        {
            // Auto_Receired();
            //DateTime current = DateTime.UtcNow.AddMinutes(5);

            while (true)
            {
                Auto_Receired();
                System.Threading.Thread.Sleep(100);
                //if(current < DateTime.UtcNow)
                //{
                //    break;
                //}
            }
        }

        private async void Auto_Receired()
        {
            //Task<bool> t1 = KLinesCandlestickSync("BTC_USDT", 0.028m);
            Task<bool> t1 = KLinesCandlestickSync("BTC_USDT", 0.019m);
            //Task<bool> t2 = KLinesCandlestickSync("ETH_USDT");
            //Task<bool> t3 = KLinesCandlestickSync("BNB_USDT");
            //Task<bool> t7 = KLinesCandlestickSync("XLM_USDT");
            //Task<bool> t9 = KLinesCandlestickSync("BCH_USDT");
            //await System.Threading.Tasks.Task.WhenAll(t1, t2, t3, t7, t9);
            await System.Threading.Tasks.Task.WhenAll(t1);
        }
      

        public async Task<bool> KLinesCandlestickSync(string symbol, decimal maxPercentRandom=1)
        {
            TaskRepository _task = new TaskRepository();
            var timenow = DateTime.UtcNow;
            long timestamp = _task.ConvertToUnixTime(timenow);      
            try
            {
                var ConversionType = symbol.Split('_');
                string localsymbol = ConversionType[1].Equals("USDT") ? ConversionType[0] + "_USD" : symbol;

                decimal _lastPriceWin = 0;
                RobotModel priceModel = null;

                BNBClient client = new BNBClient();
                var data = client.Klines(symbol.Replace("_", ""), "1m", 1);
                KlineCandlesticks lastdata = new KlineCandlesticks();
                lastdata = _task.Candlestick_GetBy_Pair_LastTime(localsymbol);
                int timeSecond = _task.Ger_Server_Time_Second();
                //if(timeSecond > 57 || timeSecond <=59)
                if (timeSecond >= 55 && timeSecond <= 59)
                {
                    int select = 0;
                    int.TryParse(_task.Setting_GetValueByName(Constants.INTERVENTION_SYSTEM), out select);
                    int stopLoss = 1000;
                    int.TryParse(_task.Setting_GetValueByName(Constants.INTERVENTION_SYSTEM_STOPLOSS), out stopLoss);
                    if (select == (int)InterventionSystem.CROWD_WIN) //user dat nhieu lenh hon
                    {
                        priceModel = _task.Ticker_Change_Last_Price(localsymbol);
                        _lastPriceWin = _task.Calculator_TradeWin(localsymbol, InterventionSystem.CROWD_WIN, priceModel, stopLoss);
                    }
                    else if (select == (int)InterventionSystem.SMALL_WIN) // ben nao volumn nho hon
                    {
                        priceModel = _task.Ticker_Change_Last_Price(localsymbol);
                        _lastPriceWin = _task.Calculator_TradeWin(localsymbol, InterventionSystem.SMALL_WIN, priceModel, stopLoss);
                    }
                    else if(select == (int)InterventionSystem.FAIR)
                    {
                        priceModel = _task.Ticker_Change_Last_Price(localsymbol);
                        _lastPriceWin = _task.Calculator_TradeWin(localsymbol, InterventionSystem.SMALL_WIN, priceModel, stopLoss);
                    }
                }

                var priceChangePercent = client.GetDailyTicker(symbol.Replace("_", "")).PriceChangePercent;
                foreach (var item in data)
                {
                    KlineCandlesticks candlesticks = new KlineCandlesticks();
                    //candlesticks.IntervalValue = "";
                    if(timenow.Second == 30)
                    {
                        candlesticks.IntervalValue = "30s";
                    }
                    if (timenow.Second == 0)
                    {
                        candlesticks.IntervalValue = "1m";
                    }
                    //if ((timenow.Minute % 2) == 0 && timenow.Second == 0)
                    //{
                    //    candlesticks.IntervalValue += ",2m";
                    //}
                    //if ((timenow.Minute % 3) == 0 && timenow.Second == 0)
                    //{
                    //    candlesticks.IntervalValue += ",3m";
                    //}
                    //if ((timenow.Minute % 30) == 0 && timenow.Second == 0)
                    //{
                    //    candlesticks.IntervalValue += ",5m,15m,30m";
                    //}
                    //else if ((timenow.Minute % 15) == 0 && timenow.Second == 0)
                    //{
                    //    candlesticks.IntervalValue += ",5m,15m";
                    //}
                    //else if ((timenow.Minute % 5) == 0 && timenow.Second == 0)
                    //{
                    //    candlesticks.IntervalValue += ",5m";
                    //}
                    //if ((timenow.Hour % 4) == 0 && (timenow.Minute >= 0))
                    //{
                    //    candlesticks.IntervalValue += ",1h,2h,4h";
                    //}
                    //else if ((timenow.Hour % 2) == 0 && (timenow.Minute >= 0))
                    //{
                    //    candlesticks.IntervalValue += ",1h,2h";
                    //}
                    //else if ((timenow.Minute >= 59) && timenow.Second == 59)
                    //{
                    //    candlesticks.IntervalValue += ",1h";
                    //}
                    //if ((timenow.Hour == 23 && timenow.Minute >= 59) || (timenow.Hour == 0 && timenow.Minute == 0))
                    //{
                    //    candlesticks.IntervalValue += ",D";
                    //}

                    decimal _lastclose = (decimal)item[4];
                   
                    #region Random Price test
                    Random rd = new Random();
                    decimal ranprice = 0;
                    switch (symbol)
                    {
                        case "BTC_USDT":
                            ranprice = 2 + (decimal)(Math.Round(rd.NextDouble(), 4));
                            //ranprice = 1.7m + (decimal)(Math.Round(rd.NextDouble(), 4));
                            break;
                        //case "ETH_USDT":
                        //    ranprice = (decimal)(Math.Round(rd.NextDouble() / 25, 3));
                        //    break;
                        //case "BCH_USDT":
                        //    ranprice = (decimal)(Math.Round(rd.NextDouble() / 23, 3));
                        //    break;
                        //case "BNB_USDT":
                        //    ranprice = (decimal)(Math.Round(rd.NextDouble() / 20, 3));
                        //    break;
                        //case "XLM_USDT":
                        //    ranprice = (decimal)(Math.Round(rd.NextDouble() / 10, 3));
                        //    break;
                        default:
                            break;
                    }
                    if (symbol.Equals("BTC_USDT"))
                    {
                       
                        candlesticks.Close = (_lastclose + ranprice);
                    }
                    else
                    {
                         
                        candlesticks.Close = Math.Round((_lastclose + (ranprice * _lastclose / 100)),4);
                    }
                    // tạm khóa
                    //if(_lastPriceWin != 0 && priceModel != null)
                    //{
                    //    candlesticks.Close = priceModel.LastPrice + _lastPriceWin;
                    //}
                    #endregion
                   // Console.WriteLine("Second: " + timenow.Second);

                    if (lastdata == null || ((timestamp * 1000) > decimal.Parse(lastdata.TimeOpen) && (timenow.Second == 0 || timenow.Second ==30)))
                    {
                        candlesticks.Id = lastdata.Id;
                        candlesticks.TimeOpen = timestamp.ToString();
                        candlesticks.TimeClose = timestamp.ToString();
                        candlesticks.ConversionType = ConversionType[1].Equals("USDT") ? "USD" : ConversionType[1];
                        candlesticks.ConversionSymbol = ConversionType[0];
                        candlesticks.VolumeFrom = (decimal?)item[5];
                        candlesticks.VolumeTo = (decimal?)item[5];
                        candlesticks.PairName = candlesticks.ConversionSymbol + "_" + candlesticks.ConversionType;
                        if (lastdata!=null)
                        {
                            candlesticks.High = lastdata.Close;
                            candlesticks.Low = lastdata.Close;
                            //candlesticks.Open = candlesticks.Close;// lastdata.Close;
                            candlesticks.Open = lastdata.Close;
                            //if (candlesticks.High < lastdata.Open)
                            //{
                            //    candlesticks.High = lastdata.Open;
                            //}
                            //if (candlesticks.Low > lastdata.Open)
                            //{
                            //    candlesticks.Low = lastdata.Open;
                            //}
                        }
                        else 
                        {
                            candlesticks.High = candlesticks.Close;// (decimal?)item[2];
                            candlesticks.Low = candlesticks.Close;// (decimal?)item[3];
                            candlesticks.Open = candlesticks.Close;// (decimal?)item[1];
                        }
                        //candlesticks.PriceChangePercent = priceChangePercent;
                        _task.KLinesCandlestick_Ins(candlesticks);
                        if (timenow.Second == 0)
                        {
                            try
                            {
                                OrderMatching orderMatching = new OrderMatching();
                                orderMatching.BuildCalculator();
                            }
                            catch (Exception ex)
                            {
                                _task.ErrorLog_Insert(null, ex.Message, "OrderMatching");
                            }
                        }
                        //if (timenow.Second == 0)
                        //{
                        //    try
                        //    {
                        //        OrderMatching order = new OrderMatching();
                        //        order.BuildCalculator();
                        //    }
                        //    catch (Exception ex)
                        //    {
                        //        _task.ErrorLog_Insert(null, ex.Message, "AutoSyncLastDeposit ==> OrderMatching", 400);
                        //    }

                        //}



                    } 
                    //else if (timenow.Second != 0 && timenow.Second != 1 && timenow.Second != 30)
                    else if (timenow.Second != 0  && timenow.Second != 30)
                    {
                        //if (candlesticks.Close == lastdata.High && lastdata.Low == candlesticks.Close)
                        //{
                        //    continue;
                        //}
                        //candlesticks.PriceChangePercent = priceChangePercent;
                        candlesticks.High = candlesticks.Close> lastdata.High? candlesticks.Close : lastdata.High;
                        candlesticks.Low = candlesticks.Close < lastdata.Low ? candlesticks.Close : lastdata.Low>0? lastdata.Low: candlesticks.Low;
                        if (candlesticks.High< lastdata.Open)
                        {
                            candlesticks.High = lastdata.Open;
                        }
                        if (candlesticks.Low > lastdata.Open)
                        {
                            candlesticks.Low = lastdata.Open;
                        }
                        //candlesticks.Open = (decimal?)item[1];
                        candlesticks.ConversionType = ConversionType[1].Equals("USDT") ? "USD" : ConversionType[1]; // USDT
                        candlesticks.ConversionSymbol = ConversionType[0]; 
                        candlesticks.PairName = candlesticks.ConversionSymbol + "_" + candlesticks.ConversionType;
                        //candlesticks.TimeOpen = timestamp.ToString();
                        candlesticks.TimeClose = timestamp.ToString();
                        //candlesticks.VolumeFrom = (decimal?)item[5];
                        //candlesticks.VolumeTo = (decimal?)item[5];
                        _task.KLinesCandlestick_Update(candlesticks);
                       
                    }
                }
                return true;
            }
            catch(Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "Crypto api market from Markets");
                return false;
            }
        }
      
    }
}
