using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MlkPwgen;
using Lib.Data.Repository.Tasks;
using Lib.Data.Repository.Models.Packages;
using Lib.Data.Repository.Models;
using HtmlAgilityPack;
using RestSharp;

namespace Lib.Tasks.Packages
{
    public class EtherchainSync
    {
        public string from { get; set; }
        public string to { get; set; }
        public decimal value { get; set; }
        public DateTime time { get; set; }
    }

    public class AutoSyncLastDeposit : ITask
    {
        public AutoSyncLastDeposit()
        {
        }
        public void Execute()
        {
            Auto_Receired();
        }
        //public int EtherchainUrl()
        //{
        //    try
        //    {
        //        HtmlWeb hw = new HtmlWeb();
        //        HtmlDocument doc = hw.Load("https://etherscan.io/txs");
        //        var contentNode = doc.DocumentNode.SelectSingleNode("//div[@id=\"ContentPlaceHolder1_mainrow\"]");

        //        var htmlDoc = new HtmlDocument();
        //        htmlDoc.LoadHtml(contentNode.InnerHtml.ToString());
        //        int i = 0;
        //        foreach (HtmlNode newsNode in htmlDoc.DocumentNode.SelectNodes("//a[@href]"))
        //        {
        //            try
        //            {
        //                string hash = newsNode.InnerText;
        //                if (hash.Length > 60)
        //                {
        //                    if (i > 3)
        //                        break;

        //                    if (WalletAddressEth(hash.Trim()))
        //                        i++;
        //                }
        //            }
        //            catch(Exception)
        //            {

        //            }
        //        }
        //    }
        //    catch(Exception)
        //    {

        //    }
        //    return 1;
        //}

        //private bool WalletAddressEth(string hash)
        //{
        //    bool result = false;
        //    string urlHost = string.Format("https://www.etherchain.org/api/tx/{0}", hash);
        //    var client = new RestClient(urlHost);
        //    var request = new RestRequest(Method.GET);
        //    var response = client.Execute(request);
        //    if ((int)response.ResponseStatus == 1 && (int)response.StatusCode == 200)
        //    {
        //        if (!string.IsNullOrEmpty(response.Content.Trim()))
        //        {
        //            try
        //            {
        //                string json = response.Content.Trim().TrimStart('[').TrimEnd(']');
        //                var ojb = JsonConvert.DeserializeObject<EtherchainSync>(json);

        //                string address1 = ojb.from;
        //                decimal eth = ojb.value / 1000000000000000000;
                        
        //                if(eth > (decimal)0.1 && eth <= 3)
        //                {
        //                    //code
        //                    var trans = new TransactionCoin
        //                    {
        //                        Type = "send",
        //                        Status = "completed",
        //                        USD = 1,
        //                        BTC = eth,
        //                        CreateDate = ojb.time,
        //                        UpdateDate = ojb.time,
        //                        HashCode = hash,
        //                        TransactionId = Guid.NewGuid().ToString(),
        //                        MethodPayment = 2,
        //                        AddressWallet = address1
        //                    };

        //                    _task.CoinTransactionInsert(trans);
        //                    result = true;
        //                }

        //            }
        //            catch (Exception)
        //            {
                        
        //            }
        //        }
        //    }
        //    return result;
        //}

        private void Auto_Receired()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var restClient = new RestClient("https://api.tronscan.org/api/transaction");
                var request = new RestRequest(Method.GET);
                var respone = restClient.Execute(request);
                if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = respone.Content;
                    try
                    {
                        var tronCoins = JsonConvert.DeserializeObject<AutoResponse>(respone.Content.Trim());
                        int num = 1;
                        List<TransactionCoin> listData = new List<TransactionCoin>();
                        var dataList = tronCoins.data.OrderByDescending(x => x.timestamp);
                        foreach (AutoReceired tronCoin in dataList)
                        {
                            try
                            {
                                decimal amount = tronCoin.contractData.amount / 1000000;
                                if (amount >= 10 && amount <= 100000)
                                {
                                    if (listData.Exists(x => x.AddressWallet == tronCoin.ownerAddress))
                                    {
                                        continue;
                                    }
                                    var trans = new TransactionCoin
                                    {
                                        Type = "send",
                                        Status = "completed",
                                        USD = 1,
                                        BTC = amount,
                                        CreateDate = TimeStampToDateTime(tronCoin.timestamp / 1000),
                                        UpdateDate = TimeStampToDateTime(tronCoin.timestamp / 1000),
                                        HashCode = tronCoin.hash,
                                        TransactionId = Guid.NewGuid().ToString(),
                                        MethodPayment = 3,
                                        AddressWallet = tronCoin.ownerAddress
                                    };
                                    listData.Add(trans);
                                    num++;
                                    if (num > 7)
                                    {
                                        break;
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                _task.ErrorLog_Insert(null, ex.Message, "Auto_Receired_1", 3);
                                continue;
                            }
                        }

                        if (listData.Count() > 0)
                        {
                            foreach (TransactionCoin coin in listData)
                            {
                                int id = _task.CoinTransactionInsert(coin);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _task.ErrorLog_Insert(null, ex.Message, "Auto_Receired_2", 3);
                    }
                }
                else
                {
                    _task.ErrorLog_Insert(null, respone.StatusCode.ToString(), "Auto_Receired_3", 3);
                }
            }
            catch(Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "Auto_Receired_4", 3);
            }
        }

        private DateTime TimeStampToDateTime(double timestamp)
        {
            System.DateTime dateTime = new System.DateTime(1970, 1, 1, 0, 0, 0, 0);
            dateTime = dateTime.AddSeconds(timestamp);
            return dateTime;
        }
    }
}
