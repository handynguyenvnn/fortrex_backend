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
    public class BonusList
    {
        public decimal Bonus { get; set; }
        public decimal BonusBranch { get; set; }
        public decimal MaxBonusBranch { get; set; }
        public decimal LuckyBonus { get; set; }
        public int MaxLevelLuckyBonus { get; set; }
    }

    public class BonusPackeges : ITask
    {
        public BonusPackeges()
        {
            
        }
        public void Execute()
        {
            CalculatorPackeges();
        }
        public void CalculatorPackeges()
        {
            var _task = new TaskRepository();
            var data = _task.User_Branch_Balance_GetALL();
            if(data.Count > 0)
            {
                List<int> uids = data.Select(x => x.UserId).ToList();
                var dataCompleted = _task.User_Branch_Balance_Completed(string.Join(",", uids));
                foreach (User_Branch_Balance bonus in data)
                {
                    Insert_Bonus(bonus, dataCompleted, _task);
                }
            }
        }

        private void Insert_Bonus(User_Branch_Balance bonus, List<User_Branch_Balance> dataCompleted, TaskRepository task)
        {
            try
            {
                decimal bonusBranch = 6;
                var completed = dataCompleted.Where(x => x.UserId == bonus.UserId).FirstOrDefault();
                if (completed == null)
                {
                    bonus.LeftReset = bonus.LeftAmount;
                    bonus.RightReset = bonus.RightAmount;
                    bonus.Status = (int)BranchStatus.Completed;
                    task.User_Branch_Balance_Update(bonus);
                }
                else
                {
                    if (bonus.LeftAmount > 0)
                    {
                        bonus.Status = (int)BranchStatus.Processing;
                        var branchLeft = bonus.LeftAmount + completed.LeftReset;
                        var minBonus = Math.Min(branchLeft, completed.RightReset);
                        bonus.Bonus = Math.Round(minBonus * bonusBranch / 100, 8);
                        if (branchLeft > completed.RightReset)
                        {
                            bonus.LeftReset = branchLeft - completed.RightReset;
                            bonus.RightReset = 0;
                        }
                        else
                        {
                            bonus.LeftReset = 0;
                            bonus.RightReset = completed.RightReset - branchLeft;
                        }
                        task.User_Branch_Balance_Update(bonus);
                    }
                    else if (bonus.RightAmount > 0)
                    {
                        bonus.Status = (int)BranchStatus.Processing;
                        var branchRight = bonus.RightAmount + completed.RightReset;
                        var minBonus = Math.Min(branchRight, completed.LeftReset);
                        bonus.Bonus = Math.Round(minBonus * bonusBranch / 100, 8);
                        if (branchRight < completed.LeftReset)
                        {
                            bonus.LeftReset = completed.LeftReset - branchRight;
                            bonus.RightReset = 0;
                        }
                        else
                        {
                            bonus.LeftReset = 0;
                            bonus.RightReset = branchRight - completed.LeftReset;
                        }
                        task.User_Branch_Balance_Update(bonus);
                    }
                }
            }
            catch
            {
                var json = new JavaScriptSerializer().Serialize(bonus);
                task.ErrorLog_Insert(bonus.Id, json, "Insert_Bonus_Exception", 4);
            }
        }
    }
}
