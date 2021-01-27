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
using Lib.Domain.Withdraws;
using Lib.Domain.Common;

namespace Lib.Service.Service.Wallet
{
    public interface IWithdrawService
    {
        List<WithdrawList> WithdrawsGetby_Userid(ParammetersWithdrawModel param);
    }

    public class WithdrawService : IWithdrawService
    {
        private readonly IWithdrawRepository _withdrawRepository;

        public WithdrawService(IWithdrawRepository withdrawRepository)
        {
            _withdrawRepository = withdrawRepository;
        }

        
        public List<WithdrawList> WithdrawsGetby_Userid(ParammetersWithdrawModel param)
        {
            return _withdrawRepository.WithdrawsGetby_Userid(param);
        }
    }
}
 