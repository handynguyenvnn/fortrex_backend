using System;
using System.Diagnostics;
using Lib.Data.Repository.Models;
using Lib.Data.Repository.Tasks;

namespace Lib.Tasks
{
    /// <summary>
    /// Task
    /// </summary>
    public partial class Task
    {
        #region
        private bool _enabled;
        private readonly string _type;
        private bool _isRunning;
        private readonly string _name;
        private DateTime? _lastStartUtc;
        private DateTime? _lastSuccessUtc;
        private DateTime? _lastEndUtc;
        private readonly bool _stopOnError;
        /// <summary>
        /// Ctor for Task
        /// </summary>
        private Task()
        {
            this._enabled = true;
        }

        /// <summary>
        /// Ctor for Task
        /// </summary>
        /// <param name="task">Task </param>
        public Task(ScheduleTask task)
        {
            this._type = task.Type;
            this._enabled = true;
            this._name = task.Name;
            this._stopOnError = task.StopOnError;
        }

        private ITask CreateTask()
        {
            ITask task = null;
            if (this.Enabled)
            {
                var type2 = System.Type.GetType(this._type);
                if (type2 != null)
                {
                    task = Activator.CreateInstance(type2) as ITask;
                }
                //this._enabled = task != null;
            }
            return task;
        }

        /// <summary>
        /// Executes the task
        /// </summary>
        public void Execute()
        {
            this._isRunning = true;
            try
            {
                var task = this.CreateTask();
                if (task != null)
                {
                    this._lastStartUtc = DateTime.UtcNow.AddHours(7);
                    task.Execute();
                    this._lastEndUtc = this._lastSuccessUtc = DateTime.UtcNow.AddHours(7);
                }
            }
            catch (Exception ex)
            {
                this._enabled = !this.StopOnError;
                this._lastEndUtc = DateTime.UtcNow.AddHours(7);
                LibraryLog.WriteErrorLog(ex.Message);
            }

            try
            {
                //find current schedule task update time excure
                TaskRepository _task = new TaskRepository();
                var scheduleTask = _task.GetTaskByType(this._type);
                if (scheduleTask != null)
                {
                    scheduleTask.LastStartUtc = this.LastStartUtc;
                    scheduleTask.LastEndUtc = this.LastEndUtc;
                    scheduleTask.LastSuccessUtc = this.LastSuccessUtc;
                    _task.UpdateTask(scheduleTask);
                }
            }
            catch (Exception ex)
            {
                LibraryLog.WriteErrorLog(ex.Message);
            }
            this._isRunning = false;
        }

        /// <summary>
        /// A value indicating whether a task is running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this._isRunning;
            }
        }

        public DateTime? LastStartUtc
        {
            get
            {
                return this._lastStartUtc;
            }
        }

        /// <summary>
        /// Datetime of the last end
        /// </summary>
        public DateTime? LastEndUtc
        {
            get
            {
                return this._lastEndUtc;
            }
        }

        /// <summary>
        /// Datetime of the last success
        /// </summary>
        public DateTime? LastSuccessUtc
        {
            get
            {
                return this._lastSuccessUtc;
            }
        }

        /// <summary>
        /// A value indicating type of the task
        /// </summary>
        public string Type
        {
            get
            {
                return this._type;
            }
        }

        /// <summary>
        /// A value indicating whether to stop task on error
        /// </summary>
        public bool StopOnError
        {
            get
            {
                return this._stopOnError;
            }
        }

        /// <summary>
        /// A value indicating whether the task is enabled
        /// </summary>
        public bool Enabled
        {
            get
            {
                return this._enabled;
            }
        }

        /// <summary>
        /// Get the task name
        /// </summary>
        public string Name
        {
            get
            {
                return this._name;
            }
        }

        #endregion
    }
}

