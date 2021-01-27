using System;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Data.Domain.Trade;
using Web.SourceCoin.Common;
using System.Threading;

namespace Lib.Tasks.Packages
{
    public class TradeRandomVolume : ITask
    {
        public TradeRandomVolume()
        {
        }
        public  void Execute()
        {
            Trade_RandomVolume();
        }

        private void Trade_RandomVolume()
        {
            TaskRepository task = new TaskRepository();
            Random ran = new Random();
            
            int b=0, s=0;


            b = (int)(ran.NextDouble()*100);
            s = 100 - b;
            if (b<15)
            {
                b = 15;
                s = 85;
            }
            if (s<15)
            {
                b = 85;
                s = 15;
            }
            task.Random_VolumeBuySell_Update(b,s);
        }
      

      
    }
}
