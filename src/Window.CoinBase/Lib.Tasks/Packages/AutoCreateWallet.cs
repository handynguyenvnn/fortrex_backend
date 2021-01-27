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
using RestSharp;

namespace Lib.Tasks.Packages
{
    public class AutoCreateWallet : ITask
    {
        public AutoCreateWallet()
        {

        }
        public void Execute()
        {
            CalculatorPackeges();
        }
        public void CalculatorPackeges()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                var userWallets = _task.Tool_Get_New_User_Not_wallet();
                if (userWallets.Count() > 0)
                {
                    foreach (UserWallet wallet in userWallets)
                    {
                        if (string.IsNullOrEmpty(wallet.WalletBTC))
                        {
                            wallet.WalletBTC = CreateWalletBTC(wallet, _task);
                        }
                        if (string.IsNullOrEmpty(wallet.WalletETH))
                        {
                            wallet.WalletETH = CreateWalletETH(wallet, _task);
                        }
                        _task.User_WalletAddress_Update(wallet.UserId, wallet.WalletBTC, wallet.WalletETH);
                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(null, ex.Message, "AutoCreateWallet", 4);
            }
        }

        private string CreateWalletBTC(UserWallet info, TaskRepository task)
        {
            string nameWallet = string.Format("{0}_{1}", info.Username, info.UserId);
            string wallet = string.Empty;
            try
            {
                var response = TaskHelper.CreateAddressBTC(nameWallet);
                if (string.IsNullOrEmpty(response.meg))
                {
                    wallet = response.data.address;
                }
                else
                {
                    task.ErrorLog_Insert(info.UserId, response.meg, "AutoCreateWallet->BTC", 200);
                }
            }
            catch (Exception ex)
            {
                task.ErrorLog_Insert(info.UserId, ex.Message, "AutoCreateWallet->BTC", 200);
            }
            return wallet;
        }
        private string CreateWalletETH(UserWallet info, TaskRepository task)
        {
            string nameWallet = string.Format("{0}_{1}", info.Username, info.UserId);
            string wallet = string.Empty;
            try
            {
                var response = TaskHelper.CreateAddressETH(nameWallet);
                if (string.IsNullOrEmpty(response.meg))
                {
                    wallet = response.data.address;
                }
                else
                {
                    task.ErrorLog_Insert(info.UserId, response.meg, "AutoCreateWallet->ETH", 200);
                }
            }
            catch (Exception ex)
            {
                task.ErrorLog_Insert(info.UserId, ex.Message, "AutoCreateWallet->ETH", 200);
            }
            return wallet;
        }
    }
}
