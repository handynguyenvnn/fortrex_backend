using System;
using System.Linq;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.TronCoins;

namespace Lib.Tasks.Coinbase
{
    public class UpdateDeposit : ITask
    {
        public UpdateDeposit()
        {
            
        }
        public void Execute()
        {
            GetDataRetry();
        }
        public void GetDataRetry()
        {
            TaskRepository _task = new TaskRepository();           
            var data = _task.Tool_CompleteDeposit_Btc();
            if (data.Count() > 0)
            {
                foreach(CoinTransaction coin in data)
                {
                    try
                    {
                        _task.Admin_UpdateMoneyDeposit(coin.AddressWallet, coin.TransactionId, coin.MethodPayment);
                    }
                    catch (Exception ex)
                    {
                        var json = new JavaScriptSerializer().Serialize(coin);
                        _task.ErrorLog_Insert(null, ex.Message + " -> " + json, "UpdateDeposit", 10);
                    }
                }
            }          
        }
    }
}
