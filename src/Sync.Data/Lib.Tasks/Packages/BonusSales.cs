using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;

namespace Lib.Tasks.Packages
{
    public class BonusSales : ITask
    {
        public BonusSales()
        {
            
        }
        public void Execute()
        {
            var _task = new TaskRepository();
            DateTime currentDate = DateTime.Now;
            int dayOfWeek = GetDayOfWeek(currentDate);
            if (dayOfWeek == 0)
            {
                string isRun = _task.Setting_GetValueByName("BonusSales.IsRun");
                if (isRun.Equals("true"))
                {
                    _task.UpdateSetting("BonusSales.IsRun", "false");
                    currentDate = currentDate.AddDays(-1);
                    DateTime beginDate = FirstDayOfWeek(currentDate);
                    DateTime endDate = LastDayOfWeek(currentDate);
                    CalculatorPackeges(beginDate, endDate, _task);
                }
            }
            else
            {
                _task.UpdateSetting("BonusSales.IsRun", "true");
            }
        }
        public void CalculatorPackeges(DateTime beginDate, DateTime endDate, TaskRepository _task)
        {
            int _lastId = 0;
            int count = 0;
            List<HighchartSyncTrade> listData = new List<HighchartSyncTrade>();
            do
            {
                List<HighchartSyncTrade> data = _task.Tool_Bonus_Sale(beginDate, endDate, _lastId);
                count = data.Count;
                if (count > 0)
                {
                    listData.AddRange(data);
                    _lastId = data.Max(x => x.Id);
                    Thread.Sleep(10);
                }
            }
            while (count > 0);

            var resultGroup = listData
                .GroupBy(x => x.UserId)
                .Select(cl => new HighchartSyncTrade
                {
                    Amount = cl.Sum(c => c.Amount),
                    UserId = cl.Key
                }).ToList();

            List<int> UserIds = resultGroup.Select(s => s.UserId).ToList();
            string _userIds = string.Join(",", UserIds);
            var DataParrent = _task.Tool_Bonus_sale_Get_Parrent_Data(_userIds);

            foreach (HighchartSyncTrade item in resultGroup)
            {
                try
                {
                    var parrent = DataParrent.Where(x => x.Id == item.UserId).FirstOrDefault();
                    if(parrent == null)
                    {
                        continue;
                    }

                    int level = _task.User_Branch_Balance_Get_Is_F(parrent.Id);
                    double percent = 0;
                    int upLevel = 0;
                    if (parrent.UserLevel == 0)
                    {
                        if(parrent.TotalF1 == 3 && parrent.TotalDeposit == 10000 && item.Amount >= 500)
                        {
                            upLevel = 1;
                            percent = GetPercent(level, 1);
                        }
                    }
                    else if (parrent.UserLevel == 1)
                    {
                        if (parrent.TotalF1 == 3 && parrent.TotalTree >= 20000 && item.Amount >= 1000)
                        {
                            upLevel = 2;
                            percent = GetPercent(level, 2);
                        }
                        else if (item.Amount >= 1000)
                        {
                            upLevel = 2;
                            percent = GetPercent(level, 2);
                        }
                    }
                    else if (parrent.UserLevel == 2)
                    {
                        if (parrent.TotalF1 == 3 && parrent.TotalTree >= 80000 && item.Amount >= 3000)
                        {
                            upLevel = 3;
                            percent = GetPercent(level, 3);
                        }
                        else if (item.Amount >= 3000)
                        {
                            upLevel = 3;
                            percent = GetPercent(level, 3);
                        }
                    }
                    else if (parrent.UserLevel == 3)
                    {
                        if (parrent.TotalF1 == 3 && parrent.TotalTree >= 350000 && item.Amount >= 5000)
                        {
                            upLevel = 4;
                            percent = GetPercent(level, 4);
                        }
                        else if (item.Amount >= 5000)
                        {
                            upLevel = 4;
                            percent = GetPercent(level, 4);
                        }
                    }
                    else if (parrent.UserLevel == 4)
                    {
                        if (parrent.TotalF1 == 2 && parrent.TotalTree >= 1100000 && item.Amount >= 10000)
                        {
                            upLevel = 5;
                            percent = GetPercent(level, 5);
                        }
                        else if (item.Amount >= 10000)
                        {
                            upLevel = 5;
                            percent = GetPercent(level, 5);
                        }
                    }
                    else if (parrent.UserLevel == 5)
                    {
                        if (parrent.TotalF1 == 2 && parrent.TotalTree >= 5000000 && item.Amount >= 20000)
                        {
                            upLevel = 6;
                            percent = GetPercent(level, 6);
                        }
                        else if (item.Amount >= 20000)
                        {
                            upLevel = 6;
                            percent = GetPercent(level, 6);
                        }
                    }

                    decimal _bonus = parrent.TotalTree * (decimal)percent / 100;
                    if(_bonus > 0 && upLevel > 0)
                    {
                        _task.Tool_Bonus_Sale_Update_Level(item.UserId, upLevel);
                        int rel = _task.Bonus_branch_Update(0, item.UserId, item.UserId, 3, _bonus, 0);
                        if (rel != 1)
                        {
                            var json = new JavaScriptSerializer().Serialize(item);
                            _task.ErrorLog_Insert(item.UserId, json, "Insert_Bonus_Sale_Fail", 4000);
                        }
                        else
                        {
                            if (upLevel == 6)
                            {
                                decimal xBonus = _bonus * 10 / 100;
                                _task.User_Branch_Balance_Update_Bonus_F(item.UserId, xBonus);
                            }
                        }
                    }

                }
                catch(Exception ex)
                {
                    var json = new JavaScriptSerializer().Serialize(item);
                    _task.ErrorLog_Insert(null, json + " /n/r " + ex.Message, "BonusSales", 4000);
                }
            }
        }

        private int GetDayOfWeek(DateTime dt)
        {
            var culture = System.Threading.Thread.CurrentThread.CurrentCulture;
            return dt.DayOfWeek - culture.DateTimeFormat.FirstDayOfWeek;
        }

        private DateTime FirstDayOfWeek(DateTime dt)
        {
            var diff = GetDayOfWeek(dt);
            if (diff < 0)
            {
                diff += 7;
            }
            return dt.AddDays(-diff).Date;
        }

        private DateTime LastDayOfWeek(DateTime dt)
        {
            return FirstDayOfWeek(dt).AddDays(6);
        }

        private double GetPercent(int lv1, int lv2)
        {
            int x = lv2 - lv1;
            if (x <= 0)
            {
                return 0;
            }

            if (lv2 == 2)
            {
                if (lv1 == 1)
                {
                    return 0.6 - 0.3;
                }
            }
            else if (lv2 == 3)
            {
                if (lv1 == 1)
                {
                    return 0.8 - 0.3;
                }
                else
                {
                    return 0.8 - 0.6;
                }
            }
            else if (lv2 == 4)
            {
                if (lv1 == 1)
                {
                    return 1 - 0.3;
                }
                else if (lv1 == 2)
                {
                    return 1 - 0.6;
                }
                else
                {
                    return 1 - 0.8;
                }
            }
            else if (lv2 == 5)
            {
                if (lv1 == 1)
                {
                    return 1.2 - 0.3;
                }
                else if (lv1 == 2)
                {
                    return 1.2 - 0.6;
                }
                else if (lv1 == 3)
                {
                    return 1.2 - 0.8;
                }
                else
                {
                    return 1.2 - 1;
                }
            }
            else if (lv2 == 6)
            {
                if (lv1 == 1)
                {
                    return 1.3 - 0.3;
                }
                else if (lv1 == 2)
                {
                    return 1.3 - 0.6;
                }
                else if (lv1 == 3)
                {
                    return 1.3 - 0.8;
                }
                else if (lv1 == 4)
                {
                    return 1.3 - 1;
                }
                else
                {
                    return 1.3 - 1.2;
                }
            }

            return 0;
        }
    }
}
