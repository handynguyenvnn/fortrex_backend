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
using Lib.Data.Domain.Trade;
using Lib.Data.Repository.Models;

namespace Lib.Tasks.Packages
{
    public class SyncTab : ITask
    {
        public SyncTab()
        {

        }
        public void Execute()
        {
            BuildData();
        }
        public void BuildData()
        {
            var _task = new TaskRepository();
            UInt64 lastId = 0;
            int top = 50;
            int count = 0;
            do
            {
                var data = _task.Sync_Get_Data(lastId, top);
                count = data.Count;
                lastId = data.Max(x => x.Id);
                if (count > 0)
                {
                    foreach (SyncDataTab vol in data)
                    {
                        try
                        {
                            var tran = JsonConvert.DeserializeObject<UserVolData>(vol.ExtraData);
                            var parentIds = _task.MUser_GetParentByUserId(tran.UserId);
                            foreach (Parents pa in parentIds)
                            {
                                try
                                {
                                    var branch = new Sync_User_Branch_Balance
                                    {
                                        UserId = pa.ParentId,
                                        LeftAmount = tran.Amount,
                                        RightAmount = 0,
                                        LeftReset = 0,
                                        RightReset = 0,
                                        Status = (int)BranchStatus.Avalible,
                                        CreateDate = DateTime.Now,
                                        ByUid = tran.UserId,
                                        PackageId = tran.PackageId,
                                        MaxInvest = pa.Level
                                    };

                                    _task.User_Branch_Balance_Insert(branch);
                                }
                                catch { }
                            }
                        }
                        catch { }
                    }

                    var listIds = data.Select(x => x.Id).ToList();
                    _task.Sync_Remove(string.Join(",", listIds));
                }
                
                System.Threading.Thread.Sleep(5);
            }
            while (count == top);

        }
    }
}
