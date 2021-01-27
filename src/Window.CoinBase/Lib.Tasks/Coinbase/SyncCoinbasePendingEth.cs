using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using CoinbaseConnector;
using CoinbaseConnector.ModelCoin;
using Newtonsoft.Json;
using CoinbaseConnector.ModelCoin.Transactions;
using System.Web.Script.Serialization;
using Lib.Data.Repository.Models;
using CoinbaseConnector.ModelCoin.Base;

namespace Lib.Tasks.Coinbase
{
    public class SyncCoinbasePendingEth : ITask
    {
        public SyncCoinbasePendingEth()
        {

        }
        public void Execute()
        {
            GetTransactionPendding();
        }
        public void GetTransactionPendding()
        {
            TaskRepository _task = new TaskRepository();
            var datas = _task.CoinTransaction_GetDataPendding((int)EnumMethod.ETH);
            if (datas.Count > 0)
            {
                var cbc = new Connector();
                var user = TaskHelper.AccountEth(cbc);//.data.Where(p => p.currency.Equals("ETH")).ToList();
                if (user != null)
                {
                    if (user.data.Count > 0)
                    {
                        var userFirst = user.data.FirstOrDefault();
                        //var userFirst = user.Where(p => p.currency.Equals("ETH")).FirstOrDefault();
                        if (userFirst != null)
                        {
                            foreach (TransactionCoin coin in datas)
                            {
                                var trans = JsonConvert.DeserializeObject<TransactionGet>(cbc.GetTransactionsDetailETH(userFirst.id, coin.TransactionId));
                                if (trans.data.status == "completed")
                                {
                                    string walletAddress = TaskHelper.WalletAddressEthClone(trans.data.network.hash, _task, (int)EnumMethod.ETH);
                                    if (string.IsNullOrEmpty(walletAddress) || (!string.IsNullOrEmpty(walletAddress) && trans.data.network.hash != coin.HashCode))
                                    {
                                        var json = new JavaScriptSerializer().Serialize(trans);
                                        _task.ErrorLog_Insert(null, json, "Pending: Not exists a wallet address or hash not Equals", 8);
                                        continue;
                                    }
                                    _task.CoinTransaction_StatusUpdate(trans.data.status, coin.Id, coin.TransactionId, trans.data.updated_at);
                                    TaskHelper.UserDeposit(walletAddress, coin.TransactionId, _task, (int)EnumMethod.ETH);
                                }
                                else if (trans.data.status != "pending")
                                {
                                    _task.CoinTransaction_StatusUpdate(trans.data.status, coin.Id, coin.TransactionId, trans.data.updated_at);
                                }
                            }
                        }

                    }

                }
            }
        }
    }
}
