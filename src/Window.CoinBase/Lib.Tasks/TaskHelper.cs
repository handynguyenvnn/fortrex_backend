using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Lib.Data.Repository.Models;
using System.Globalization;
using Lib.Data.Repository.Tasks;
using CoinbaseConnector;
using CoinbaseConnector.ModelCoin;
using System.Net.Mail;
using System.Net;
using CoinbaseConnector.ModelCoin.Addresss;
using CoinbaseConnector.ModelCoin.Base;
using Web.SourceCoin.Common;

namespace Lib.Tasks
{
    public static class TaskHelper
    {
        public static DateTime convertIsoToDateTime(string iso)
        {
            return DateTime.ParseExact(iso, "yyyy-MM-dd'T'HH:mm:ssZ", new CultureInfo("en-US"));
        }
        public static string WalletAddress(string hash, TaskRepository _task, int methodPayment)
        {
            var response = ExecuteHash(hash);
            if ((int)response.ResponseStatus == 1 && (int)response.StatusCode == 200)
            {
                if (!string.IsNullOrEmpty(response.Content.Trim()))
                {
                    try
                    {
                        var ojb = JsonConvert.DeserializeObject<Blockcypher>(response.Content.Trim());

                        List<string> addressList = new List<string>();
                        int totalAddress = ojb.addresses.Count();
                        if (totalAddress > 0)
                        {
                            for (int i = 0; i < totalAddress; i++)
                            {
                                addressList.Add(ojb.addresses[i]);
                            }
                        }
                        else
                        {
                            int totalRow = ojb.outputs.Count;
                            if (totalRow > 0)
                            {
                                for (int i = 0; i < totalRow; i++)
                                {
                                    addressList.Add(ojb.outputs[i].addresses[0]);
                                }
                            }
                        }

                        if (addressList.Count > 0)
                        {
                            string address = string.Join(",", addressList);
                            var data = _task.Transaction_GetAddressBTC(address);
                            if (data.Count == 1)
                            {
                                return data.FirstOrDefault();
                            }
                            else if (data.Count > 1)
                            {
                                for (int j = 0; j < data.Count; j++)
                                {

                                }
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        _task.ErrorLog_Insert(null, ex.Message, "BTC: Not exists a wallet address.");
                    }
                }
            }
            else
            {
                _task.ErrorLog_Insert(null, "BTC: " + hash, response.StatusCode.ToString());
            }
            return null;
        }

        public static string WalletAddressEthClone(string hash, TaskRepository _task, int methodPayment)
        {
            Erc20ApisClient apisClient = new Erc20ApisClient();
            var resultTransaction = apisClient.GetTransactionInfo(hash);
            if (resultTransaction != null)
            {
                if (resultTransaction.confirmations >= 12)
                {
                    string address1 = resultTransaction.from; //ojb.addresses[0] ?? "";
                    string address2 = resultTransaction.to; //ojb.addresses[ojb.addresses.Count - 1] ?? "";
                                                            //_task.ErrorLog_Insert(null, "", "Deposit success by api.blockcypher.com");
                    return _task.Transaction_GetAddress(address1, address2, methodPayment);
                }

            }
            else
            {
                
                    _task.ErrorLog_Insert(null, "WalletAddressEthClone: " + hash, "Not find hash details");
                
            }
            return null;
        }

        public static string WalletAddressEth(string hash, TaskRepository _task, int methodPayment)
        {
            var response = ExecuteHashETH(hash);
            if ((int)response.ResponseStatus == 1 && (int)response.StatusCode == 200)
            {
                if (!string.IsNullOrEmpty(response.Content.Trim()))
                {
                    var ojb = JsonConvert.DeserializeObject<Blockcypher>(response.Content.Trim());

                    string address1 = string.Empty, address2 = string.Empty;

                    if (ojb.outputs.Count == 1)
                    {
                        address1 = address2 = ojb.outputs[0].addresses[0];
                    }
                    else if (ojb.outputs.Count > 1)
                    {
                        address1 = ojb.outputs[0].addresses[0];
                        address2 = ojb.outputs[1].addresses[0];
                    }
                    return _task.Transaction_GetAddress(address1, address2, methodPayment);
                }
            }
            return null;
        }

        private static IRestResponse ExecuteHash(string hash)
        {
            string urlHost = string.Format("https://api.blockcypher.com/v1/btc/main/txs/{0}", hash);
            var client = new RestClient(urlHost);
            var request = new RestRequest(Method.GET);
            return client.Execute(request);
        }

        private static IRestResponse ExecuteHashETH(string hash)
        {
            string urlHost = string.Format("https://api.blockcypher.com/v1/eth/main/txs/{0}", hash);
            var client = new RestClient(urlHost);
            var request = new RestRequest(Method.GET);
            return client.Execute(request);
        }

        public static void UserDeposit(string walletAddress, string transactionId, TaskRepository _task, int methodPayment)
        {
            //xử lý code
            _task.Tool_UpdateWalletUserId(walletAddress, transactionId, methodPayment);
        }

        //public static AccountList AccountCoinbase(Connector cbc, EnumMethod method)
        //{
        //    return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettings(method));
        //}
        public static AccountList Account(Connector cbc)
        {
            return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettings());
        }

        public static AccountList AccountEth(Connector cbc)
        {
            return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettingsETH());
        }
        //public static AccountList AccountETH(Connector cbc)
        //{
        //    return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettingsETH());
        //}

        //public static AccountList AccountLTC(Connector cbc)
        //{
        //    return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettingsLTC());
        //}

        //public static AccountList AccountXRP(Connector cbc)
        //{
        //    return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettingsXRP());
        //}

        public static void SendNotificationAsync(Genaral_Marketing_Mail mail)
        {
            try
            {
                string addMail = mail.Email;
                string smtpHost = mail.Host;
                int smtpPost = mail.Port;
                bool smtpEnableSsl = mail.EnableSsl;
                string smtpUserName = mail.Username;
                string smtpPassword = mail.Password;
                string displayName = mail.DisplayName;

                MailAddress ma = new MailAddress(addMail, displayName);
                MailAddress maTo = new MailAddress(mail.ToMail);
                using (MailMessage mm = new MailMessage(ma, maTo))
                {
                    mm.Subject = mail.Title;
                    mm.Body = mail.Body;
                    mm.IsBodyHtml = true;
                    NetworkCredential NetworkCred = new NetworkCredential(smtpUserName, smtpPassword);
                    SmtpClient smtp = new SmtpClient(smtpHost);
                    smtp.Host = smtpHost;
                    smtp.UseDefaultCredentials = mail.UseDefaultCaredential;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = smtpPost;
                    smtp.EnableSsl = smtpEnableSsl;
                    smtp.Send(mm);
                }
            }
            catch (Exception)
            {

            }
        }

        

        //public static WalletAddress CreateAddressETH(string name)
        //{
        //    WalletAddress response = new WalletAddress();
        //    try
        //    {
        //        var cbc = new Connector();
        //        var user = AccountETH(cbc);
        //        if (user != null && user.data.Count > 0)
        //        {
        //            var userFirst = user.data.FirstOrDefault();
        //            response = JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddressETH(userFirst.id, name));
        //        }
        //        else
        //        {
        //            response.meg = "Account is null";
        //        }
        //    }
        //    catch(Exception ex)
        //    {
        //        response.meg = ex.Message;
        //    }
        //    return response;
        //}

        //public static WalletAddress CreateAddressLTC(string name)
        //{
        //    WalletAddress response = new WalletAddress();
        //    try
        //    {
        //        var cbc = new Connector();
        //        var user = AccountLTC(cbc);
        //        if (user != null && user.data.Count > 0)
        //        {
        //            var userFirst = user.data.FirstOrDefault();
        //            response = JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddressLTC(userFirst.id, name));
        //        }
        //        else
        //        {
        //            response.meg = "Account is null";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.meg = ex.Message;
        //    }
        //    return response;
        //}

        //public static WalletAddress CreateAddressXRP(string name)
        //{
        //    WalletAddress response = new WalletAddress();
        //    try
        //    {
        //        var cbc = new Connector();
        //        var user = AccountXRP(cbc);
        //        if (user != null && user.data.Count > 0)
        //        {
        //            var userFirst = user.data.FirstOrDefault();
        //            response = JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddressXRP(userFirst.id, name));
        //        }
        //        else
        //        {
        //            response.meg = "Account is null";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        response.meg = ex.Message;
        //    }
        //    return response;
        //}
        public static double RandomNumberBetween(double minValue, double maxValue)
        {
            Random random = new Random();
            var next = random.NextDouble();

            return (minValue + (next * (maxValue - minValue))) / 1000;
        }
        public static long ConvertToUnixTime(DateTime datetime)
        {
            TimeSpan span = (datetime - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToUniversalTime());
            return (long)span.TotalSeconds;

        }
        public static WalletAddress CreateAddressBTC(string name)
        {
            WalletAddress response = new WalletAddress();
            try
            {
                var cbc = new Connector();
                var user = Account(cbc);
                if (user != null && user.data.Count > 0)
                {
                    var userFirst = user.data.FirstOrDefault();
                    response = JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddress(userFirst.id, name));
                }
                else
                {
                    response.meg = "Account is null";
                }
            }
            catch (Exception ex)
            {
                response.meg = ex.Message;
            }
            return response;
        }

        public static WalletAddress CreateAddressETH(string name)
        {
            WalletAddress response = new WalletAddress();
            try
            {
                var cbc = new Connector();
                var user = AccountEth(cbc);
                if (user != null && user.data.Count > 0)
                {
                    var userFirst = user.data.FirstOrDefault();
                    response = JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddressETH(userFirst.id, name));
                }
                else
                {
                    response.meg = "Account is null";
                }
            }
            catch (Exception ex)
            {
                response.meg = ex.Message;
            }
            return response;
        }
    }
}
