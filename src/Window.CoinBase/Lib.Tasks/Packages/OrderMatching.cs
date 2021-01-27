using System;
using System.Linq;
using System.Threading;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Lib.Domain.Coins;
using Lib.Domain.Simples;
using Lib.Domain;

namespace Lib.Tasks.Packages
{
    public class OrderMatching : ITask
    {
        public OrderMatching()
        {

        }
        public void Execute()
        {
            while (true)
            {
                var currentDate = DateTime.Now.Second;
                if (currentDate ==2)
                {
                    BuildCalculator();
                    System.Threading.Thread.Sleep(900);
                }
               
                
            }
           
        }
        public void BuildCalculator()
        {
            var _task = new TaskRepository();
            DateTime ServerTime = _task.Get_Server_Time();
            //var currentDate = DateTime.Now.Second;
            //int second = 59 - currentDate;
            if (ServerTime.Second > 1 && ServerTime.Second <= 59)
            {
                return;
            }
            else if (ServerTime.Second == 2)
            {
               // ServerTime = ServerTime.AddSeconds(1);
                var dataChange = _task.Get_TickerPriceChange();
                if (dataChange != null)
                {
                    DateTime createTime = new DateTime(ServerTime.Year, ServerTime.Month, ServerTime.Day, ServerTime.Hour, ServerTime.Minute, 0);
                    int GetTop = 200, count = 0;
                    do
                    {
                        var data = _task.HighchartSync_OrderMatching(createTime, GetTop);
                        count = data.Count;
                        if (count > 0)
                        {
                            foreach (HighchartSyncTrade item in data)
                            {
                                var lockkey = string.Format("HandleOrder_{0}_{1}", item.Id,item.UserId);
                                try
                                {
                                    if (item.ByType == 0)
                                    {
                                        item.ByType = (int)InvestByType.USD;
                                    }
                                    //var _childrent = dataChange;
                                    //var _priceClose = _childrent.LastPrice;
                                    //var _priceOpen = _childrent.OpenPrice;

                                    //item.BeginAmount = _priceOpen;
                                    //item.EndAmount = _priceClose;
                                    lock (LockHelper.GetLock(lockkey))
                                    {
                                        var _percent = dataChange.TradeWinPercent;
                                        item.BeginAmount = dataChange.OpenPrice;
                                        item.EndAmount = dataChange.CandlestickCloseType;
                                        if (item.IsCall) // order UP
                                        {
                                            //if (_priceOpen < _priceClose) // win
                                            //{
                                            //    item.Status = 1;
                                            //    item.Profit = item.Amount + item.Amount * _percent / 100;
                                            //}
                                            //else if (_priceOpen > _priceClose) // lose
                                            //{
                                            //    item.Status = -1;
                                            //    item.Profit = item.Amount * (-1);
                                            //}
                                            //else // refund money _priceOpen == _priceClose
                                            //{
                                            //    item.Status = 2;
                                            //}

                                            if (dataChange.CandlestickCloseType == (int)CandlestickCloseType.WIN) // win
                                            {
                                                item.Status = 1;
                                                item.Profit = item.Amount + item.Amount * _percent / 100;
                                            }
                                            else if (dataChange.CandlestickCloseType == (int)CandlestickCloseType.LOSE) // lose
                                            {
                                                item.Status = -1;
                                                item.Profit = item.Amount * (-1);
                                            }
                                            else // refund money _priceOpen == _priceClose
                                            {
                                                item.Status = 2;
                                            }
                                        }
                                        else // order down
                                        {
                                            if (dataChange.CandlestickCloseType == (int)CandlestickCloseType.LOSE) // win
                                            {
                                                item.Status = 1;
                                                item.Profit = item.Amount + item.Amount * _percent / 100;
                                            }
                                            else if (dataChange.CandlestickCloseType == (int)CandlestickCloseType.WIN) // lose
                                            {
                                                item.Status = -1;
                                                item.Profit = item.Amount * (-1);
                                            }
                                            else // refund money _priceOpen == _priceClose
                                            {
                                                item.Status = 2;
                                            }
                                        }
                                        _task.HighchartSync_OrderMatching_Update(item);
                                    }
                                    LockHelper.ReleaseLock(lockkey);

                                    //if (!item.IsDemo)
                                    //{
                                    //    int referal = _task.GetReferralIdByUserId(item.UserId);
                                    //    if (referal > 0)
                                    //    {
                                    //        var bonus = item.Amount * (decimal)0.2 / 100;
                                    //        _task.HighchartSync_OrderMatching_Update_BonusF(referal, (decimal)bonus, item.UserId);
                                    //    }
                                    //}
                                }
                                catch (Exception ex)
                                {
                                    _task.ErrorLog_Insert(null, ex.Message, "OrderMatching", 400);
                                }
                                finally
                                {
                                    LockHelper.ReleaseLock(lockkey);
                                }
                            }
                            //reset random 
                            _task.Random_Orders_WinLose_Reset();
                            Thread.Sleep(1);
                        }
                    }
                    while (GetTop == count);

                    RemoveRoBotTrade(_task);

                    Thread.Sleep(5000);
                }

            }


        }

        public decimal BotAutoPushDataChart(TaskRepository task, string pairname)
        {
            //bot xử lý lệnh
            //Random_Orders_WinLose random_Orders = new Random_Orders_WinLose();
            //var random_Orders = task.Random_Orders_WinLose_Get(pairname);
            //if (random_Orders != null)
            //{
            //    // false is PUT win, true is Call win
            //    if (random_Orders.TypeRandom)//true
            //    {
            //        var lastdata = task.Candlestick_GetBy_Pair_LastTime(pairname);
            //        lastdata.Close = random_Orders.MatchingPrice + (random_Orders.MatchingPrice * 0.266m / 100);
            //        task.KLinesCandlestick_Update(lastdata);
            //        return (decimal)lastdata.Close;
            //    }
            //    else
            //    {
            //        var lastdata = task.Candlestick_GetBy_Pair_LastTime(pairname);
            //        lastdata.Close = random_Orders.MatchingPrice - (random_Orders.MatchingPrice * 0.347m / 100);
            //        task.KLinesCandlestick_Update(lastdata);
            //        return (decimal)lastdata.Close;
            //    }
            //}
            return 0;
            // end bot xử lý lệnh
        }

        public void RemoveRoBotTrade(TaskRepository task)
        {
            task.RobotTrade_Remoce();
        }
    }
}
