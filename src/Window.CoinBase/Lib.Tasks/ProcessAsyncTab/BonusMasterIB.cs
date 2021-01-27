using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Lib.Domain.AsynTabs;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Lib.Domain.Simples;

namespace Lib.Tasks.ProcessAsyncTab
{
    public class BonusMasterIB : ITask
    {
        public BonusMasterIB()
        {
        }
        public void Execute()
        {
            BonusBuild();
        }

        public void BonusBuild()
        {
            var _task = new TaskRepository();
            var data = _task.AsynTab_Get((int)AsynTabType.PROCESS_VOLUME_SYSTEM, (int)AsynTabStatus.PENDING);
            foreach (AsynTab it in data)
            {
                try
                {
                    _task.AsynTab_Update(it.Id, (int)AsynTabStatus.PROCESS);
                    BonusLevelExtraData extraData = JsonConvert.DeserializeObject<BonusLevelExtraData>(it.ExtraData);
                    int investUsd = (int)extraData.AmountUSD;
                    var referalData = _task.Muser_Get_Referal_Id(it.UserId);
                    if (referalData.Count() == 0)
                    {
                        _task.AsynTab_Update(it.Id, (int)AsynTabStatus.COMPLETED);
                        continue;
                    }
                    if (extraData.ByType!= (int)InvestByType.DEMO)
                    {
                        UserReceiveBonus(it.UserId, investUsd, referalData, _task, extraData.ByType);
                       
                    }
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
            foreach (ParentInvest par in parrents)
            {
                try
                {
                    var percent = GetBonus(par.MasterIB, par.Level);
                    if (percent == 0 )
                    {
                        continue;
                    }
                    string _bytype = "";
                    if (byType == (int)InvestByType.USD)
                    {
                        _bytype = "USD";
                    }
                    if (byType == (int)InvestByType.GES)
                    {
                        _bytype = "GES";
                    }
                    if (byType == (int)InvestByType.BRI)
                    {
                        _bytype = "BRI";
                    }
                    if (byType == (int)InvestByType.ELD)
                    {
                        _bytype = "ELD";
                    }
                    var bonus = amount * (decimal)percent / 100;
                    das.Packages_BonusF_Insert(
                        userId,
                        par.ParentId,
                        bonus,
                        par.Level,
                        0,
                        timeUTC,
                        (int)HistoryTransactionType.BonusVOL,
                        "Trading Commission - " + _bytype,
                        byType
                    );
                }
                catch
                { }
            }
        }

        private double GetBonus(decimal pack, int lev)
        {
            if (pack == 100)
            {
                if (lev == 1)
                {
                    return 1.3;
                }
                else if (lev == 2)
                {
                    return 0.8;
                }
                else if (lev == 3)
                {
                    return 0.5;
                }
                else if (lev == 4)
                {
                    return 0.3;
                }
                else if (lev == 5)
                {
                    return 0.2;
                }
                else if (lev >= 6 && lev <= 13)
                {
                    return 0.1;
                }
                return 0;
            }
            else if (pack == 200)
            {
                if (lev == 1)
                {
                    return 1.3;
                }
                else if (lev == 2)
                {
                    return 0.8;
                }
                else if (lev == 3)
                {
                    return 0.5;
                }
                else if (lev == 4)
                {
                    return 0.3;
                }
                else if (lev == 5)
                {
                    return 0.2;
                }
                else if(lev > 5 && lev <= 8)
                {
                    return 0.1;
                }
                return 0;
            }
            else if (pack == 300)
            {
                if (lev == 1)
                {
                    return 1.3;
                }
                else if (lev == 2)
                {
                    return 0.8;
                }
                else if (lev == 3)
                {
                    return 0.5;
                }
                else if (lev == 4)
                {
                    return 0.3;
                }
                else if (lev == 5)
                {
                    return 0.2;
                }
                else if (lev > 5 && lev <= 13)
                {
                    return 0.1;
                }
                return 0;
            }
            return 0;
        }
    }
}
