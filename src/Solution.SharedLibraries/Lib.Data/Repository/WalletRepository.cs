using System.Linq;
using System.Data;
using Lib.Domain.Coins;
using Lib.Domain;
using System.Threading.Tasks;
using System.Collections.Generic;
using Web.SourceCoin.Common;
using Lib.Domain.DataContract;
using Newtonsoft.Json;
using Lib.Domain.CommonClass;
using System.Data.Entity;
using Web.SourceCoin.Entitys;
using LibDatabaseEntitys;
//using Web.SourceCoin.Entitys;
//using Web.SourceCoin.Entitys;
//

namespace Lib.Data.Repository.Tasks
{
    public interface IWalletRepository
    {
        Task<int> User_CreateWallet_With_Privatekey(int userid, CoinList coin);
        Task<TResult> User_Get_Wallet<TResult>(int userid, string coinsymbol);
    }

    public class WalletRepository : BaseDbContext, IWalletRepository
    {
        //public WalletRepository(CoreExchangeDB dbcontext) : base(dbcontext)
        //{

        //}
        public async Task<int> User_CreateWallet_With_Privatekey(int userid, CoinList coin)
        {
            switch (coin.TypeCoin)
            {
                case "ERC20":
                    return await User_WalletGenerate_ERC20(userid, coin);
                // break;
                case "ETH":
                    return await User_WalletGenerate_ETH(userid, coin);
                default:
                    return -1;

            }
        }
        
        public async Task<TResult> User_Get_Wallet<TResult>(int userid, string coinsymbol)
        {
            switch (coinsymbol)
            {
                case "ETH":
                    var wallet = _db.Wallet_ETH_General.Where(p => p.UserId == userid).
                        Select(s => new DataMemberWalletETH
                        {
                            CoinAddress = s.CoinAddress,
                            CoinName = s.CoinName,
                            CoinSymbol = s.CoinSymbol
                        }).FirstOrDefault();
                    if (wallet != null)
                    {
                        return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet));
                    }
                    else
                    {
                        var coiname = _db.CoinLists.Where(p => p.CoinSymbol == coinsymbol).FirstOrDefault();
                        var insertResult = await User_CreateWallet_With_Privatekey(userid, coiname);
                        if (insertResult > 0)
                        {
                            var wallet2 = _db.Wallet_ETH_General.Where(p => p.UserId == userid).FirstOrDefault();
                            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet2));
                        }

                    }
                    break;
               
                case "USDT":
                    var walletUSDT = _db.Wallet_USDT.Where(p => p.UserId == userid)
                        .Select(s => new DataMemberWalletUSDT
                        {
                            CoinAddress = s.CoinAddress,
                            CoinName = s.CoinName,
                            CoinSymbol = s.CoinSymbol
                        })
                        .FirstOrDefault();
                    if (walletUSDT != null)
                    {
                        return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(walletUSDT));
                    }
                    else
                    {
                        var coiname = _db.CoinLists.Where(p => p.CoinSymbol == coinsymbol).FirstOrDefault();
                        var insertResult = await User_CreateWallet_With_Privatekey(userid, coiname);
                        if (insertResult > 0)
                        {
                            var wallet2 = _db.Wallet_USDT.Where(p => p.UserId == userid).FirstOrDefault();
                            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet2));
                        }

                    }
                    break;
                case "GES":
                    var walletGES = _db.Wallet_GES.Where(p => p.UserId == userid)
                        .Select(s => new DataMemberWalletGES
                        {
                            CoinAddress = s.CoinAddress,
                            CoinName = s.CoinName,
                            CoinSymbol = s.CoinSymbol
                        }).FirstOrDefault();
                    if (walletGES != null)
                    {
                        return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(walletGES));
                    }
                    else
                    {
                        var coiname = _db.CoinLists.Where(p => p.CoinSymbol == coinsymbol).FirstOrDefault();
                        var insertResult = await User_CreateWallet_With_Privatekey(userid, coiname);
                        if (insertResult > 0)
                        {
                            var wallet2 = _db.Wallet_GES.Where(p => p.UserId == userid).FirstOrDefault();
                            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet2));
                        }

                    }
                    break;
                case "ELD":
                    var walletELD = _db.Wallet_ELD.Where(p => p.UserId == userid)
                        .Select(s => new DataMemberWalletELD
                        {
                            CoinAddress = s.CoinAddress,
                            CoinName = s.CoinName,
                            CoinSymbol = s.CoinSymbol
                        }).FirstOrDefault();
                    if (walletELD != null)
                    {
                        return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(walletELD));
                    }
                    else
                    {
                        var coiname = _db.CoinLists.Where(p => p.CoinSymbol == coinsymbol).FirstOrDefault();
                        var insertResult = await User_CreateWallet_With_Privatekey(userid, coiname);
                        if (insertResult > 0)
                        {
                            var wallet2 = _db.Wallet_ELD.Where(p => p.UserId == userid).FirstOrDefault();
                            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet2));
                        }

                    }
                    break;
                case "BRI":
                    var walletBRI = _db.Wallet_BRI.Where(p => p.UserId == userid)
                        .Select(s => new DataMemberWalletBRI
                        {
                            CoinAddress = s.CoinAddress,
                            CoinName = s.CoinName,
                            CoinSymbol = s.CoinSymbol
                        }).FirstOrDefault();
                    if (walletBRI != null)
                    {
                        return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(walletBRI));
                    }
                    else
                    {
                        var coiname = _db.CoinLists.Where(p => p.CoinSymbol == coinsymbol).FirstOrDefault();
                        var insertResult = await User_CreateWallet_With_Privatekey(userid, coiname);
                        if (insertResult > 0)
                        {
                            var wallet2 = _db.Wallet_BRI.Where(p => p.UserId == userid).FirstOrDefault();
                            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(wallet2));
                        }

                    }
                    break;
            }
            //await Task.Delay(10);
            object obj = new object();
            return JsonConvert.DeserializeObject<TResult>(Commons.SerializeObject(obj));
        }

        #region private function
        /// <summary>
        /// Chỉ cần tạo 1 địa chỉ ví và private key của ETHEREUM thì có thể dùng chung cho tất cả các loại Token ERC20
        /// Gọi hàm này lúc mở màn hình deposit.
        /// </summary>
        /// <param name="userid"></param>
        /// <param name="coin"></param>
        /// <returns></returns>

        private async Task<int> User_WalletGenerate_ETH(int userid, CoinList coin)
        {
            var wallet = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == coin.CoinSymbol).FirstOrDefault();
            if (wallet == null)
            {
                wallet = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    wallet.CoinAddress = objwallet.payload.address;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.CoinPrivateKey = objwallet.payload.privateKey;
                    wallet.CoinPublicKey = objwallet.payload.publicKey;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    wallet.UserId = userid;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    await _db.SaveChangesAsync();
                    return 1;
                }
            }
            return -1;
        }

        private async Task<int> User_WalletGenerate_ERC20(int userid, CoinList coin)
        {
            // tao vi erc20 va luu vao bang token erc20 theo loai COIN
            switch (coin.CoinSymbol)
            {
                case "GES":
                    return await WALLET_ERC20_GES_Ins(userid, coin);
                case "USDT":
                    return await WALLET_ERC20_USDT_Ins(userid, coin);

                case "BNB":
                    return await WALLET_ERC20_BNB_Ins(userid, coin);
                case "ELD":
                    return await WALLET_ERC20_ELD_Ins(userid, coin);
                case "BRI":
                    return await WALLET_ERC20_BRI_Ins(userid, coin);
                default:
                    return -1;
            }
        }

        private async Task<int> WALLET_ERC20_GES_Ins(int userid, CoinList coin)
        {
            Wallet_GES wallet = new Wallet_GES();
            var walleteth = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
            //Vì loại coin này là ERC20 nên phải  Kiểm tra ví ETH của account này đã dc tạo hay chưa. Nếu tạo rồi thì copy địa chỉ ví ETh qua cho loại coin nay.

            if (walleteth != null)
            {
                wallet.CoinAddress = walleteth.CoinAddress;
                wallet.CoinContract = coin.CoinContract;
                wallet.CoinName = coin.CoinName;
                wallet.UserId = userid;
                wallet.CoinSymbol = coin.CoinSymbol;
                _db.Entry(wallet).State = System.Data.Entity.EntityState.Added;
                //savechange to Database
                int result = await _db.SaveChangesAsync();
                return result;
            }
            else // Ngược lại nếu chưa có ví ETH thì phải tạo ví ETH trước, rồi sau đó copy ví ETH vừa tạo insert vào cho coin này.
            {
                walleteth = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    walleteth.CoinAddress = objwallet.payload.address;
                    walleteth.CoinContract = "";
                    walleteth.CoinName = Constants.COIN_NAME_ETH;
                    walleteth.CoinPrivateKey = objwallet.payload.privateKey;
                    walleteth.CoinPublicKey = objwallet.payload.publicKey;
                    walleteth.CoinSymbol = Constants.COIN_SYMBOL_ETH;
                    walleteth.UserId = userid;
                    _db.Entry(walleteth).State = EntityState.Added;

                    wallet.CoinAddress = walleteth.CoinAddress;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.UserId = userid;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    await _db.SaveChangesAsync();
                    return 1;
                }
            }
            return -1;
        }
        private async Task<int> WALLET_ERC20_ELD_Ins(int userid, CoinList coin)
        {
            Wallet_ELD wallet = new Wallet_ELD();
            var walleteth = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
            //Vì loại coin này là ERC20 nên phải  Kiểm tra ví ETH của account này đã dc tạo hay chưa. Nếu tạo rồi thì copy địa chỉ ví ETh qua cho loại coin nay.

            if (walleteth != null)
            {
                wallet.CoinAddress = walleteth.CoinAddress;
                wallet.CoinContract = coin.CoinContract;
                wallet.CoinName = coin.CoinName;
                wallet.UserId = userid;
                wallet.CoinSymbol = coin.CoinSymbol;
                _db.Entry(wallet).State = System.Data.Entity.EntityState.Added;
                //savechange to Database
                int result = await _db.SaveChangesAsync();
                return result;
            }
            else // Ngược lại nếu chưa có ví ETH thì phải tạo ví ETH trước, rồi sau đó copy ví ETH vừa tạo insert vào cho coin này.
            {
                walleteth = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    walleteth.CoinAddress = objwallet.payload.address;
                    walleteth.CoinContract = "";
                    walleteth.CoinName = Constants.COIN_NAME_ETH;
                    walleteth.CoinPrivateKey = objwallet.payload.privateKey;
                    walleteth.CoinPublicKey = objwallet.payload.publicKey;
                    walleteth.CoinSymbol = Constants.COIN_SYMBOL_ETH;
                    walleteth.UserId = userid;
                    _db.Entry(walleteth).State = EntityState.Added;

                    wallet.CoinAddress = walleteth.CoinAddress;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.UserId = userid;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    await _db.SaveChangesAsync();
                    return 1;
                }
            }
            return -1;
        }
        private async Task<int> WALLET_ERC20_BRI_Ins(int userid, CoinList coin)
        {
            Wallet_BRI wallet = new Wallet_BRI();
            var walleteth = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
            //Vì loại coin này là ERC20 nên phải  Kiểm tra ví ETH của account này đã dc tạo hay chưa. Nếu tạo rồi thì copy địa chỉ ví ETh qua cho loại coin nay.

            if (walleteth != null)
            {
                wallet.CoinAddress = walleteth.CoinAddress;
                wallet.CoinContract = coin.CoinContract;
                wallet.CoinName = coin.CoinName;
                wallet.UserId = userid;
                wallet.CoinSymbol = coin.CoinSymbol;
                _db.Entry(wallet).State = System.Data.Entity.EntityState.Added;
                //savechange to Database
                int result = await _db.SaveChangesAsync();
                return result;
            }
            else // Ngược lại nếu chưa có ví ETH thì phải tạo ví ETH trước, rồi sau đó copy ví ETH vừa tạo insert vào cho coin này.
            {
                walleteth = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    walleteth.CoinAddress = objwallet.payload.address;
                    walleteth.CoinContract = "";
                    walleteth.CoinName = Constants.COIN_NAME_ETH;
                    walleteth.CoinPrivateKey = objwallet.payload.privateKey;
                    walleteth.CoinPublicKey = objwallet.payload.publicKey;
                    walleteth.CoinSymbol = Constants.COIN_SYMBOL_ETH;
                    walleteth.UserId = userid;
                    _db.Entry(walleteth).State = EntityState.Added;

                    wallet.CoinAddress = walleteth.CoinAddress;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.UserId = userid;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    await _db.SaveChangesAsync();
                    return 1;
                }
            }
            return -1;
        }
        private async Task<int> WALLET_ERC20_USDT_Ins(int userid, CoinList coin)
        {
            Wallet_USDT wallet = new Wallet_USDT();
            var walleteth = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
            //Vì loại coin này là ERC20 nên phải  Kiểm tra ví ETH của account này đã dc tạo hay chưa. Nếu tạo rồi thì copy địa chỉ ví ETh qua cho loại coin nay.

            if (walleteth != null)
            {
                wallet.CoinAddress = walleteth.CoinAddress;
                wallet.CoinContract = coin.CoinContract;
                wallet.CoinName = coin.CoinName;
                wallet.UserId = userid;
                wallet.CoinSymbol = coin.CoinSymbol;
                _db.Entry(wallet).State = EntityState.Added;
                //savechange to Database
                int result = await _db.SaveChangesAsync();
                return result;
            }
            else // Ngược lại nếu chưa có ví ETH thì phải tạo ví ETH trước, rồi sau đó copy ví ETH vừa tạo insert vào cho coin này.
            {
                walleteth = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    walleteth.CoinAddress = objwallet.payload.address;
                    walleteth.CoinContract = "";
                    walleteth.CoinName = Constants.COIN_NAME_ETH;
                    walleteth.CoinPrivateKey = objwallet.payload.privateKey;
                    walleteth.CoinPublicKey = objwallet.payload.publicKey;
                    walleteth.CoinSymbol = Constants.COIN_SYMBOL_ETH;
                    walleteth.UserId = userid;
                    _db.Entry(walleteth).State = EntityState.Added;

                    wallet.CoinAddress = walleteth.CoinAddress;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.UserId = userid;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    await _db.SaveChangesAsync();
                    return 1;
                }
            }
            return -1;
        }

        private async Task<int> WALLET_ERC20_BNB_Ins(int userid, CoinList coin)
        {
            Wallet_BNB wallet = new Wallet_BNB();
            var walleteth = _db.Wallet_ETH_General.Where(p => p.UserId == userid && p.CoinSymbol == Constants.COIN_SYMBOL_ETH).FirstOrDefault();
            //Vì loại coin này là ERC20 nên phải  Kiểm tra ví ETH của account này đã dc tạo hay chưa. Nếu tạo rồi thì copy địa chỉ ví ETh qua cho loại coin nay.

            if (walleteth != null)
            {
                wallet.CoinAddress = walleteth.CoinAddress;
                wallet.CoinContract = coin.CoinContract;
                wallet.CoinName = coin.CoinName;
                wallet.UserId = userid;
                wallet.CoinSymbol = coin.CoinSymbol;
                _db.Entry(wallet).State = EntityState.Added;
                //savechange to Database
                int result = await _db.SaveChangesAsync();
                return result;
            }
            else // Ngược lại nếu chưa có ví ETH thì phải tạo ví ETH trước, rồi sau đó copy ví ETH vừa tạo insert vào cho coin này.
            {
                walleteth = new Wallet_ETH_General();
                // call api to generate wallet
                CryptoapisClient client = new CryptoapisClient();
                var objwallet = client.EthWallet_Generate();
                if (objwallet != null)
                {
                    // create wallet with private key. bảng này dùng để xử lý quét lệnh deposit.

                    walleteth.CoinAddress = objwallet.payload.address;
                    walleteth.CoinContract = "";
                    walleteth.CoinName = Constants.COIN_NAME_ETH;
                    walleteth.CoinPrivateKey = objwallet.payload.privateKey;
                    walleteth.CoinPublicKey = objwallet.payload.publicKey;
                    walleteth.CoinSymbol = Constants.COIN_SYMBOL_ETH;
                    walleteth.UserId = userid;
                    _db.Entry(walleteth).State = EntityState.Added;

                    wallet.CoinAddress = walleteth.CoinAddress;
                    wallet.CoinContract = coin.CoinContract;
                    wallet.CoinName = coin.CoinName;
                    wallet.UserId = userid;
                    wallet.CoinSymbol = coin.CoinSymbol;
                    _db.Entry(wallet).State = EntityState.Added;
                    //savechange to Database
                    int result = await _db.SaveChangesAsync();
                    return result;
                }
            }
            return -1;
        }
        #endregion

        #region xử lý khớp lệnh cơ bản
        
        #endregion
    }
}
