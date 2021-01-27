
using Lib.Data.Domain.Trade;
using Lib.Data.Repository.Tasks;
using Lib.Tasks.Packages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Web.SourceCoin.Common;

namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("run test...");
            var today = DateTime.Now;
            //ForexSync forex = new ForexSync();
            AutoSyncLastDeposit autoSync = new AutoSyncLastDeposit();
            //TradeRandomVolume trade = new TradeRandomVolume();

            for (int i = 0; i < 100000; i++)
            {
                //trade.Execute();
                //forex.Execute();
                autoSync.Execute();
                
               
                System.Threading.Thread.Sleep(5);
            }

          
        }
        public static  void Executes()
        {
            try
            {
                //GetTimeserver();
                //Lib.Tasks.RealtimePrice arbitrage = new Lib.Tasks.RealtimePrice();
                //arbitrage.CalculatorPackeges();
                AutoSyncLastDeposit autoSync = new AutoSyncLastDeposit();

                var times = DateTime.UtcNow;
                Console.WriteLine("Times: " + times);
              
                Task.Run(() => autoSync.KLinesCandlestickSync("BTC_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("ETH_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("TRX_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("XLM_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("BNB_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("ADA_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("XRP_USDT"));
                //Task.Run(() => autoSync.KLinesCandlestickSync("DASH_USDT"));

                //await System.Threading.Tasks.Task.WhenAll(t1, t2, t3, t4, t5, t6, t7, t8);
                //System.Threading.Thread.Sleep(599);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
       
        public static void GetTimeserver()
        {
            BNBClient client = new BNBClient();
            var data = client.Klines("BTCUSDT", "1m", 1);
            if (data != null)
            {
                TaskRepository _task = new TaskRepository();
                foreach (var item in data)
                {
                    KlineCandlesticks candlesticks = new KlineCandlesticks();
                    var date = DateTime.Now;
                    var timeCloseServer  = _task.ConvertToUnixTime(new DateTime(date.Year, date.Month, date.Day, date.Hour, date.Minute, 00)) * 1000;
                    Console.WriteLine("timeCloseServer: " + timeCloseServer);
                    Console.WriteLine("TimeOpen: " + item[0].ToString().Substring(0, 10));
                    Console.WriteLine("TimeClose: "+ item[6].ToString().Substring(0, 10));
                    //candlesticks.TimeOpen = item[0].ToString().Substring(0, 10);
                    //candlesticks.TimeClose = item[6].ToString().Substring(0, 10);
                    
                }
            }
        }
    }
}
