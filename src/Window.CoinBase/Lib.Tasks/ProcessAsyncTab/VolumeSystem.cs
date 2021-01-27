using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Domain.AsynTabs;
using Lib.Data.Repository.Models.Packages;
using Lib.Domain.Simples;

namespace Lib.Tasks.ProcessAsyncTab
{
    public class VolumeSystem : ITask
    {
        public VolumeSystem()
        {

        }
        public void Execute()
        {
            BuildData();
        }
        public void BuildData()
        {
            var _task = new TaskRepository();
            var data = _task.AsynTab_Get((int)AsynTabType.PROCESS_PACKAGE, (int)AsynTabStatus.PENDING);
            foreach (AsynTab it in data)
            {
                try
                {
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.PROCESS);
                    BonusLevelExtraData extraData = JsonConvert.DeserializeObject<BonusLevelExtraData>(it.ExtraData);
                    int investUsd = (int)extraData.AmountUSD;
                    var referalData = _task.Muser_Get_Referal_Id(it.UserId);
                    if(referalData.Count() == 0)
                    {
                        _task.AsynTab_Update(it.Id, (int)AsynTabStatus.COMPLETED);
                        continue;
                    }

                    UserReceiveBonus(it.UserId, investUsd, referalData, _task, (int)InvestByType.USD);
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.COMPLETED);
                }
                catch (Exception ex)
                {
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.FAIL);
                    _task.ErrorLog_Insert(it.UserId, ex.ToString(), "Process_Vomume", 34);
                }
            }
        }

        private void UserReceiveBonus(int userId, decimal amount, List<ParentInvest> parrents, TaskRepository das, int byType)
        {
            DateTime timeUTC = DateTime.UtcNow;
            foreach(ParentInvest par in parrents)
            {
                try
                {
                    var percent = GetBonus(par.MasterIB, par.Level);
                    if(percent == 0)
                    {
                        continue;
                    }

                    var bonus = (amount * percent) / 100;
                    das.Packages_BonusF_Insert(
                        userId,
                        par.ParentId,
                        bonus,
                        par.Level,
                        0,
                        timeUTC,
                        (int)HistoryTransactionType.BonusVolunmTrade,
                        "Network Commission",
                        byType
                    );
                }
                catch
                { }
            }
        }

        private decimal GetBonus(decimal pack, int lev)
        {
            if(pack == 100)
            {
                if(lev == 1)
                {
                    return 50;
                }
                else if (lev == 2)
                {
                    return 5;
                }
                else if (lev == 3)
                {
                    return 5;
                }
                else if (lev == 4)
                {
                    return 5;
                }
                else if (lev >= 5 && lev <= 8)
                {
                    return 2.5m;
                }
                else if (lev >= 9 && lev <= 13)
                {
                    return 1;
                }
                return 0;
            }
            else if (pack == 200)
            {
                if (lev == 1)
                {
                    return 21;
                }
                else if (lev == 2)
                {
                    return 8;
                }
                else if (lev == 3)
                {
                    return 8;
                }
                else if (lev == 4)
                {
                    return 5;
                }
                else if (lev == 5)
                {
                    return 5;
                }
                else if (lev == 6)
                {
                    return 3;
                }
                else if (lev == 7)
                {
                    return 2;
                }
                else if (lev == 8)
                {
                    return 1;
                }
                return 0;
            }
            else if (pack == 300)
            {
                if (lev == 1)
                {
                    return 21;
                }
                else if (lev == 2)
                {
                    return 8;
                }
                else if (lev == 3)
                {
                    return 8;
                }
                else if (lev == 4)
                {
                    return 5;
                }
                else if (lev == 5)
                {
                    return 5;
                }
                else if (lev == 6)
                {
                    return 3;
                }
                else if (lev == 7)
                {
                    return 2;
                }
                else if (lev == 8)
                {
                    return 1;
                }
                else if (lev == 9)
                {
                    return 1;
                }
                else if (lev == 10)
                {
                    return 2;
                }
                else if (lev == 11)
                {
                    return 3;
                }
                else if (lev == 12)
                {
                    return 5;
                }
                else if (lev == 13)
                {
                    return 6;
                }
                return 0;
            }
            return 0;
        }
    }
}
