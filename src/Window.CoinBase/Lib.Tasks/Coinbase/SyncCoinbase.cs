using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public class SyncCoinbase : ITask
    {
        public SyncCoinbase()
        {

        }
        public void Execute()
        {
            GetListDataCoin();
        }
        public void GetListDataCoin()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var cbc = new Connector();
                var user = TaskHelper.Account(cbc);
                if (user != null && user.data.Count > 0)
                {
                    var userFirst = user.data.FirstOrDefault();
                    TransactionList datas = new TransactionList();
                    datas = JsonConvert.DeserializeObject<TransactionList>(cbc.GetTransactionsList("", userFirst.id));
                    string lastestUpdate = _task.GetLastedSync((int)EnumMethod.BTC);
                    string nowUpdate = datas != null && datas.data != null && datas.data.Count() > 0 ? datas.data.FirstOrDefault().created_at : DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    _task.LastedSyncUpdate(nowUpdate, (int)EnumMethod.BTC);

                    bool brackWhile = false;
                    while (datas != null && datas.data != null && datas.data.Count() > 0)
                    {
                        foreach (CoinbaseConnector.ModelCoin.Transactions.Transaction data in datas.data)
                        {
                            try
                            {
                                if (TaskHelper.convertIsoToDateTime(lastestUpdate) >= TaskHelper.convertIsoToDateTime(data.created_at))
                                {
                                    brackWhile = true;
                                    break;
                                }

                                //code here
                                var trans = new TransactionCoin
                                {
                                    Type = data.type,
                                    Status = data.status,
                                    USD = data.native_amount != null ? decimal.Parse(data.native_amount.amount) : 0,
                                    BTC = data.amount != null ? decimal.Parse(data.amount.amount) : 0,
                                    CreateDate = TaskHelper.convertIsoToDateTime(data.created_at),
                                    UpdateDate = TaskHelper.convertIsoToDateTime(data.created_at),
                                    HashCode = data.network != null ? data.network.hash : string.Empty,
                                    TransactionId = data.id,
                                    MethodPayment = (int)EnumMethod.BTC
                                };

                                string walletAddress = TaskHelper.WalletAddress(data.network.hash, _task, (int)EnumMethod.BTC);
                                if (string.IsNullOrEmpty(walletAddress))
                                {
                                    _task.CoinTransaction_Clone_Insert(trans);

                                    var json = new JavaScriptSerializer().Serialize(data);
                                    _task.ErrorLog_Insert(null, json, "Not exists a wallet address.", 5);
                                    continue;
                                }

                                trans.AddressWallet = walletAddress;

                                //insert data vao day.
                                int IdTrans = _task.CoinTransactionInsert(trans);
                                if (IdTrans == -1)
                                {
                                    var json = new JavaScriptSerializer().Serialize(data);
                                    _task.ErrorLog_Insert(null, json, "Error system when add new.", 6);
                                }
                                else if (IdTrans > 0)
                                {
                                    if (trans.Status == "completed")
                                    {
                                        TaskHelper.UserDeposit(walletAddress, trans.TransactionId, _task, (int)EnumMethod.BTC);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _task.ErrorLog_Insert(null, ex.Message, data.id, 6);
                            }
                        }

                        if (brackWhile)
                        {
                            datas = null;
                            break;
                        }

                        if (datas.pagination != null && !string.IsNullOrEmpty(datas.pagination.next_uri))
                        {
                            datas = JsonConvert.DeserializeObject<TransactionList>(cbc.GetTransactionsList(datas.pagination.next_uri, userFirst.id));
                        }
                        else
                        {
                            datas = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "BTC", 6);
            }
        }
    }
}
