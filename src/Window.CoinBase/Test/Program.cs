
using Lib.Data.Repository.Tasks;
using Lib.Tasks.Coinbase;
using Lib.Tasks.Deposit;
using Lib.Tasks.Packages;
using Lib.Tasks.ProcessAsyncTab;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Test
{
    class Program
    {
        static void Main(string[] args)
        {
            TaskRepository _task = new TaskRepository();
            Console.WriteLine("run test...");
            //for (int i = 0; i < 10000; i++)
            //{
            //    var times = DateTime.UtcNow;
            //    Console.WriteLine("Times: " + times);
            //    Console.WriteLine("Times: " + (int)times.DayOfWeek);
            //    //Console.WriteLine("minute: " + times.Minute +", Second: "+ times.Second);
            //    //long timestamp = _task.ConvertToUnixTime(times);
            //    //Console.WriteLine("timestamp: " + timestamp);
            //    Executes();
            //    System.Threading.Thread.Sleep(599);
            //}
            //for (int i = 0; i < 1; i++)
            //{
            //    Console.WriteLine("in function AutoCreateWallet: ");
            //    AutoCreateWallet wallet = new AutoCreateWallet();
            //    wallet.CalculatorPackeges();
            //    Console.WriteLine("out function AutoCreateWallet: ");
            //    SyncCoinbaseEth eth = new SyncCoinbaseEth();
            //    eth.Execute();
            //}

            //SyncCoinbaseEth eth = new SyncCoinbaseEth();
            //eth.Execute();

            ///deposit
            DepositETH_ERC20 deposit = new DepositETH_ERC20();
            deposit.Wallet_ERC20_lst();
            //VolumeSystem volume = new VolumeSystem();
            //volume.BuildData();
            ///end deposit
            Console.WriteLine("-------------- Done -----------------");
            System.Threading.Thread.Sleep(500099);
            //SyncRegisterAccount ad = new SyncRegisterAccount();
            //ad.GetUserNotRegisterFromCopytrade();
        }
        
    }
}
