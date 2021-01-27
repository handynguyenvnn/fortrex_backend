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
    public class AutoDeleteData : ITask
    {
        public AutoDeleteData()
        {
            
        }
        public void Execute()
        {
            BuildData();
        }
        public void BuildData()
        {
            var _task = new TaskRepository();
            DateTime date = DateTime.Now.AddHours(2);
            _task.HighchartSync_RemoveData(date);
        }

    }
}
