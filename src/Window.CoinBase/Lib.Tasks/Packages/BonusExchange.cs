using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MlkPwgen;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Lib.Data.Repository.Models;

namespace Lib.Tasks.Packages
{
    public class BonusExchange : ITask
    {
        private const decimal ESP = 1000000;
        public BonusExchange()
        {
            
        }
        public void Execute()
        {
            BuildData();
        }
        public void BuildData()
        {
            var _task = new TaskRepository();
            decimal totalExchange = _task.Tool_Get_Total_Exchange();
            decimal x = totalExchange / ESP;
            if (x < 10)
            {
                return;
            }
            double bonusPercent = 0;
            if(x >= 10 && x < 50)
            {
                bonusPercent = 0.1;
            }
            else if (x >= 50 && x < 100)
            {
                bonusPercent = 0.2;
            }
            else if (x >= 100 && x < 200)
            {
                bonusPercent = 0.3;
            }
            else if (x >= 200 && x < 500)
            {
                bonusPercent = 0.5;
            }
            else if (x >= 500 && x < 1000)
            {
                bonusPercent = 1;
            }
            else if (x >= 1000)
            {
                bonusPercent = 2;
            }
            if (bonusPercent > 0)
            {
                decimal bonus = totalExchange * (decimal)bonusPercent / 100;
                List<int> uids = _task.Tool_Get_UserId_Level_Five();
                if(uids.Count > 0)
                {
                    foreach(int uid in uids)
                    {
                        try
                        {
                            _task.User_Branch_Balance_VOL_Update(uid, bonus, 15, "Bonus exchanges", (decimal)bonusPercent);
                        }
                        catch { }
                    }
                }
            }
        }
    }
}
