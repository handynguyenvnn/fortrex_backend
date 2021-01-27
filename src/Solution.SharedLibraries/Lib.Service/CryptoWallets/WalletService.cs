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
using LibDatabaseEntitys;

namespace Lib.Service.Service.Wallet
{
    public interface IWalletService
    {
        Task<int> User_CreateWallet_With_Privatekey(int userid, CoinList coin);
        Task<TResult> User_Get_Wallet<TResult>(int userid, string coinsymbol);
    }

    public class WalletService : IWalletService
    {
        private readonly Data.Repository.Tasks.IWalletRepository _walletRepository;
        public WalletService(Data.Repository.Tasks.IWalletRepository walletRepository)
        {
            _walletRepository = walletRepository;
        }
        public async Task<int> User_CreateWallet_With_Privatekey(int userid, CoinList coin)
        {
            return await _walletRepository.User_CreateWallet_With_Privatekey(userid, coin);
        }
        public async Task<TResult> User_Get_Wallet<TResult>(int userid, string coinsymbol)
        {
            return await _walletRepository.User_Get_Wallet<TResult>(userid, coinsymbol);
        }
        
    }
}