using System;
using System.Linq;
using System.Threading;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Lib.Domain.Coins;

namespace Lib.Tasks.Packages
{
    public class Bots : ITask
    {
        public Bots()
        {

        }
        public void Execute()
        {
            //BuildCalculator();
        }
        //public void BuildCalculator()
        //{
        //    var _task = new TaskRepository();
        //    DateTime ServerTime = _task.Get_Server_Time();
        //    int second = 59 - ServerTime.Second;
        //    if (second == 59 || (second != 0 && second < 57))
        //    {
        //        return;
        //    }
        //    if (second == 0)
        //    {
        //        ServerTime = ServerTime.AddSeconds(1);
        //    }

        //    var dataChange = _task.Get_TickerPriceChange();

        //    DateTime createTime = new DateTime(ServerTime.Year, ServerTime.Month, ServerTime.Day, ServerTime.Hour, ServerTime.Minute, 0);
        //    int GetTop = 100, count = 0;
        //    do
        //    {
        //        var data = _task.HighchartSync_OrderMatching(createTime, GetTop);
        //        count = data.Count;
        //        if (count > 0)
        //        {
        //            foreach (HighchartSyncTrade item in data)
        //            {
        //                try
        //                {
        //                    var _childrent = dataChange.FirstOrDefault(x => x.PairName == item.MarketName);
        //                    var _price = BotAutoPushDataChart(_task,item.MarketName);//_childrent.LastPrice;
        //                    if (_price<=0)
        //                    {
        //                        _price = _childrent.LastPrice;
        //                    }
                           
                            
        //                    var _percent = _childrent.TradeWinPercent;
        //                    if (item.IsCall)
        //                    {
        //                        if (item.BeginAmount < _price)
        //                        {
        //                            item.Status = 1;
        //                            item.Profit = item.Amount + item.Amount * _percent / 100;
        //                        }
        //                        else if (item.BeginAmount > _price)
        //                        {
        //                            item.Status = -1;
        //                            item.Profit = item.Amount * (-1);
        //                        }
        //                        else
        //                        {
        //                            item.Status = 2;
        //                        }
        //                    }
        //                    else
        //                    {
        //                        if (item.BeginAmount > _price)
        //                        {
        //                            item.Status = 1;
        //                            item.Profit = item.Amount + item.Amount * _percent / 100;
        //                        }
        //                        else if (item.BeginAmount < _price)
        //                        {
        //                            item.Status = -1;
        //                            item.Profit = item.Amount * (-1);
        //                        }
        //                        else
        //                        {
        //                            item.Status = 2;
        //                        }
        //                    }
        //                    _task.HighchartSync_OrderMatching_Update(item);

        //                    //if (!item.IsDemo)
        //                    //{
        //                    //    int referal = _task.GetReferralIdByUserId(item.UserId);
        //                    //    if (referal > 0)
        //                    //    {
        //                    //        var bonus = item.Amount * (decimal)0.2 / 100;
        //                    //        _task.HighchartSync_OrderMatching_Update_BonusF(referal, (decimal)bonus, item.UserId);
        //                    //    }
        //                    //}
        //                }
        //                catch (Exception ex)
        //                {
        //                    _task.ErrorLog_Insert(null, ex.Message, "OrderMatching", 400);
        //                }
        //            }
        //            //reset random 
        //           // _task.Random_Orders_WinLose_Reset();
        //            Thread.Sleep(1);
        //        }
        //    }
        //    while (GetTop == count);

        //    Thread.Sleep(5000);
        //}

        //public decimal BotAutoPushDataChart(TaskRepository task, string pairname)
        //{
        //    //bot xử lý lệnh
        //    //Random_Orders_WinLose random_Orders = new Random_Orders_WinLose();
        //    var random_Orders = task.Random_Orders_WinLose_Get(pairname);
        //    if (random_Orders!=null)
        //    {
        //        // false is PUT win, true is Call win
        //        if (random_Orders.TypeRandom)//true
        //        {
        //            var lastdata = task.Candlestick_GetBy_Pair_LastTime(pairname);
        //            lastdata.Close = random_Orders.MatchingPrice + (random_Orders.MatchingPrice * 0.266m / 100);
        //            task.KLinesCandlestick_Update(lastdata);
        //            return (decimal)lastdata.Close;
        //        }
        //        else
        //        {
        //            var lastdata = task.Candlestick_GetBy_Pair_LastTime(pairname);
        //            lastdata.Close = random_Orders.MatchingPrice - (random_Orders.MatchingPrice * 0.347m / 100);
        //            task.KLinesCandlestick_Update(lastdata);
        //            return (decimal)lastdata.Close;
        //        }
        //    }
        //    return 0;
        //    // end bot xử lý lệnh
        //}
    }
}
