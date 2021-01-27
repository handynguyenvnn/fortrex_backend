using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Data;
using CoinbaseConnector;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models;
using CoinbaseConnector.ModelCoin.Transactions;
using CoinbaseConnector.ModelCoin.Base;
using Lib.Domain.Coins;

namespace Lib.Tasks.Coinbase
{
    public class RetryDeposit : ITask
    {
        public RetryDeposit()
        {

        }
        public void Execute()
        {
            GetDataRetry();

        }
        public void GetDataRetry()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var dataClone = _task.CoinTransaction_GetDataClone();
                if (dataClone.Count > 0)
                {
                    var cbc = new Connector();
                    var user = TaskHelper.Account(cbc);
                    var userEth = TaskHelper.AccountEth(cbc);
                    if (user != null && user.data.Count > 0 && userEth != null && userEth.data.Count > 0)
                    {
                        foreach (TransactionCoin data in dataClone)
                        {
                            try
                            {
                                bool check = _task.CoinTransaction_CheckExistHashCode(data.HashCode);
                                if (!check)
                                {
                                    TransactionGet trans = new TransactionGet();
                                    if (data.MethodPayment == (int)EnumMethod.BTC)
                                    {
                                        var userFirst = user.data.FirstOrDefault();
                                        //var userFirst = user.data.Where(p=>p.currency.Equals("BTC")).FirstOrDefault();
                                        trans = JsonConvert.DeserializeObject<TransactionGet>(cbc.GetTransactionsDetail(userFirst.id, data.TransactionId));
                                    }
                                    else if (data.MethodPayment == (int)EnumMethod.ETH)
                                    {
                                        //var userFirst = userEth.data.FirstOrDefault();
                                        var userFirst = user.data.Where(p => p.currency.Equals("ETH")).FirstOrDefault();
                                        trans = JsonConvert.DeserializeObject<TransactionGet>(cbc.GetTransactionsDetailETH(userFirst.id, data.TransactionId));
                                    }


                                    var tranData = new TransactionCoin
                                    {
                                        Type = trans.data.type,
                                        Status = trans.data.status,
                                        USD = trans.data.native_amount != null ? decimal.Parse(trans.data.native_amount.amount) : 0,
                                        BTC = trans.data.amount != null ? decimal.Parse(trans.data.amount.amount) : 0,
                                        CreateDate = TaskHelper.convertIsoToDateTime(trans.data.created_at),
                                        UpdateDate = TaskHelper.convertIsoToDateTime(trans.data.created_at),
                                        HashCode = trans.data.network != null ? trans.data.network.hash : string.Empty,
                                        TransactionId = trans.data.id,
                                        MethodPayment = data.MethodPayment
                                    };

                                    string walletAddress = string.Empty;
                                    if (data.MethodPayment == (int)EnumMethod.BTC)
                                    {
                                        walletAddress = TaskHelper.WalletAddress(trans.data.network.hash, _task, (int)EnumMethod.BTC);
                                    }
                                    else if (data.MethodPayment == (int)EnumMethod.ETH)
                                    {
                                        walletAddress = TaskHelper.WalletAddressEthClone(trans.data.network.hash, _task, (int)EnumMethod.ETH);
                                        if (string.IsNullOrEmpty(walletAddress))
                                        {
                                            walletAddress = TaskHelper.WalletAddressEth(trans.data.network.hash, _task, (int)EnumMethod.ETH);
                                        }
                                    }

                                    if (string.IsNullOrEmpty(walletAddress))
                                    {
                                        continue;
                                    }

                                    tranData.AddressWallet = walletAddress;
                                    int IdTrans = _task.CoinTransactionInsert(tranData);
                                    if (IdTrans > 0)
                                    {
                                        if (trans.data.status == "completed")
                                        {
                                            TaskHelper.UserDeposit(walletAddress, tranData.TransactionId, _task, tranData.MethodPayment);
                                        }
                                        _task.CoinTransaction_DataClone_Delete(data.Id);
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _task.ErrorLog_Insert(null, ex.Message, data.TransactionId, 10);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "Retry_Deposit", 10);
            }
        }

    }
}
