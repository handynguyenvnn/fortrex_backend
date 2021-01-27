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
    public class AutoLockAccount : ITask
    {
        public AutoLockAccount()
        {
            
        }
        public void Execute()
        {
            CalculatorPackeges();
        }
        public void CalculatorPackeges()
        {
            TaskRepository _task = new TaskRepository();
            var userIds = _task.Get_All_User_on_Tree_Not_Invest();
            if (userIds.Count > 0)
            {
                string uids = string.Join(",", userIds);
                _task.Lock_All_User_on_Tree_Not_Invest(uids);
            }
        }
    }
}
