using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Lib.Data.Repository.Models;

namespace Lib.Tasks.Packages
{
    public class BonusBranch : ITask
    {
        public BonusBranch()
        {
            
        }
        public void Execute()
        {
            CalculatorPackeges();
        }
        public void CalculatorPackeges()
        {
            var _task = new TaskRepository();

            var data = _task.User_Branch_Balance_GetBonus();
            if(data.Count > 0)
            {
                foreach (User_Branch_Balance bonus in data)
                {
                    Insert_Bonus(bonus, _task);
                }
            }
        }

        private void Insert_Bonus(User_Branch_Balance bonus, TaskRepository task)
        {
            try
            {
                int level = task.User_Branch_Balance_Get_Is_F(bonus.UserId);
                double percent = GetPercent(level, bonus.UserLevel);
                decimal _bonus = bonus.LeftAmount * (decimal)percent / 100;

                int rel = task.Bonus_branch_Update(bonus.Id, bonus.UserId, bonus.ByUid, (int)BranchStatus.Completed, _bonus, 0);
                if (rel != 1)
                {
                    var json = new JavaScriptSerializer().Serialize(bonus);
                    task.ErrorLog_Insert(bonus.Id, json, "Insert_Bonus_Banch_Fail", 7);
                }
                else
                {
                    if(_bonus > 0 && bonus.UserLevel == 6)
                    {
                        decimal xBonus = _bonus * 10 / 100;
                        task.User_Branch_Balance_Update_Bonus_F(bonus.UserId, xBonus);
                    }
                }
            }
            catch
            {
                var json = new JavaScriptSerializer().Serialize(bonus);
                task.ErrorLog_Insert(bonus.Id, json, "Insert_Bonus_Exception", 7);
            }
        }

        private double GetPercent(int lv1, int lv2)
        {
            int x = lv2 - lv1;
            if(x <= 0)
            {
                return 0;
            }
            
            if(lv2 == 2)
            {
                if(lv1 == 1)
                {
                    return 0.6 - 0.3;
                }
            }
            else if(lv2 == 3)
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
                else if(lv1 == 2)
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
                else if(lv1 == 3)
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
