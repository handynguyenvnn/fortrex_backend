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
    public class WithdrawListExpireUnconfirmedEmail : ITask
    {
        public WithdrawListExpireUnconfirmedEmail()
        {
            
        }
        public void Execute()
        {
            ListExpireUnconfirmedEmail();
        }
        public void ListExpireUnconfirmedEmail()
        {
            var _task = new TaskRepository();
            var data = _task.WithdrawExpires();
            if(data.Count > 0)
            {
                foreach (WithdrawExpire item in data)
                {
                    try
                    {
                        _task.Withdraw_UpdateStatus(item.Id, 3, item.UserId,DateTime.Now,"");
                    }
                    catch (Exception ex)
                    {
                        var json = new JavaScriptSerializer().Serialize(item);
                        _task.ErrorLog_Insert(item.UserId, json + " - " + ex.ToString(), "ListExpireUnconfirmedEmail", 99);
                    }         
                }
            }
        }

    }
}
