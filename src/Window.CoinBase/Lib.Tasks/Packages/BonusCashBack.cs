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
    public class BonusCashBack : ITask
    {
        public BonusCashBack()
        {
            
        }
        public void Execute()
        {
            BuildData();
        }
        public void BuildData()
        {
            var _task = new TaskRepository();
            int lastId = 0;
            int top = 50;
            int count = 0;
            do
            {
                var data = _task.Tool_Get_Vol_By_Uid(lastId, top);
                count = data.Count;
                lastId = data.Max(x => x.Id);
                if(count > 0)
                {
                    foreach (User_Vol vol in data)
                    {
                        decimal bonus = GetBonus(vol.TotalTrade);
                        if(bonus == 0)
                        {
                            continue;
                        }
                        try
                        {
                            _task.User_Branch_Balance_VOL_Update(vol.UserId, bonus, 14, "Bonus Cash Back", bonus);
                        }
                        catch { }
                    }
                }
                System.Threading.Thread.Sleep(5);
            }
            while (top == count);

        }

        private decimal GetBonus(decimal amount)
        {
            if(amount >= 1000 && amount <= 3000)
            {
                return 5;
            }
            else if (amount > 3000 && amount <= 6000)
            {
                return 10;
            }
            else if (amount > 6000 && amount <= 10000)
            {
                return 20;
            }
            else if (amount > 10000 && amount <= 20000)
            {
                return 30;
            }
            else if (amount > 20000 && amount <= 50000)
            {
                return 50;
            }
            else if(amount > 50000)
            {
                return 100;
            }
            return 0;
        }
    }
}
