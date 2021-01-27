using System.ServiceProcess;
using Lib.Tasks;

namespace Window.CoinBase
{
    public partial class Service1 : ServiceBase
    {
        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            LibraryLog.WriteErrorLog("start service");
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }

        protected override void OnStop()
        {
            LibraryLog.WriteErrorLog("stop service");
        }
    }
}
