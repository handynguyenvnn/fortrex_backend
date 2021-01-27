using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Lib.Data.Repository.Tasks;
using Lib.Domain.Coins.Deposit;

namespace Lib.Service.Service.Wallet
{
    public interface IDepositService
    {
        //List<DepositList> DepositsGetby_Userid(int userid, DateTime? from, DateTime? to);
        List<DepositList> DepositsGetby_Userid(int userid, int pageIndex, int pageSize, out int total, DateTime? fromdate, DateTime? todate, string walletName);
    }

    public class DepositService : IDepositService
    {
        private readonly IDepositRepository _depositRepository;

        public DepositService(IDepositRepository depositRepository)
        {
            _depositRepository = depositRepository;
        }

        public List<DepositList> DepositsGetby_Userid(int userid, int pageIndex, int pageSize, out int total, DateTime? fromdate, DateTime? todate, string walletName)
        {
            return _depositRepository.DepositsGetby_Userid(userid, pageIndex, pageSize, out total, fromdate, todate, walletName);
        }
    }
}
 