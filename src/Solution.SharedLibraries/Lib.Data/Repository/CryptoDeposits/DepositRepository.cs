using System.Linq;
using System.Data;
using Lib.Domain;
using System.Data.Entity;
using System.Threading.Tasks;
using Lib.Domain.CommonClass;
using Web.SourceCoin.Common;
using System;
using System.Collections.Generic;
using Lib.Data.Repository.User;
using Lib.Domain.Coins.Deposit;
using Web.SourceCoin.Entitys;
using LibDatabaseEntitys;

namespace Lib.Data.Repository.Tasks
{

    public interface IDepositRepository
    {
        // List<DepositList> DepositsGetby_Userid(int userid,DateTime? from, DateTime? to);
        List<DepositList> DepositsGetby_Userid(int userid, int pageIndex, int pageSize, out int total, DateTime? from, DateTime? to, string walletName);
    }

    public class DepositRepository : BaseDbContext, IDepositRepository
    {
        public DepositRepository()
        {
        }


        public List<DepositList> DepositsGetby_Userid(int userid, int pageIndex, int pageSize, out int total, DateTime? fromdate, DateTime? todate, string walletName)
        {

            //long stampfromdate = long.Parse(fromdate);
            //long stamptodate = long.Parse(todate);

            int stampfromdate = (Int32)(DateTime.Parse(fromdate.Value.ToString("yyyy-MM-dd")).Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;

            int stamptodate = (Int32)(DateTime.Parse(todate.Value.ToString("yyyy-MM-dd")).Subtract(new DateTime(1970, 1, 1, 0, 0, 0))).TotalSeconds;

            var result = _db.DepositHistories.AsNoTracking()
                .Where(p => p.UserId == userid
                //&& (p.Timestamp>= stampfromdate && p.Timestamp<= stamptodate)
                && (p.WalletType.Contains(walletName))
                ).OrderByDescending(o => o.Timestamp).ToList();
            var deposits = _db.DepositProgresses.AsNoTracking()
                .Where(p => p.UserId == userid && p.Confirmations < 12
                 //&& (p.timestamp >= stampfromdate && p.timestamp <= stamptodate)
                 && (p.WalletType.Contains(walletName))
                 ).ToList();
            List<DepositList> depositHistory = new List<DepositList>();
            foreach (var item in deposits)
            {
                //var checkcompleted = result.Where(p=>p.TxHash==item.TxHash).Any();
                //if (!checkcompleted)
                //{
                //    DepositList entity = new DepositList();
                //    entity.UserId = item.UserId;
                //    entity.WalletType = item.WalletType;
                //    entity.CoinValue = item.CoinValue;
                //    entity.AmountUSD = item.AmountUSD;
                //    entity.WalletAddress = item.WalletAddress;
                //    entity.TxHash = item.TxHash;
                //    entity.FillConfirm = item.FillConfirm;
                //    entity.Confirmations = item.Confirmations;
                //    entity.Success = item.Success;
                //    entity.CreateAt = Commons.getDateTimeFromUnixTimeStamp((uint)item.timestamp);
                //    depositHistory.Add(entity);
                //}
                DepositList entity = new DepositList();
                entity.UserId = item.UserId;
                entity.WalletType = item.WalletType;
                entity.CoinValue = item.CoinValue;
                //entity.AmountUSD = item.AmountUSD;
                entity.WalletAddress = item.WalletAddress;
                entity.FromAddress = item.FromAddress;
                entity.TxHash = item.TxHash;
                entity.FillConfirm = item.FillConfirm;
                entity.Confirmations = item.Confirmations;
                entity.Success = item.Success;
                entity.CreateAt = Commons.TimeStampToDateTime((uint)item.timestamp);
                depositHistory.Add(entity);
            }
            foreach (var item in result)
            {
                DepositList entity = new DepositList();
                entity.UserId = item.UserId;
                entity.WalletType = item.WalletType;
                entity.CoinValue = item.CoinValue;
                //entity.AmountUSD = item.AmountUSD;
                entity.WalletAddress = item.WalletAddress;
                entity.FromAddress = item.FromAddress;
                entity.TxHash = item.TxHash;
                entity.FillConfirm = item.FillConfirm;
                entity.Confirmations = item.Confirmations;
                entity.Success = item.Success;
                entity.CreateAt = Commons.TimeStampToDateTime((uint)item.Timestamp);
               
                depositHistory.Add(entity);

            }
            total = depositHistory.Count();
           
        var query = (from p in depositHistory.Skip(pageIndex).Take(pageSize)
                         join coin in _db.CoinLists.AsNoTracking() on p.WalletType equals coin.CoinSymbol into coinname
                         from type in coinname.DefaultIfEmpty()
                         select new DepositList
                         {
                            // AmountUSD = p.AmountUSD,
                             // BlockNumber=p.BlockNumber,
                             CoinType = type.TypeCoin,
                             CoinValue = p.CoinValue,
                             CompletedAt = p.CompletedAt,
                             Confirmations = p.Confirmations,
                             CreateAt = p.CreateAt,
                             FillConfirm = p.FillConfirm,
                             FromAddress = p.FromAddress,
                             Id = p.Id,
                             Success = p.Success,
                             timestamp = p.timestamp,
                             TxHash = p.TxHash,
                             UserId = p.UserId,
                             WalletAddress = p.WalletAddress,
                             WalletType = p.WalletType,
                             Status = p.Confirmations>=12?"Success": "Pending",
                             StrCreateDate = ((DateTime)p.CreateAt).ToString("yyyy-MM-dd HH:mm:ss"),
                             Amount = Commons.FormatNumber(p.CoinValue)
                         }).ToList();
            return query;
        }
    }
    
}