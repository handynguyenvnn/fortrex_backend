using System;
using System.Linq;
using System.Threading;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Lib.Data.Repository.Models.Packages;

namespace Lib.Tasks.Packages
{
    public class BonusVOL : ITask
    {
        public BonusVOL()
        {
            
        }
        public void Execute()
        {
            var _task = new TaskRepository();
            DateTime currentDate = DateTime.Now;
            int dayOfWeek = GetDayOfWeek(currentDate);
            if (dayOfWeek == 0)
            {
                string isRun = _task.Setting_GetValueByName("BonusVOL.IsRun");
                if (isRun.Equals("true"))
                {
                    _task.UpdateSetting("BonusVOL.IsRun", "false");
                    currentDate = currentDate.AddDays(-1);
                    DateTime beginDate = FirstDayOfWeek(currentDate);
                    DateTime endDate = LastDayOfWeek(currentDate);
                    CalculatorPackeges(beginDate, endDate, _task);
                }
            }
            else
            {
                _task.UpdateSetting("BonusVOL.IsRun", "true");
            }
        }
        public void CalculatorPackeges(DateTime beginDate, DateTime endDate, TaskRepository _task)
        {
            int _lastId = 0;
            int count = 0;
            List<User_Branch_Balance> listData = new List<User_Branch_Balance>();
            do
            {
                List<User_Branch_Balance> data = _task.User_Branch_Balance_VOL(beginDate, endDate, _lastId);
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
                .Select(cl => new User_Branch_Balance
                {
                    LeftAmount = cl.Sum(c => c.LeftAmount),
                    UserId = cl.Key
                }).ToList();

            foreach (User_Branch_Balance item in resultGroup)
            {
                try
                {
                    double bonus = 0;
                    if(item.LeftAmount >= 1000000 && item.LeftAmount < 5000000)
                    {
                        bonus = 0.1;
                    }
                    else if (item.LeftAmount >= 5000000 && item.LeftAmount < 10000000)
                    {
                        bonus = 0.2;
                    }
                    else if (item.LeftAmount >= 10000000 && item.LeftAmount < 20000000)
                    {
                        bonus = 0.3;
                    }
                    else if(item.LeftAmount >= 20000000)
                    {
                        bonus = 0.4;
                    }

                    var _bonus = item.LeftAmount * (decimal)bonus / 100;
                    if(_bonus > 0)
                    {
                        _task.User_Branch_Balance_VOL_Update(item.UserId, _bonus, 13, "Bonus VOL", (decimal)bonus);
                    }

                }
                catch(Exception ex)
                {
                    var json = new JavaScriptSerializer().Serialize(item);
                    _task.ErrorLog_Insert(null, json + " /n/r " + ex.Message, "BonusVOL", 4000);
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
    }
}
