using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Domain.AsynTabs;
using Lib.Domain.Simples;

namespace Lib.Tasks.ProcessAsyncTab
{
    public class ProcessBonusLevel : ITask
    {
        public ProcessBonusLevel()
        {
        }
        public void Execute()
        {
            DateTime current = DateTime.UtcNow.AddHours(7);
            int day = (int)current.DayOfWeek;
            if(day != 1)
            {
                return;
            }
            if (current.Hour == 11)
            {
                BonusBuild(current);
            }
        }

        public void BonusBuild(DateTime date)
        {
            var _task = new TaskRepository();
            int dayOfWeek = CoinbaseConnector.Common.GetIso8601WeekOfYear(date);
            dayOfWeek = dayOfWeek - 1;
            if (dayOfWeek == 0)
            {
                dayOfWeek = CoinbaseConnector.Common.GetIso8601WeekOfYear(date.AddDays(-6));
            }

            DateTime currentStartWeek = StartOfWeek(date, 0);
            DateTime endWeek = currentStartWeek.AddDays(-1).AddHours(23);
            DateTime beginWeek = StartOfWeek(endWeek, 0).Date;

            int top = 50;
            int lastId = 0;
            int count = 0;
            do
            {
                try
                {
                    var data = _task.VomumeSystem_Get(top, lastId, dayOfWeek, (int)PocessVolumnSystem.BONUS_MASTERIB_COMPLETE);
                    count = data.Count;
                    lastId = data.Max(x => x.Id);
                    if(count > 0)
                    {
                        var uids = data.Select(x => x.UserId).ToList();
                        string _strUids = string.Join(",", uids);
                        var dataTrade = _task.Get_Total_Trade_Of_UIDS(_strUids, beginWeek, endWeek);
                        foreach (VolumnSystemModel vol in data)
                        {
                            try
                            {
                                _task.VomumeSystem_Update(vol.Id, (int)PocessVolumnSystem.BONUS_LEVEL_PROCESS);
                                ProcessBonus(vol, dataTrade, _task);
                                _task.VomumeSystem_Update(vol.Id, (int)PocessVolumnSystem.BONUS_LEVEL_COMPLETE);
                            }
                            catch
                            {
                                _task.VomumeSystem_Update(vol.Id, (int)PocessVolumnSystem.BONUS_LEVEL_FAIL);
                            }
                        }
                    }
                }
                catch
                {
                    count = 0;
                }
            }
            while (top == count);
        }

        private void ProcessBonus(VolumnSystemModel volum, List<TotalUserTrade> volumTrade, TaskRepository task)
        {
            var trade = volumTrade.Where(x => x.UserId == volum.UserId).FirstOrDefault();
            if(trade == null)
            {
                return;
            }
            decimal bonus = 0;
            if(volum.LevelId == 1)
            {
                bonus = volum.VolumnSystem * volum.MasterIB / 100;
            }
            else if (volum.LevelId == 2)
            {
                if (trade.TotalTrade >= 1000)
                {
                    bonus = volum.VolumnSystem * volum.MasterIB / 100;
                }
            }
            else if (volum.LevelId == 3)
            {
                if (trade.TotalTrade >= 2000)
                {
                    bonus = volum.VolumnSystem * volum.MasterIB / 100;
                }
            }
            else if (volum.LevelId == 4)
            {
                if (trade.TotalTrade >= 2000)
                {
                    bonus = volum.VolumnSystem * volum.MasterIB / 100;
                }
            }
            else if (volum.LevelId == 5)
            {
                if (trade.TotalTrade >= 2000)
                {
                    bonus = volum.VolumnSystem * volum.MasterIB / 100;
                }
            }

            if(bonus > 0)
            {
                task.Packages_BonusF_Insert(volum.UserId, volum.UserId, bonus, volum.LevelId, volum.Id, DateTime.UtcNow, (int)HistoryTransactionType.VolumnLevelTrade, "Volumn Trade Bonus");
            }
        }

        public DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
            return dt.AddDays(-1 * diff).Date;
        }
    }
}
