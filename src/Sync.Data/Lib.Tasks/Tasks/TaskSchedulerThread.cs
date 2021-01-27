using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Lib.Data.Repository.Models;

namespace Lib.Tasks
{
    public partial class TaskSchedulerThread : IDisposable
    {
        private Timer _timer;
        private bool _disposed;
        private DateTime _startedUtc;
        private bool _isRunning;
        private readonly Dictionary<string, Task> _tasks;
        private int _seconds;

        internal TaskSchedulerThread()
        {
            this._tasks = new Dictionary<string, Task>();
            this._seconds = 10 * 60;
        }

        internal TaskSchedulerThread(ScheduleTask scheduleTask)
        {
            this._tasks = new Dictionary<string, Task>();
            this._seconds = scheduleTask.Seconds;
            this._isRunning = false;
        }

        private void Run()
        {
            if (_seconds <= 0)
                return;

            this._startedUtc = DateTime.UtcNow;
            this._isRunning = true;
            foreach (Task task in this._tasks.Values)
            {
                task.Execute();
            }
            this._isRunning = false;
        }

        private void TimerHandler(object state)
        {
            this._timer.Change(-1, -1);
            this.Run();
            this._timer.Change(this.Interval, this.Interval);
        }

        /// <summary>
        /// Disposes the instance
        /// </summary>
        public void Dispose()
        {
            if ((this._timer != null) && !this._disposed)
            {
                lock (this)
                {
                    this._timer.Dispose();
                    this._timer = null;
                    this._disposed = true;
                }
            }
        }

        /// <summary>
        /// Inits a timer
        /// </summary>
        public void InitTimer()
        {
            if (this._timer == null)
            {
                this._timer = new Timer(new TimerCallback(this.TimerHandler), null, this.DueTime, this.Interval);
            }
        }

        /// <summary>
        /// Adds a task to the thread
        /// </summary>
        /// <param name="task">The task to be added</param>
        public void AddTask(Task task)
        {
            if (!this._tasks.ContainsKey(task.Name))
            {
                this._tasks.Add(task.Name, task);
            }
        }


        /// <summary>
        /// Gets or sets the interval in seconds at which to run the tasks
        /// </summary>
        public int Seconds
        {
            get
            {
                return this._seconds;
            }
            internal set
            {
                this._seconds = value;
            }
        }

        /// <summary>
        /// Get a datetime when thread has been started
        /// </summary>
        public DateTime Started
        {
            get
            {
                return this._startedUtc;
            }
        }

        /// <summary>
        /// Get a value indicating whether thread is running
        /// </summary>
        public bool IsRunning
        {
            get
            {
                return this._isRunning;
            }
        }

        /// <summary>
        /// Get a list of tasks
        /// </summary>
        public IList<Task> Tasks
        {
            get
            {
                var list = new List<Task>();
                foreach (var task in this._tasks.Values)
                {
                    list.Add(task);
                }
                return new System.Collections.ObjectModel.ReadOnlyCollection<Task>(list);
            }
        }

        /// <summary>
        /// Gets the interval at which to run the tasks
        /// </summary>
        public int Interval
        {
            get
            {
                return this._seconds * 1000;
            }
        }
        private int DueTime
        {
            get
            {
                return 60 * 1000;//1m
            }
        }
    }
}
