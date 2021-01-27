using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Lib.Cache;
using System.Web.Script.Serialization;
using System.Collections;
using System.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading;
using System.Threading.Tasks;
using CoinbaseConnector;
using CoinbaseConnector.ModelCoin;
using CoinbaseConnector.ModelCoin.Transactions;
using CoinbaseConnector.ModelCoin.Addresss;
using CoinbaseConnector.ModelCoin.Base;

namespace Lib.Service.Service.CoinBase
{
    public interface ICoinService
    {
        WalletAddress CreateAddress(string name);
        WalletAddress CreateAddressEth(string name);
        TransactionList ListTransactions();
        TransactionList ListTransactionsEth();
    }

    public class CoinService : ICoinService
    {
        public CoinService()
        {
            
        }

        private AccountList Account()
        {
            var cbc = new Connector();
            return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettings());
        }

        private AccountList AccountEth()
        {
            var cbc = new Connector();
            return JsonConvert.DeserializeObject<AccountList>(cbc.GetAccountSettingsETH());
        }

        public TransactionList ListTransactions()
        {
            var cbc = new Connector();
            var user = Account();
            if (user != null && user.data.Count > 0)
            {
                var userFirst = user.data.FirstOrDefault();
                //return JsonConvert.DeserializeObject<TransactionList>(cbc.GetTransactionsList("", userFirst.id, ""));
            }
            return null;
        }

        public TransactionList ListTransactionsEth()
        {
            var cbc = new Connector();
            var user = AccountEth();
            if (user != null && user.data.Count > 0)
            {
                var userFirst = user.data.FirstOrDefault();
                //return JsonConvert.DeserializeObject<TransactionList>(cbc.GetTransactionsList("", userFirst.id, EnumMethod.ETH));
            }
            return null;
        }

        //public TransactionGet TransactionDetail(string id)
        //{
        //    var cbc = new Connector();
        //    var user = Account();
        //    if (user != null && user.data.Count > 0)
        //    {
        //        var userFirst = user.data.FirstOrDefault();
        //        return JsonConvert.DeserializeObject<TransactionGet>(cbc.GetTransactionsDetail(userFirst.id, id));
        //    }
        //    return null;
        //}

        public WalletAddress CreateAddress(string name)
        {
            try
            {
                var cbc = new Connector();
                var user = Account();
                if (user != null && user.data.Count > 0)
                {
                    var userFirst = user.data.FirstOrDefault();
                    //return JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddress(userFirst.id, name, EnumMethod.BTC));
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public WalletAddress CreateAddressEth(string name)
        {
            try
            {
                var cbc = new Connector();
                var user = AccountEth();
                if (user != null && user.data.Count > 0)
                {
                    var userFirst = user.data.FirstOrDefault();
                    //return JsonConvert.DeserializeObject<WalletAddress>(cbc.CreateAddress(userFirst.id, name,EnumMethod.ETH));
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}