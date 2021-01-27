using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using Lib.Data.Repository.Models;
using System.Threading;
using Lib.Data.Repository.Tasks;

namespace Lib.Tasks
{
    /// <summary>
    /// Represents task manager
    /// </summary>
    public partial class TaskManager
    {
        private static readonly TaskManager _taskManager = new TaskManager();
        private readonly List<TaskThread> _taskThreads = new List<TaskThread>();
        private TaskManager()
        {

        }

        /// <summary>
        /// Initializes the task manager with the property values specified in the configuration file.
        /// </summary>
        public void Initialize()
        {
            this._taskThreads.Clear();

            int projectType = int.Parse(ConfigurationManager.AppSettings["ProjectType"]);
            TaskRepository _task = new TaskRepository();
            var scheduleTasks = _task.ScheduleTask_GetByProjectId(projectType);


            //one thread, one task
            foreach (var scheduleTask in scheduleTasks)
            {
                var taskThread = new TaskThread(scheduleTask);
                this._taskThreads.Add(taskThread);
                var task = new Task(scheduleTask);
                taskThread.AddTask(task);
            }
        }

        /// <summary>
        /// Starts the task manager
        /// </summary>
        public void Start()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.InitTimer();
            }
        }

        /// <summary>
        /// Stops the task manager
        /// </summary>
        public void Stop()
        {
            foreach (var taskThread in this._taskThreads)
            {
                taskThread.Dispose();
            }
        }

        /// <summary>
        /// Gets the task mamanger instance
        /// </summary>
        public static TaskManager Instance
        {
            get
            {
                return _taskManager;
            }
        }

        /// <summary>
        /// Gets a list of task threads of this task manager
        /// </summary>
        public IList<TaskThread> TaskThreads
        {
            get
            {
                return new ReadOnlyCollection<TaskThread>(this._taskThreads);
            }
        }
    }
}
