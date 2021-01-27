using System;
using System.Linq;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Lib.Domain.Simples;

namespace Lib.Tasks.Packages
{
    public class BonusInvest : ITask
    {
        public BonusInvest()
        {
        }
        public void Execute()
        {
            BonusBuild();
        }

        public void BonusBuild()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                string config = _task.Setting_GetValueByName("Invest.Profit.OnDay.Percent");

                var data = _task.Tool_Get_Packeges_Bonus();
                if (data.Count > 0)
                {
                    double perent = 10;
                    double.TryParse(config, out perent);
                    foreach (Packages_Bonus bonus in data)
                    {
                        try
                        {
                            Payment(bonus, (decimal)perent, _task);
                        }
                        catch (Exception ex)
                        {
                            var json = new JavaScriptSerializer().Serialize(bonus);
                            _task.ErrorLog_Insert(null, "Payment -> " + ex.Message, json, 7);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "BonusInvest-Exception", 7);
            }
        }

        private void Payment(Packages_Bonus bonus, decimal percent, TaskRepository task)
        {
            decimal ProfitUSD = bonus.Invested * percent / 100;

            var model = new Packages_Bonus_Transaction
            {
                UserId = bonus.UserId,
                Bonus = ProfitUSD,
                CreateDate = bonus.CurrentDate,
                PercentAmount = percent,
                PackagesId = bonus.Id,
                Type = 8,
                TotalBonus = 0,
                MaxBonusOnMonth = 0
            };
            int rel = task.Tool_Packages_Bonus_Transaction_Insert(model);
            if(rel == 1)
            {
                var referalData = task.Muser_Get_Referal_Id(bonus.UserId);
                if(referalData.Count > 0)
                {
                    string parrents = string.Join(",", referalData.Select(x => x.ParentId).ToList());
                    var uIdValids = task.Invest_Get_MasterIB(parrents);
                    if(uIdValids.Count > 0)
                    {
                        foreach(ParentInvest inv in referalData)
                        {
                            try
                            {
                                if (!uIdValids.Any(x => x == inv.ParentId))
                                {
                                    continue;
                                }

                                var _percent = GetLevelBonus(inv.Level);
                                if (_percent == 0)
                                {
                                    continue;
                                }

                                var _bonus = ProfitUSD * _percent / 100;
                                task.Packages_BonusF_Insert(bonus.UserId, inv.ParentId, _bonus, inv.Level, bonus.Id, bonus.CurrentDate, (int)HistoryTransactionType.Bonus, "Affiliate Bonus");
                            }
                            catch { }
                        }
                    }
                }
            }
            else
            {
                var json = new JavaScriptSerializer().Serialize(model);
                task.ErrorLog_Insert(bonus.Id, json, "BonusInvest-Payment", 7);
            }
        }

        private int GetLevelBonus(int level)
        {
            if(level == 1)
            {
                return 20;
            }
            else if (level == 2)
            {
                return 10;
            }
            else if (level == 3)
            {
                return 5;
            }
            else if (level == 4)
            {
                return 3;
            }
            else if (level == 5)
            {
                return 3;
            }
            else if (level == 6)
            {
                return 3;
            }
            else
            {
                return 0;
            }
        }
    }
}
