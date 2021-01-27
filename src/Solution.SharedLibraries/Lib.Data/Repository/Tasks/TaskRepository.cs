using System.Linq;
using System.Data;
using System.Data.Common;
using System.Collections.Generic;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Lib.Data.MapBuilder;
using Lib.Core.Data;
using Lib.Data.ResultSetMapper;
using Lib.Domain.Tasks;
using System;

namespace Lib.Data.Repository.Tasks
{
    public interface ITaskRepository
    {
        List<ScheduleTask> ScheduleTask_All();
        ScheduleTask GetTaskByType(string type);
        int UpdateTask(ScheduleTask task);
       
    }

    public class TaskRepository : BaseRepository, ITaskRepository
    {
        public List<ScheduleTask> ScheduleTask_All()
        {
            var map = NewsMapBuilder<ScheduleTask>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ScheduleTask_All", map);
            return query.Execute().ToList();
        }

        public ScheduleTask GetTaskByType(string type)
        {
            if (string.IsNullOrWhiteSpace(type))
                return null;

            var map = NewsMapBuilder<ScheduleTask>.BuildAllProperties();
            var query = _db.CreateSprocAccessor("ScheduleTask_GetTaskByType", map);
            return query.Execute(type).FirstOrDefault();
        }
       
        public int UpdateTask(ScheduleTask task)
        {
            var map = new IntegerResultSetMapper();
            var query = _db.CreateSprocAccessor("ScheduleTask_UpdateTask", map);
            return query.Execute(task.LastStartUtc,
                task.LastEndUtc,
                task.LastSuccessUtc).FirstOrDefault();
        }
    }
}