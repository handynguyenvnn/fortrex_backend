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
using Lib.Data.Repository.Models.TronCoins;

namespace Lib.Tasks.Packages
{
    public class TronTransaction : ITask
    {
        private string userHost = "https://api.tronscan.org/api/account/";
        const int TRXMAX = 1000000;
        public TronTransaction()
        {
        }
        public void Execute()
        {
            TronUrl();
        }
        public void TronUrl()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                int lastestId = 0;
                while(lastestId >= 0)
                {
                    var data = _task.Tron_Get_all_Address(lastestId);
                    if (data.Count > 0)
                    {
                        lastestId = data.Max(x => x.Id);
                        foreach (TronCoin tron in data)
                        {
                            try
                            {
                                System.Net.HttpStatusCode statusCode = CheckAddress(tron, _task);
                                if (statusCode == System.Net.HttpStatusCode.NotFound)
                                {
                                    lastestId = -1;
                                    break;
                                }
                            }
                            catch (Exception ex)
                            {
                                LibraryLog.WriteErrorLog(ex);
                            }
                        }
                    }
                    else
                    {
                        lastestId = -1;
                    }
                }
            }
            catch(Exception ex)
            {
                LibraryLog.WriteErrorLog(ex);
            }
        }

        private System.Net.HttpStatusCode CheckAddress(TronCoin tron, TaskRepository _task)
        {
            string url = userHost + tron.Address;
            try
            {
                double my_balance;
                if (double.TryParse(tron.Balance.ToString(), out my_balance))
                {
                    my_balance = my_balance * TRXMAX;
                }
                else
                {
                    return System.Net.HttpStatusCode.InternalServerError;
                }
                var restClient = new RestClient(url);
                var request = new RestRequest(Method.GET);
                var respone = restClient.Execute(request);
                if (respone.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    string content = respone.Content;
                    try
                    {
                        var tran = JsonConvert.DeserializeObject<TronTran>(respone.Content.Trim());
                        double coinTrx = tran.balance;
                        if (tron.Address == tran.address && coinTrx > 0)
                        { 
                            if (my_balance == 0)
                            {
                                tron.Balance = (decimal)coinTrx / TRXMAX;
                                _task.User_Tron_Update_Tool(tron);
                            }
                            else
                            {
                                if (coinTrx > my_balance)
                                {
                                    double _coinTrx = coinTrx - my_balance;
                                    tron.Balance = (decimal)_coinTrx / TRXMAX;
                                    _task.User_Tron_Update_Tool(tron);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        LibraryLog.WriteErrorLog(ex, url);
                    }
                }
                else
                {
                    LibraryLog.WriteErrorLog(respone.StatusCode.ToString() + " : " + url);
                    if(respone.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        return System.Net.HttpStatusCode.NotFound;
                    }
                }
            }
            catch (Exception ex)
            {
                LibraryLog.WriteErrorLog(ex, url);
            }
            return System.Net.HttpStatusCode.OK;
        }
    }
}
