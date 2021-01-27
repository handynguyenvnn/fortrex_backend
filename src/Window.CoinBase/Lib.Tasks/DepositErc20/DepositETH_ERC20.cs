using System;
using System.Linq;
using System.Data;
using CoinbaseConnector;
using Web.SourceCoin.Common;
using System.Data.Entity;
using Lib.Data.Repository.Tasks;
using LibDatabaseEntitys;
using Web.SourceCoin.Entitys;
using Lib.Domain;

namespace Lib.Tasks.Deposit
{
    public class DepositETH_ERC20 : BaseDbContext, ITask
    //public class DepositETH_ERC20 : ITask
    {
        public DepositETH_ERC20()
        {
            
        }
        public void Execute()
        {
          
            //Wallet_Ethereum_lst();
            Wallet_ERC20_lst();
        }
        /// <summary>
        /// Quet Deposit coin Ethereum. Chỉ dành cho Ethereum (ETH)
        /// </summary>
        /// <returns></returns>
        public void Wallet_Ethereum_lst()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                //CoreExchangeDB _db = new CoreExchangeDB();
                var wallets = _db.Wallet_ETH_General.ToList();
                Erc20ApisClient client = new Erc20ApisClient();
                var logtxHash = _db.Log_EthHashLog.Select(s => s.txHash).ToList();
                // get danh sách coin bnct
                foreach (var add in wallets)
                {
                    var obj = client.EthTransactionByAddress(add.CoinAddress);
                    if (obj != null)
                    {
                        var result = obj.result.Where(p => !logtxHash.Contains(p.hash)).ToList();
                        foreach (var item in result)
                        {
                            var transactions = client.GetTransactionInfo(item.hash);
                            if (transactions != null)
                            {
                                // kiểm tra ví = ví
                                var toaddress = item.to;
                                if (add.CoinAddress.Equals(toaddress))
                                {
                                    var confirm = item.confirmations;
                                    var coinvalueDeposit = transactions.value;
                                    if (confirm >= 0 && confirm < 20)
                                    {
                                        DepositProgress entity = new DepositProgress();
                                        var isExists = _db.DepositProgresses.Where(p => p.TxHash == item.hash).FirstOrDefault();
                                        if (isExists == null)
                                        {
                                            entity.AmountUSD = 0;
                                            entity.Confirmations = confirm;
                                            entity.timestamp = (int?)transactions.timeStamp;
                                            entity.TxHash = transactions.hash;
                                            entity.UserId = add.UserId;
                                            entity.FromAddress = item.from;
                                            entity.WalletAddress = toaddress;
                                            entity.WalletType = Constants.COIN_SYMBOL_ETH;
                                            entity.CoinValue = coinvalueDeposit;
                                            entity.FillConfirm = 20;
                                            entity.Success = transactions.success;
                                            _db.Entry(entity).State = EntityState.Added;
                                        }
                                        else
                                        {
                                            isExists.Success = (bool?)transactions.success;
                                            isExists.Confirmations = confirm;
                                            _db.Entry(isExists).State = EntityState.Modified;
                                        }
                                        _db.SaveChangesAsync();
                                    }
                                    else if (confirm > 20)
                                    {
                                        DepositHistory entity = new DepositHistory();
                                        var isExists = _db.DepositHistories.Where(p => p.TxHash == item.hash).FirstOrDefault();
                                        if (isExists == null)
                                        {
                                            // thêm vào kết quả deposit
                                            entity.AmountUSD = 0;
                                            entity.Confirmations = confirm;
                                            entity.Timestamp = (int?)transactions.timeStamp;
                                            entity.TxHash = transactions.hash;
                                            entity.UserId = add.UserId;
                                            entity.FromAddress = item.from;
                                            entity.WalletAddress = toaddress;
                                            entity.WalletType = Constants.COIN_SYMBOL_ETH;
                                            entity.CoinValue = coinvalueDeposit;
                                            entity.FillConfirm = 20;
                                            entity.Success = transactions.success;
                                            _db.Entry(entity).State = EntityState.Added;
                                            // Thêm vào log txhash
                                            Log_Erc20HashLog logHash = new Log_Erc20HashLog();
                                            logHash.timestamp = (int)transactions.timeStamp;
                                            logHash.txHash = transactions.hash;
                                            logHash.UserId = add.UserId;
                                            _db.Entry(logHash).State = EntityState.Added;
                                            // Thêm vào coinvalue trương ứng cho UserId
                                            //var getwallet = _db.User_Wallet.Where(w => w.UserId == add.UserId && w.WalletType == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
                                            //if (getwallet == null)
                                            //{
                                            //    //***** cập nhật chỗ này cho số dư ví usdt

                                            //    //User_WalletEntity wallet = new User_WalletEntity();
                                            //    //wallet.UserId = add.UserId;
                                            //    //wallet.WalletAddress = add.CoinAddress;
                                            //    //wallet.WalletType = Constants.COIN_SYMBOL_ETH;
                                            //    //wallet.LastAmount = 0;
                                            //    //wallet.Amount = coinvalueDeposit;
                                            //    //_db.Entry(wallet).State = EntityState.Added;

                                            //}
                                            //else
                                            //{
                                            //    getwallet.LastAmount = getwallet.Amount;
                                            //    getwallet.Amount = getwallet.Amount + coinvalueDeposit;
                                            //    _db.Entry(getwallet).State = EntityState.Modified;
                                            //}
                                            // save data after finished
                                            _db.SaveChangesAsync();
                                            // send email
                                            ////code send mail here
                                            //var user = _db.MUsers.Where(u => u.Id == add.UserId).FirstOrDefault();
                                            //string body, template = "";

                                            //template = "";
                                            //template += "Hi, " + user.FullName ;
                                            //template += string.Format("You have successfully deposited {0} {1} into Fortrex.com",coinvalueDeposit,item.tokenSymbol);

                                            //var mail = new Email
                                            //{
                                            //    Title = "[Fortrex.com] - Deposit success",
                                            //    Body = template,
                                            //    EmailTo = user.Email
                                            //};

                                        }
                                        else
                                        {
                                            // Neu da confirm = thanh cong, thi chi can cap nhat lai so luong confirm cua giao dich va kiem tra ma hash da ton tai hay chua
                                            // cu moi ngay mot lan la du.
                                            DateTime date = DateTime.Now;
                                            if (date.Hour == 1 && date.Minute == 5 && date.Second < 30)
                                            {
                                                isExists.Success = (bool?)transactions.success;
                                                isExists.Confirmations = confirm;
                                                _db.Entry(isExists).State = EntityState.Modified;

                                                // Thêm vào log txhash
                                                var checklog = _db.Log_EthHashLog.Where(p => p.txHash == transactions.hash).FirstOrDefault();
                                                if (checklog == null)
                                                {
                                                    Log_Erc20HashLog logHash = new Log_Erc20HashLog();
                                                    logHash.timestamp = (int)transactions.timeStamp;
                                                    logHash.txHash = transactions.hash;
                                                    logHash.UserId = add.UserId;
                                                    _db.Entry(logHash).State = EntityState.Added;
                                                }
                                                _db.SaveChangesAsync();
                                            }
                                        }
                                    }
                                }
                            }
                        }


                    }
                }
            }
            catch (Exception ex)
            {
                _task.ErrorLog_Insert(0, ex.Message, "Wallet_Ethereum_lst", 500);
            }
        }
        // <summary>
        /// Quet Deposit coin ERC20. Áp dụng quét deposit cho tất cả các Ví coin ERC20. Ngoại trừ Ethereum (ETH)
        /// </summary>
        /// <returns></returns>
        public void Wallet_ERC20_lst()
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                //CoreExchangeDB _db = new CoreExchangeDB();
                var coinlist = _db.CoinLists.Where(p => p.TypeCoin.Equals(Constants.TYPECOIN_ERC20)).ToList();
                foreach (var tokenItem in coinlist)
                {
                    string contractToken = tokenItem.CoinContract;
                    var wallets = _db.Wallet_ETH_General.AsNoTracking().ToList();
                    Erc20ApisClient client = new Erc20ApisClient();
                    var logtxHash = _db.Log_Erc20HashLog.AsNoTracking().Select(s => s.txHash).ToList();
                    // get danh sách coin
                    foreach (var add in wallets)
                    {
                        var obj = client.Getlastaddressoperations(contractToken, add.CoinAddress);
                        if (obj != null)
                        {
                            var result = obj.operations.Where(p => !logtxHash.Contains(p.transactionHash)).ToList();
                            foreach (var item in result)
                            {
                                // kiểm tra mã hợp đồng được quét từ ethscan phải tồn tại trong bảng coinlist của sàn.
                                // Nếu là coin ETH thì không cần so sánh mã hợp đồng, ngược lại phải so sánh coinname và coincontract phải tồn tại trong bảng coinlist

                                // var coinlist = _db.CoinLists.Where(c => c.CoinSymbol == item.tokenInfo.symbol
                                //                                 && (c.CoinContract == item.tokenInfo.address && !c.CoinSymbol.Equals("ETH"))).FirstOrDefault();
                                //if (coinlist != null)
                                if (tokenItem.CoinSymbol.ToLower().Equals(item.tokenInfo.symbol.ToLower()) &&
                                    tokenItem.CoinContract.ToLower().Equals(item.tokenInfo.address.ToLower()))
                                {
                                    var transactionsInfo = client.GetTransactionInfo(item.transactionHash);
                                    if (transactionsInfo != null)
                                    {
                                        // kiểm tra ví = ví
                                        var toaddress = item.to; //transactions.operations.FirstOrDefault().to;
                                        if (add.CoinAddress.Equals(toaddress))
                                        {
                                            var confirm = transactionsInfo.confirmations;
                                            var setcoinvalue = "1";
                                            for (int i = 0; i < item.tokenInfo.decimals; i++)
                                            {
                                                setcoinvalue += "0";
                                            }
                                            var coinvalueDeposit = item.value / decimal.Parse(setcoinvalue);
                                            if (confirm >= 0 && confirm < 12)
                                            {
                                                DepositProgress entity = new DepositProgress();
                                                var isExists = _db.DepositProgresses.Where(p => p.TxHash == item.transactionHash).FirstOrDefault();
                                                if (isExists == null)
                                                {
                                                    entity.AmountUSD = 0;
                                                    entity.Confirmations = confirm;
                                                    entity.timestamp = (int?)transactionsInfo.timeStamp;
                                                    entity.TxHash = transactionsInfo.hash;
                                                    entity.UserId = add.UserId;
                                                    entity.FromAddress = item.from;
                                                    entity.WalletAddress = toaddress;
                                                    entity.WalletType = item.tokenInfo.symbol;
                                                    entity.CoinValue = coinvalueDeposit;
                                                    entity.FillConfirm = 12;
                                                    entity.Success = transactionsInfo.success;
                                                    _db.Entry(entity).State = EntityState.Added;
                                                }
                                                else
                                                {
                                                    isExists.Success = (bool?)transactionsInfo.success;
                                                    isExists.Confirmations = confirm;
                                                    _db.Entry(isExists).State = EntityState.Modified;
                                                }
                                                _db.SaveChangesAsync();
                                            }
                                            else if (confirm >= 12)
                                            {
                                                var isExistsPedingDeposit = _db.DepositProgresses.Where(p => p.TxHash == item.transactionHash && p.Confirmations < 12).FirstOrDefault();
                                                if (isExistsPedingDeposit != null)
                                                {
                                                    //isExistsPedingDeposit.Success = (bool?)transactions.success;
                                                    isExistsPedingDeposit.Confirmations = confirm;
                                                    _db.Entry(isExistsPedingDeposit).State = EntityState.Modified;
                                                    _db.SaveChanges();
                                                }
                                                DepositHistory entity = new DepositHistory();
                                                var isExists = _db.DepositHistories.Where(p => p.TxHash == item.transactionHash).FirstOrDefault();
                                                if (isExists == null)
                                                {
                                                    // thêm vào kết quả deposit
                                                    entity.AmountUSD = 0;
                                                    entity.Confirmations = confirm;
                                                    entity.Timestamp = (int?)item.timestamp;
                                                    entity.TxHash = transactionsInfo.hash;
                                                    entity.UserId = add.UserId;
                                                    entity.FromAddress = item.from;
                                                    entity.WalletAddress = toaddress;
                                                    entity.WalletType = item.tokenInfo.symbol;
                                                    entity.CoinValue = coinvalueDeposit;
                                                    entity.FillConfirm = 12;
                                                    //entity.Success = transactions.success;
                                                    _db.Entry(entity).State = EntityState.Added;
                                                    // Thêm vào log txhash
                                                    Log_Erc20HashLog logErc20Hash = new Log_Erc20HashLog();
                                                    logErc20Hash.timestamp = (int)transactionsInfo.timeStamp;
                                                    logErc20Hash.txHash = transactionsInfo.hash;
                                                    logErc20Hash.UserId = add.UserId;
                                                    _db.Entry(logErc20Hash).State = EntityState.Added;
                                                    //thêm vào lịch sử
                                                    Random rd = new Random();
                                                    HistoryTransactionEntity history = new HistoryTransactionEntity();
                                                    history.UserId = add.UserId;
                                                    history.Amount = coinvalueDeposit;
                                                    history.FromUserId = add.UserId;
                                                    history.Description = "Deposit from "+ item.tokenInfo.symbol + " Wallet";
                                                    history.Type = 3;
                                                    history.Status = 1;
                                                    history.CreateOn = DateTime.Now;
                                                    history.UpdateOn = DateTime.Now;
                                                    history.CoinBaseTransactionId = rd.Next(111111, 9999999).ToString();
                                                    _db.Entry(history).State = EntityState.Added;

                                                    // Thêm vào coinvalue tương ứng cho UserId
                                                    // kiểm tra thông tin trong txhash. Nếu operations.isEth==false thì txhash này là của Token erc20.
                                                    // Ngược lại nếu 
                                                    var getwallet = _db.User_WalletAddress.Where(w => w.UserId == add.UserId).FirstOrDefault();
                                                    if (getwallet != null && Constants.COIN_SYMBOL_USDT.Equals(item.tokenInfo.symbol))
                                                    {
                                                        getwallet.MoneyUSD += coinvalueDeposit;
                                                        _db.Entry(getwallet).State = EntityState.Modified;
                                                    }
                                                    else if (getwallet != null && Constants.COIN_SYMBOL_GES.Equals(item.tokenInfo.symbol))
                                                    {
                                                        decimal balancestw = getwallet.MoneyGES;
                                                        getwallet.MoneyGES = balancestw + coinvalueDeposit;
                                                        _db.Entry(getwallet).State = EntityState.Modified;
                                                    }
                                                    else if (getwallet != null && Constants.COIN_SYMBOL_ELD.Equals(item.tokenInfo.symbol))
                                                    {
                                                        decimal balancestw = getwallet.MoneyELD;
                                                        getwallet.MoneyELD = balancestw + coinvalueDeposit;
                                                        _db.Entry(getwallet).State = EntityState.Modified;
                                                    }
                                                    else if (getwallet != null && Constants.COIN_SYMBOL_BRI.Equals(item.tokenInfo.symbol))
                                                    {
                                                        decimal balancestw = getwallet.MoneyBRI;
                                                        getwallet.MoneyBRI = balancestw + coinvalueDeposit;
                                                        _db.Entry(getwallet).State = EntityState.Modified;
                                                    }

                                                    // save data after finished
                                                    _db.SaveChanges();
                                                    // send email
                                                    ////code send mail here
                                                    //var user = _db.MUsers.Where(u => u.Id == add.UserId).FirstOrDefault();
                                                    //string body, template = "";

                                                    //template = "";
                                                    //template += "Hi, " + user.FullName ;
                                                    //template += string.Format("You have successfully deposited {0} {1} into Fortrex.com",coinvalueDeposit,item.tokenSymbol);

                                                    //var mail = new Email
                                                    //{
                                                    //    Title = "[] - Deposit success",
                                                    //    Body = template,
                                                    //    EmailTo = user.Email
                                                    //};
                                                }
                                                else
                                                {
                                                    // Neu da confirm = thanh cong, thi chi can cap nhat lai so luong confirm cua giao dich va kiem tra ma hash da ton tai hay chua
                                                    // cu moi ngay mot lan la du.

                                                    //DateTime date = DateTime.Now;
                                                    //if (date.Hour == 1 && date.Minute == 5 && date.Second < 30)
                                                    //{
                                                    //    //isExists.Success = (bool?)transactions.success;
                                                    //    isExists.Confirmations = confirm;
                                                    //    _db.Entry(isExists).State = EntityState.Modified;

                                                    //    // Thêm vào log txhash
                                                    //    var checklog = _db.Log_Erc20HashLog.Where(p => p.txHash == item.transactionHash).FirstOrDefault();
                                                    //    if (checklog == null)
                                                    //    {
                                                    //        Log_Erc20HashLog logErc20Hash = new Log_Erc20HashLog();
                                                    //        logErc20Hash.timestamp = (int)transactionsInfo.timeStamp;
                                                    //        logErc20Hash.txHash = transactionsInfo.hash;
                                                    //        logErc20Hash.UserId = add.UserId;
                                                    //        _db.Entry(logErc20Hash).State = EntityState.Added;
                                                    //    }
                                                    //    _db.SaveChangesAsync();
                                                    //}
                                                }
                                            }
                                        }
                                    }
                                }

                            }


                        }
                    }
                }
               
                
                
            }
            catch (System.Exception ex)
            {
                _task.ErrorLog_Insert(0, ex.Message, "Wallet_ERC20_lst", 500);
            }
        }

    }
}
