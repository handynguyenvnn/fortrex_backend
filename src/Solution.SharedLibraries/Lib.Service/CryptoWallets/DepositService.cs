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
using Lib.Data.Repository.TreeDatas;
using Lib.Domain.Trees;
using Lib.Data.Repository.Tasks;
using Web.SourceCoin.Entity;
using Lib.Domain.Coins.Deposit;

namespace Lib.Service.Service.Wallet
{
    public interface IDepositService
    {
        Task<int> Wallet_Ethereum_lst();
        Task<int> Wallet_ERC20_lst();
        List<DepositList> DepositsGetby_Userid(int userid);
    }

    public class DepositService : IDepositService
    {
        private readonly IDepositRepository _depositRepository;

        public DepositService(IDepositRepository depositRepository)
        {
            _depositRepository = depositRepository;
        }

        public async Task<int> Wallet_Ethereum_lst()
        {
            return await _depositRepository.Wallet_Ethereum_lst();
        }
        public async Task<int> Wallet_ERC20_lst()
        {
            return await _depositRepository.Wallet_ERC20_lst();
        }
        public List<DepositList> DepositsGetby_Userid(int userid)
        {
            return _depositRepository.DepositsGetby_Userid(userid);
        }
    }
}
 