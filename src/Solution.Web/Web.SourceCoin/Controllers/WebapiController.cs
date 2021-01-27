using Lib.Domain.ModelApi;
using Lib.Domain.Models;
using Lib.Domain.Packages.Trades;
using Lib.Domain.Response;
using Lib.Domain.Simples;
using Lib.Domain.TransactionHistorys;
using Lib.Domain.Transfers;
using Lib.Domain.User;
using Lib.Domain.Withdraws;
using Lib.Service.Service.Packages;
using Lib.Service.Service.TreeDatas;
using Lib.Service.Service.User;
using Lib.Service.Service.Wallet;
using Microsoft.AspNet.SignalR.Messaging;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;
using Web.AppAuth;
using Web.SourceCoin.Common;
using Web.SourceCoin.Helpers;
using Web.SourceCoin.Models;
using Web.SourceCoin.Models.Dashboards;
using Web.SourceCoin.Models.ModelApi;
using Web.SourceCoin.Models.Users;
using Lib.Domain.Packages;
using Lib.Domain;
using Web.SourceCoin.Models.Transfer;
using Lib.Domain.AsynTabs;
using System;
using LibDatabaseEntitys;
using Lib.Domain.DataContract;
using System.Threading.Tasks;
using Lib.Domain.Coins.Deposit;

namespace Web.SourceCoin.Controllers
{
    // xoa đoạn này đi, nó xạo đó.  chỉ cần cấu hình ở web.config
     //[EnableCors(origins: "https://fortrex.io,http://localhost:3001,http://localhost:3002", headers: "*", methods: "*")]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    public class WebapiController : BaseApiController
    {
        private readonly IPackagesService _packagesService;
        private readonly ITreeService _treeService;
        private readonly IDepositService _depositService;
        private readonly IWalletService _walletService;
        private ProcessData _action;
        private Helper _helper;
        public WebapiController(IUserService userService, IPackagesService packagesService, ITreeService treeService, IDepositService depositService
            , IWalletService walletService) : base(userService)
        {
            _packagesService = packagesService;
            _treeService = treeService;
            _depositService = depositService;
            _walletService = walletService;
            _action = new ProcessData(_userService, _packagesService, _treeService);
            _helper = new Helper();
        }
        private T GetRequestBody<T>()
        {
            try
            {
                using (var stream = new System.IO.MemoryStream())
                {
                    var context = (HttpContextBase)Request.Properties["MS_HttpContext"];
                    context.Request.InputStream.Seek(0, System.IO.SeekOrigin.Begin);
                    context.Request.InputStream.CopyTo(stream);
                    var body = System.Text.Encoding.UTF8.GetString(stream.ToArray());
                    return JsonConvert.DeserializeObject<T>(body);
                }
            }
            catch
            {
                return default(T);
            }
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Investment()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            InvestmentModel model = GetRequestBody<InvestmentModel>();
            var user = _userService.User_GetByUserId(currentUserId.Value);
            var meg = _action.Investment(model.Amount, user);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage InvestmentHistory()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            InvestmentHistoryModel model = GetRequestBody<InvestmentHistoryModel>();
            var meg = _action.InvestmentHistory(model.PageIndex, model.PageSize, currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Reply = new InvestmentPagingResponse
                {
                    Total = (int)meg.Optional,
                    Item = (List<InvestmentList>)meg.Result
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        public HttpResponseMessage GetUserTree(int id)
        {
            var data = _userService.UserTooltip_ById(id);
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Login()
        {
            LoginModel model = GetRequestBody<LoginModel>();
            Alert meg = _action.Login(model.Username, model.Password, model.TwoFACode, true, null, true);

            DataResponse response = new DataResponse();
            if (!meg.Success)
            {
                if (meg.EnableAuthy)
                {
                    response.StatusCode = HttpStatusCode.Forbidden;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.Unauthorized;

                }
                response.Meg = meg.Message;
            }
            else
            {
                response.Meg = "Login Success!";
                var user = (Lib.Domain.User.MUser)meg.Reply;
                response.Reply = new LoginResponse
                {
                    Username = model.Username,
                    Token = meg.Token,
                    Email = user.Email,
                    Fullname = user.FullName
                };
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);

        }
        // REGISTER
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Register()
        {
            RegisterModel model = GetRequestBody<RegisterModel>();
            Alert meg = _action.Register(model.ReferralCode, model.Fullname, model.Email, model.Username, model.Password, model.PasswordConfirm, model.Country, model.Phone);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(response.StatusCode, response);
        }
        // REGISTER BY Reflink.
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage PushOrder()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            PushOrderModel model = GetRequestBody<PushOrderModel>();
            var meg = _action.PushOrder(currentUserId.Value, model);
            DataResponse response = new DataResponse
            {
                Reply = meg,
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        #region Transfer 
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Transfer()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            TransfersFromToWalletModel model = GetRequestBody<TransfersFromToWalletModel>();
            model.UserIDForbit = currentUserId ?? 0;
            var meg = _action.Transfer(model);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
        #region chart
        [HttpPost]
       // [AllowAnonymous]
        [HMACAuthentication]
        public HttpResponseMessage MarketPrice()
        {
            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            var price = _packagesService.Candlestick_GetBy_Pair_LastTime(model.Pair);
            var data = new ResponsePrice
            {
                OPEN = price.Open,
                HIGH = price.High,
                LOW = price.Low,
                CLOSE = price.Close,
                TIMES = price.TimeOpen,
                VolumeFrom = price.VolumeFrom,
                VolumeTo = price.VolumeTo,
                LASTTIME = price.LastTimes,
            };
            return Request.CreateResponse(HttpStatusCode.OK, data);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Candlestick()
        {
            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            List<Candlesticks> candlesticks = _packagesService.Candlestick_GetBy_Pair(model.Pair, "", 300);
            return Request.CreateResponse(HttpStatusCode.OK, candlesticks.OrderBy(o => o.Times));
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage TradePairs()
        {
            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            var tickerPriceChange = _packagesService.TradePairs_Gets(model.Pair);

            return Request.CreateResponse(HttpStatusCode.OK, tickerPriceChange);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AppCandlestick()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            if (model.Item > 300)
            {
                model.Item = 300;
            }
            List<Candlesticks> candlesticks = _packagesService.Candlestick_GetBy_Pair(model.Pair, "", model.Item);
            DataResponse response = new DataResponse
            {
                Reply = model.Sortby.Equals(Constants.SORT_BY_ASC)?candlesticks.OrderBy(o => o.Times).ToList(): candlesticks
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AppMarketPrice()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            var price = _packagesService.Candlestick_GetBy_Pair_LastTime(model.Pair);
            var data = new ResponsePrice
            {
                OPEN = price.Open,
                HIGH = price.High,
                LOW = price.Low,
                CLOSE = price.Close,
                TIMES = price.TimeOpen,
                VolumeFrom = price.VolumeFrom,
                VolumeTo = price.VolumeTo,
                LASTTIME = price.LastTimes,
            };
            DataResponse response = new DataResponse
            {
                Reply = data
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AppTradePairs()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            var tickerPriceChange = _packagesService.TradePairs_Gets(model.Pair);
            DataResponse response = new DataResponse
            {
                Reply = tickerPriceChange
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage TradingLastResults()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
           // TradingLastResult lastResult = new TradingLastResult();
            var data = _packagesService.TradingLastResults(Constants.PAIR_DEFAULT);
            //lastResult._Down = data.Where(p => p == 1).Count();
            //lastResult._Up = data.Where(p => p == 2).Count();
            DataResponse response = new DataResponse
            {
                Reply= data
            };
            
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_PairName_by_UserId()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var pairName_lst = _packagesService.User_PairName_Mapping_Select(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Reply = pairName_lst.ToList()
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage PairName_Favorite_Ins()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            MarketPriceModel model = GetRequestBody<MarketPriceModel>();
            var result = _packagesService.PairName_Favorite_Ins(currentUserId.Value, model.Pair);
            DataResponse response = new DataResponse
            {
                Reply = result
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_Server_Time()
        {
            DataResponse response = new DataResponse
            {
                Reply = _userService.ServerGetTime()
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_Balance_By()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var balance = _userService.AccountBalance(currentUserId.Value, "C3");
            
            DataResponse response = new DataResponse
            {
                Reply = balance
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AppTradingList()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            PagingModel model = GetRequestBody<PagingModel>();
            int total = 0;
            string whereClause = string.Format(" and A.UserId = {0}", currentUserId.Value);
            var lst = _userService.Admin_Trading_List(
                model.PageIndex,
                model.PageSize,
                out total,
                whereClause);

            List<ResponseTradings> data = new List<ResponseTradings>();
            foreach (HighchartSyncTrade item in lst.Where(p => p.Status == model.Type ||  model.Type==-2))
            {
                //var color = item.Status == 1 ? "green" : item.Status == -1 ? "red" : "";
                data.Add(new ResponseTradings
                {
                    Id = item.Id,
                    IsCall = item.IsCall,
                    IsDemo = item.IsDemo,
                    symbol = item.symbol,
                    MarketName = item.MarketName,
                    PairName = item.PairName,
                    StatusName = ((TradingStatus)item.Status).ToString(),
                    Status = item.Status,
                    Amount = item.Amount,
                    CreateTimeStr = item.CreateOn.ToString("yyyy-MM-dd HH:mm:ss"),
                    Profit = item.Profit,
                    //BeginAmount = item.BeginAmount,
                    OpeningPrice = _helper.FormatNumber(item.BeginAmount),
                    ClosingPrice = _helper.FormatNumber(item.EndAmount),
                    CompleteOnStr = item.CompleteOn.ToString("yyyy-MM-dd HH:mm:ss"),
                    ByType = item.ByType
                });
            }

            DataResponse response = new DataResponse
            {
                Reply = new PagingResponseTradings
                {
                    Total = total,
                    Item = data
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #region Deposit
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Deposit_Gets()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            DepositModel model = GetRequestBody<DepositModel>();

            List<DepositListCoinNameResponse> walletResponse = new List<DepositListCoinNameResponse>();
            //var btcname = new DepositListCoinNameResponse();
            //btcname.Symbol = "BTC";
            //btcname.CoinName = "Bitcoin";
            //walletResponse.Add(btcname);
            //var ethname = new DepositListCoinNameResponse();
            //ethname.Symbol = "ETH";
            //ethname.CoinName = "Ethereum";
            //walletResponse.Add(ethname);
            var usdname = new DepositListCoinNameResponse();
            usdname.Symbol = "USDT";
            usdname.CoinName = "Tether (USDT)";
            walletResponse.Add(usdname);
            var gesname = new DepositListCoinNameResponse();
            gesname.Symbol = "GES";
            gesname.CoinName = "GES Token";
            walletResponse.Add(gesname);
            var eldname = new DepositListCoinNameResponse();
            eldname.Symbol = "ELD";
            eldname.CoinName = "ELD Token";
            walletResponse.Add(eldname);
            var briname = new DepositListCoinNameResponse();
            briname.Symbol = "BRI";
            briname.CoinName = "BRI Token";
            walletResponse.Add(briname);
            // end code get dia chi r ví
            DataResponse response = new DataResponse
            {
                Reply = walletResponse,
                Meg = "Success"
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_GetFromWallet()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            DepositModel model = GetRequestBody<DepositModel>();

            List<WithdrawGetFromWalletResponse> walletResponse = new List<WithdrawGetFromWalletResponse>();
            
            var usdname = new WithdrawGetFromWalletResponse();
            usdname.Symbol = "USDT";
            usdname.CoinName = "Tether (USDT)";
            walletResponse.Add(usdname);
            var gesname = new WithdrawGetFromWalletResponse();
            gesname.Symbol = "GES";
            gesname.CoinName = "GES Token";
            walletResponse.Add(gesname);
            var eldname = new WithdrawGetFromWalletResponse();
            eldname.Symbol = "ELD";
            eldname.CoinName = "ELD Token";
            walletResponse.Add(eldname);
            var  briname = new WithdrawGetFromWalletResponse();
            briname.Symbol = "BRI";
            briname.CoinName = "BRI Token";
            walletResponse.Add(briname);
            // end code get dia chi r ví
            DataResponse response = new DataResponse
            {
                Reply = walletResponse,
                Meg = "Success"
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public async Task<HttpResponseMessage> Deposit_Getby_Symbol()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            // tham số đầu vào là json: {"symbol":"BTC"}
            DepositModel model = GetRequestBody<DepositModel>();
            // đoạn code get địa chỉ ví ở đây
            string wallet = "", mes = "";
            //var dataWallet = _userService.User_WalletAddress_GetByUserId((int)currentUserId);
            //switch (model.Symbol.ToUpper())
            //{
            //    case "BTC":
            //        wallet = dataWallet.WalletBTC;
            //        break;
            //    case "ETH":
            //        wallet = dataWallet.WalletETH;
            //        break;
            //    case "USDT":
            //        wallet = dataWallet.WalletETH;
            //        break;
            //    case "GES":
            //        wallet = dataWallet.WalletETH;
            //        break;
            //    default:
            //        break;
            //}
            string symbol = !string.IsNullOrEmpty(model.Symbol)? model.Symbol.ToUpper():"";
            try
            {
                using (var _db = new CoreExchangeDB())
                {
                    //var result = new CustomJsonResult()
                    var coin = _db.CoinLists.Where(c => c.CoinSymbol == symbol.ToUpper()).FirstOrDefault();
                    if (coin != null)
                    {
                        // check wallet is exists in system
                        switch (symbol)
                        {
                            case "USDT":
                                var _usd = await _walletService.User_Get_Wallet<DataMemberWalletUSDT>((int)currentUserId, symbol);
                                wallet = _usd.CoinAddress;
                                break;
                            case "GES":
                                var _ges = await _walletService.User_Get_Wallet<DataMemberWalletGES>((int)currentUserId, symbol);
                                wallet = _ges.CoinAddress;
                                break;
                            case "ELD":
                                var _eld = await _walletService.User_Get_Wallet<DataMemberWalletELD>((int)currentUserId, symbol);
                                wallet = _eld.CoinAddress;
                                break;
                            case "BRI":
                                var _bri = await _walletService.User_Get_Wallet<DataMemberWalletBRI>((int)currentUserId, symbol);
                                wallet = _bri.CoinAddress;
                                break;
                            default:
                                break;
                        }
                    }
                }
            }
            catch 
            {
                wallet ="";
               // throw;
            }
            
            if (!string.IsNullOrEmpty(wallet))
            {

                mes = string.Format("Send only ERC-20 {0} to this deposit address. Sending coin or token other than ERC-20 {1} to this address may result in the loss of your deposit. <br />Average arrival time: depending on network", model.Symbol.ToUpper(), model.Symbol.ToUpper());
            }
            DeposiGetWalletResponse walletResponse = new DeposiGetWalletResponse();
            walletResponse.Symbol = model.Symbol.ToUpper();
            walletResponse.WalletAddress = wallet;
            walletResponse.CoinInfo = mes;
            // end code get dia chi r ví
            DataResponse response = new DataResponse
            {
                Reply = walletResponse,
                Meg = "Success"
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Deposit_Historys()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            // create page in history
            // declare model
            // tham số đầu vào là json: {"symbol":"BTC"}
            DepositHisotyModel model = GetRequestBody<DepositHisotyModel>();
            var defaultDate = new DateTime(1970, 1, 1);


            if (model.ToDate == null && model.FromDate == null)
            {


                model.FromDate = DateTime.Now.AddDays(-7);
                model.ToDate = DateTime.Now;

                //model.ToDate =defaultDate;
                //model.FromDate = DateTime.Now;
            }
            else if (model.ToDate == null && model.FromDate != null)
            {
                //  model.ToDate = defaultDate; 

                model.ToDate = DateTime.Now;
            }
            else if (model.ToDate != null && model.FromDate == null)
            {
                //model.ToDate = defaultDate;
                //model.FromDate = model.ToDate;

                model.FromDate = model.ToDate.Value.AddDays(-7);
            }

            int total = 0;
            var result = _depositService.DepositsGetby_Userid(
                (int)currentUserId,
                model.PageIndex,
                model.PageSize,
                out total,
                model.FromDate,
                model.ToDate,
                string.IsNullOrEmpty(model.WalletName) ? "" : model.WalletName
                );
           // List<DepositWithdrawsReponse> depositWithdraws = new List<DepositWithdrawsReponse>();
            DataResponse response = new DataResponse
            {
                Reply = new PagingResponseDepositHistorys
                {
                    Total = total,
                    Item = result
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AppTransactionHistoryList()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            PagingModel model = GetRequestBody<PagingModel>();
            int total = 0;
            string whereClause = string.Format(" and A.UserId = {0}", currentUserId.Value);
            var lst = _userService.Admin_HistoryTransaction_List(
                         model.PageIndex,
                         model.PageSize,
                         out total,
                         whereClause);
            List<ResponseHistoryTransaction> responsedata = new List<ResponseHistoryTransaction>();
            foreach (HistoryTransaction item in lst)
            {
                responsedata.Add(new ResponseHistoryTransaction
                {
                    Id = item.Id,
                    Amount = item.Amount,
                    //ByUserName = item.ByUserName,
                    CreateOn = item.CreateOn,
                    Description = item.Description,
                    FromUser = item.FromUser,
                    Status = item.Status,
                    StatusName = ((TransactionHistoryStatus)item.Status).ToString(),
                    Type = item.Type,
                    // TypeName = item.TypeName,
                    //StrAmount = item.Amount >= 0 ? "+$" + HelperCommon.NumberFormat(item.Amount) : "-$" + HelperCommon.NumberFormat((item.Amount * -1)),
                    //StrAmount = (item.Amount >= 0) ? (item.Type == 1 ? "-$" + HelperCommon.NumberFormat(item.Amount) : "+$" + HelperCommon.NumberFormat(item.Amount)) : ((item.Type == 1 || item.Type == 18) ? "-$" + HelperCommon.NumberFormat(item.Amount * -1) : "+$" + HelperCommon.NumberFormat(item.Amount * -1))
                    StrAmount = (item.Amount >= 0) ? "+$" + HelperCommon.NumberFormat(item.Amount): "-$" + HelperCommon.NumberFormat(item.Amount*-1)
                });
            }

            DataResponse response = new DataResponse
            {
                Reply = new PagingResponseHistoryTransaction
                {
                    Total = total,
                    Item = responsedata
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage ForgotPassword()
        {
            ForgotPasswordModel model = GetRequestBody<ForgotPasswordModel>();
            var data = _action.ForgotPassword(model.Email);
            Alert alert = new Alert();
            alert.Message = data.Message;
            alert.Success = data.Success;
            alert.ClassCss = data.Success ? "success" : "warning";
            DataResponse response = new DataResponse
            {
                Meg = data.Success ? "success" : "fail",
                StatusCode = data.Success? HttpStatusCode.OK: HttpStatusCode.BadRequest,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage UpdatePassword()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            UpdatePasswordModel model = GetRequestBody<UpdatePasswordModel>();
            var data = _action.ChangePassword((int)currentUserId, model.PassOld, model.PassNew, model.PassNewRe);
            Alert alert = new Alert();
            alert.Message = data.Message;
            alert.Success = data.Success;
            alert.ClassCss = data.Success ? "success" : "warning";
            DataResponse response = new DataResponse
            {
                Meg = data.Success ? "success" : "fail",
                StatusCode = data.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage ResetPassword()
        {
            ResetPasswordModel model = GetRequestBody<ResetPasswordModel>();
            var data = _action.ResetPassword(model.PassNew, model.PassNewRe, model.Token);
            Alert alert = new Alert();
            alert.Message = data.Message;
            alert.Success = data.Success;
            alert.ClassCss = data.Success ? "success" : "warning";
            DataResponse response = new DataResponse
            {
                Meg = data.Message,
                StatusCode = data.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage ActiveEmailRegister()
        {
            ActiveEmailRegitserModel model = GetRequestBody<ActiveEmailRegitserModel>();
            var data = _action.MailActive(model.token);
            Alert alert = new Alert();
            alert.Message = data.Message;
            alert.Success = data.Success;
            alert.ClassCss = data.Success ? "success" : "warning";
           
            DataResponse response = new DataResponse
            {
                Meg = data.Success ? "Successfully activate account" : "Token is not found",
                StatusCode = data.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage TransferConfirm()
        {
            ActiveEmailRegitserModel model = GetRequestBody<ActiveEmailRegitserModel>();
            var data = _action.TransferConfirm(model.token);
            Alert alert = new Alert();
            alert.Message = data.Message;
            alert.Success = data.Success;
            alert.ClassCss = data.Success ? "success" : "warning";
            DataResponse response = new DataResponse
            {
                Meg = data.Success ? "success" : "fail",
                StatusCode = HttpStatusCode.OK,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage User_WalletAddress_CopyTrade_GetByUserName()
        {
            Alert meg = new Alert();
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var user = _userService.User_GetByUserId(currentUserId.Value);

            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
            var dataWalletCopytrade = _userService.User_WalletAddress_CopyTrade_GetByUserName(user.Username);
            DataResponse response = new DataResponse
            {
                Reply = dataWalletCopytrade
            };
            response.Meg = "Success!";
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
       
        #region Affiliate
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_Affiliate()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            // get code referrals
            var dataUser = GetCurrentUser();
            var userData = _packagesService.Dasboarch((int)currentUserId);
            userData.StrTotalNetworkTrading = HelperCommon.NumberFormat(_packagesService.Get_Total_Trade((int)currentUserId));
            userData.StrTotalTrade = HelperCommon.NumberFormat(userData.TotalTrade);
            ResponseAffiliate affiliate = new ResponseAffiliate();
            affiliate.TotalF1 = userData.TotalF1;
            affiliate.TotalNetworks = userData.TotalNetwork;
            affiliate.YourTradingInWeek = userData.StrTotalTrade;
            affiliate.TotalNetWorkTradingInWeek = userData.StrTotalNetworkTrading;
            affiliate.ReferralCode = dataUser.Username;

            //end get code

            DataResponse response = new DataResponse
            {
                Meg = "Success",
                Reply = affiliate
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Lst_Trading_Affiliate()
        {
            Alert meg = new Alert();
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            PagingModel model = GetRequestBody<PagingModel>();
            //var user = _userService.User_GetByUserId(currentUserId.Value);

            int total = 0;

            
            var lst = _userService.Account_Referal_List((int)currentUserId,100,
                model.PageIndex,
                model.PageSize,
                out total);
         
          
            DataResponse response = new DataResponse
            {
                Reply = new UsersAffiliatesPagingResponse
                {
                    Total = total,
                    Item = lst
                }
            };
            response.Meg = "Success!";
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        //[HttpPost]
        //[HMACAuthentication]
        //public HttpResponseMessage TOOL_INVEST_PACKAGES()
        //{
        //    Alert meg = new Alert();
        //    int? currentUserId = GetCurrentUId();
        //    if (currentUserId == null)
        //        return Request.CreateResponse(HttpStatusCode.Unauthorized);
        //    PagingModel model = GetRequestBody<PagingModel>();
        //    var user = _userService.User_GetByUserId(currentUserId.Value);

        //    int total = 0;
        //    model.PageSize = 1000;

        //    var lst = _userService.Account_Referal_List((int)currentUserId, 1000,
        //        model.PageIndex,
        //        model.PageSize,
        //        out total);
        //    foreach (var item in lst)
        //    {
        //        var userdata = _userService.User_GetByUserId(item.UserId);
        //        _action.Investment(100, userdata);
        //        System.Threading.Thread.Sleep(400);
        //    }

        //    DataResponse response = new DataResponse
        //    {
        //        Meg= "succcess"
        //    };
        //    response.Meg = "Success!";
        //    return Request.CreateResponse(HttpStatusCode.OK, response);
        //}
        #endregion

        #region Get USD_wallet
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_WalletAddressUSD_ByUser()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            string walletAddressUSD = _packagesService.Get_WalletAddressUSD_ByUser((int)currentUserId);
            DataResponse response = new DataResponse
            {
                Meg = "Success",
                Reply = walletAddressUSD
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
        #region Transfer_USD
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Transfer_USD_GetByWalletAddress()
        {
            Alert alert = new Alert();
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            TransferInfoModel model = GetRequestBody<TransferInfoModel>();
            // check balance
            var data_walletAddressUSD = _packagesService.Get_WalletAddressInfo_ByUser((int)currentUserId);
            if (model.AmountUSD > 0)
            {
                var lockkey = string.Format("transfer_{0}", (int)currentUserId);
                lock (LockHelper.GetLock(lockkey))
                {
                    decimal checkAmountUSD = data_walletAddressUSD.MoneyUSD;
                    if (checkAmountUSD > 0 && checkAmountUSD > model.AmountUSD && model.AmountUSD > 0)
                    {
                        var data = _action.TransferInfo_Validate((int)currentUserId, model.AmountUSD, model.WalletReceived, model.NoteText);

                        alert.Message = data.Message;
                        alert.Success = data.Success;
                        alert.ClassCss = data.Success ? "success" : "warning";
                    }

                }

            }
            else
            {
                alert.Message = "Amount Invalid";
                alert.Success = false;
                alert.ClassCss = "warning";

            }

            DataResponse response = new DataResponse
            {
                Meg = alert.Success ? "success" : "fail",
                StatusCode = HttpStatusCode.OK,
                Reply = alert
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        #region Transfer_HISTORY
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Transfer_History()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            PagingModel model = GetRequestBody<PagingModel>();
            int total = 0;
            string whereClause = string.Format(" and A.UserId = {0}", currentUserId.Value);
            var lst = _userService.Transfer_History(
                model.PageIndex,
                model.PageSize,
                out total,
                whereClause);

            List<TransferHistoryModel> dataList = lst.Select(x => new TransferHistoryModel
            {
                Id = x.Id,
                UserId = x.UserId,
                ByUserName = x.ByUserName,
                Amount =x.Amount,
                StrAmount = _helper.FormatNumber(x.Amount),
                FromUser = x.FromUser,
                Description = x.Description,
                Type = x.Type,
                TypeName=x.TypeName,
                Status =x.Status,
                StatusName = x.StatusName,

                StrCreateOn = x.CreateOn.ToString("yyyy-MM-dd HH:mm:ss")

            }).ToList();

            DataResponse response = new DataResponse
            {
                Meg = "Success",
                Reply = new TransferHistoryPagingResponse
                {
                    Total = total,
                    Item = dataList
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        #region withdraw


        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_Confirm()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            WithdrawConfirmEmailModal model = GetRequestBody<WithdrawConfirmEmailModal>();
            var meg = _action.WithdrawConfirm(model.Token);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_SendCodeEmail()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            Random rd = new Random();
            string code = rd.Next(111111, 999999).ToString();
            HttpContext.Current.Session.Add("CodeVerify_" + currentUserId.ToString(), code);

            var meg = _action.WithdrawSendCode(code,(int)currentUserId);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_Ins()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            WithdrawModal model = GetRequestBody<WithdrawModal>();
            DataResponse response = new DataResponse();
            if (model!=null)
            {
                var meg = _action.Withdraw_Confirm(model.Amount, model.Type, model.Address, currentUserId.Value,model.codeDigit);

                response.Meg = meg.Message;
                response.StatusCode = meg.Success? HttpStatusCode.OK: HttpStatusCode.BadRequest;
                if (!meg.Success)
                {
                    response.StatusCode = HttpStatusCode.BadRequest;
                }
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            response.Meg = "Fail";
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_Request()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            WithdrawModal model = GetRequestBody<WithdrawModal>();
            DataResponse response = new DataResponse
            {
                Meg = "Success",
                Reply = _action.CalculatorAmount(model.Amount, model.Type)
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage Withdraw_History()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            PagingWithdrawModel model = GetRequestBody<PagingWithdrawModel>();
            int total = 0;
            string whereClause = string.Format(" and A.UserId = {0}", currentUserId.Value);
            var lst = _userService.Withdraw_History(
                model.PageIndex,
                model.PageSize,
                out total,
                whereClause);

            List<WithdrawList> dataList = lst.Select(x => new WithdrawList
            {
                Id = x.Id,
                FromTypeName = ((MethodPayment)x.FromType).ToString(),
                ToTypeName = ((MethodPayment)x.ToType).ToString(),
                strAmountSet = _helper.FormatNumber(x.AmountSet),
                Fee = x.Fee,
                strAmountGet = _helper.FormatNumber(x.AmountGet),
                StatusName = ((WithdrawStatus)x.Status).ToString(),
                strCreateDate = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),
                strApproveDate = x.ApproveDate.HasValue ? x.ApproveDate.Value.ToString("yyyy/MM/dd HH:mm:ss") : "",
                HashCode = x.HashCode,
                AddressWallet = x.Transaction
            }).ToList();

            DataResponse response = new DataResponse
            {
                Reply = new WithdrawPagingResponse
                {
                    Total = total,
                    Item = dataList
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        #endregion

        #region Investment
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_PackageInvestments()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            List<InvestmentPackageResponse> investments = new List<InvestmentPackageResponse>();
            int[] arrInvest= { 100};
            for (int i = 0; i < arrInvest.Length; i++)
            {
                InvestmentPackageResponse package = new InvestmentPackageResponse();
                var itemPackage = arrInvest[i];
                switch (itemPackage)
                {
                    //case 100:
                    //    package.PackageName = "PRO";
                    //    package.linkIcon = _helper.GetDomain()+"/images/agency-1.svg";
                    //    package.PackageActivated = "Package activated";
                    //    package.Title = "You can upgrade to higher package before Nov 30th, 2020";
                    //    package.IsActive = false;
                    //    package.Descriptions = "<table><tbody><tr><th>Level</th><th>Agency Com. (%)</th><th>Trading Com. (%)</th></tr><tr><td>F1</td><td>21</td><td>1.30</td></tr><tr><td>F2</td><td>8</td><td>0.80</td></tr><tr><td>F3</td><td>8</td><td>0.50</td></tr><tr><td>F4</td><td>5</td><td>0.30</td></tr><tr><td>F5</td><td>5</td><td>0.20</td></tr><tr><td>F6</td><td>--</td><td>--</td></tr><tr><td>F7</td><td>--</td><td>--</td></tr><tr><td>F8</td><td>--</td><td>--</td></tr><tr><td>F9</td><td>--</td><td>--</td></tr><tr><td>F10</td><td>--</td><td>--</td></tr><tr><td>F11</td><td>--</td><td>--</td></tr><tr><td>F12</td><td>--</td><td>--</td></tr><tr><td>F13</td><td>--</td><td>--</td></tr><tr><td class=color-blue>Total</td><td class=color-blue>47</td><td class=color-blue>3.10</td></tr></tbody></table>";
                    //    break;
                    //case 200:
                    //    package.PackageName = "VIP";
                    //    package.linkIcon = _helper.GetDomain() + "/images/agency-2.svg";
                    //    package.Descriptions = "<table><tbody><tr><th>Level</th><th>Agency Com. (%)</th><th>Trading Com. (%)</th></tr><tr><td>F1</td><td>21</td><td>1.30</td></tr><tr><td>F2</td><td>8</td><td>0.80</td></tr><tr><td>F3</td><td>8</td><td>0.50</td></tr><tr><td>F4</td><td>5</td><td>0.30</td></tr><tr><td>F5</td><td>5</td><td>0.20</td></tr><tr><td>F6</td><td>3</td><td>0.10</td></tr><tr><td>F7</td><td>2</td><td>0.10</td></tr><tr><td>F8</td><td>1</td><td>0.10</td></tr><tr><td>F9</td><td>--</td><td>--</td></tr><tr><td>F10</td><td>--</td><td>--</td></tr><tr><td>F11</td><td>--</td><td>--</td></tr><tr><td>F12</td><td>--</td><td>--</td></tr><tr><td>F13</td><td>--</td><td>--</td></tr><tr><td class=color-blue>Total</td><td class=color-blue>53</td><td class=color-blue>3.40</td></tr></tbody></table>";
                    //    break;
                    case 100:
                        package.PackageName = "VIP";
                        package.linkIcon = _helper.GetDomainStatis() + "/images/agency-3.svg?v=4";
                        package.Descriptions = "<table><tbody><tr><th>Level</th><th>Agency Com. (%)</th><th>Trading Com. (%)</th></tr><tr><td>F1</td><td>50</td><td>1.3</td></tr><tr><td>F2</td><td>5</td><td>0.80</td></tr><tr><td>F3</td><td>5</td><td>0.50</td></tr><tr><td>F4</td><td>5</td><td>0.30</td></tr><tr><td>F5</td><td>2.5</td><td>0.20</td></tr><tr><td>F6</td><td>2.5</td><td>0.10</td></tr><tr><td>F7</td><td>2.5</td><td>0.10</td></tr><tr><td>F8</td><td>2.5</td><td>0.10</td></tr><tr><td>F9</td><td>1</td><td>0.10</td></tr><tr><td>F10</td><td>1</td><td>0.10</td></tr><tr><td>F11</td><td>1</td><td>0.10</td></tr><tr><td>F12</td><td>1</td><td>0.10</td></tr><tr><td>F13</td><td>1</td><td>0.10</td></tr><tr><td class=color-blue>Total</td><td class=color-blue>80</td><td class=color-blue>3.90</td></tr></tbody></table>";
                        break;
                    default:
                        break;
                }
               
                package.Amount = itemPackage;
                package.PackageAmount = "$"+ _helper.FormatNumber(itemPackage);
                
               
                investments.Add(package);
            }
            
             DataResponse response = new DataResponse
            {
                Meg = "Success",
                Reply = investments
             };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage BuyMasterIB()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            InvestmentModel model = GetRequestBody<InvestmentModel>();
            var user = _userService.User_GetByUserId(currentUserId.Value);
            var meg = _action.BuyMasterIB(200, user);
          
            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetDasboard()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.GetDasboard(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetAffiliateStatistic()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.GetAffiliateStatistic(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply
                
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetNetworkStatistic()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.GetNetworkStatistic(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply

            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetLevelNetworkStatistic()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.Network_Count_Menber(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply

            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetProfitStatistic()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.Dasboard_Trading_Sumary(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply

            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AffiliateTradingHistory()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            InvestmentHistoryModel model = GetRequestBody<InvestmentHistoryModel>();
            var meg = _action.AffiliateTradingHistory(model.PageIndex, model.PageSize, currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Reply = new TradingPagingResponse
                {
                    Total = (int)meg.Optional,
                    Item = (List<AffiliateTradingList>)meg.Result
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AffiliateAgencyHistory()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            InvestmentHistoryModel model = GetRequestBody<InvestmentHistoryModel>();
            var meg = _action.AffiliateAgencyHistory(model.PageIndex, model.PageSize, currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Reply = new AgencyPagingResponse
                {
                    Total = (int)meg.Optional,
                    Item = (List<AffiliateTradingList>)meg.Result
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AffiliateChartMembers()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var meg = _action.AffiliateChartMembers(currentUserId.Value);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply,
                
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage AffiliateChartAgencyCom()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            ChartAgencyComModel model = GetRequestBody<ChartAgencyComModel>();
            var meg = _action.AffiliateChartAgencyCom(currentUserId.Value, model.Option);
            DataResponse response = new DataResponse
            {
                Meg = meg.Message,
                Reply = meg.Reply

            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }


        #region User profile
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetProfile()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            var dataUser = _userService.User_GetByUserId(currentUserId.Value);

            ResponseData response = new ResponseData
            {
                Reply = new UserProfile
                {
                    Username = dataUser.Username,
                    FullName = dataUser.FullName,
                    Email = dataUser.Email,
                    Phone = dataUser.Phone,
                    Country = dataUser.Country
                }
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage Get_UserProfile()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            var dataUser = _userService.User_GetByUserId(currentUserId.Value);
            var totalInvest = _userService.Get_Max_Invest_By_Uid((int)currentUserId);
            ResponseUserProfile responseUserProfile = new ResponseUserProfile();
            responseUserProfile.Code = dataUser.Code;
            responseUserProfile.FullName = dataUser.FullName;
            responseUserProfile.Username = dataUser.Username;
            responseUserProfile.Phone = dataUser.Phone;
            responseUserProfile.Email = dataUser.Email;
            responseUserProfile.Country = dataUser.Country;
            responseUserProfile.Password = dataUser.Password;
            responseUserProfile.TotalInvest = totalInvest??0;
            DataResponse response = new DataResponse
            {
                Reply = responseUserProfile,
                Meg ="Success"
            };
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

        #region User profile
        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage UpdateProfile()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            UserProfileModel model = GetRequestBody<UserProfileModel>();
            Alert meg = _action.User_UpdateProfile((int)currentUserId, model.FullName, model.Phone);

            DataResponse response = new DataResponse
            {
                Meg = meg.Message
            };
            if (!meg.Success)
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion
        #region FACode
        [HttpGet]
        [HMACAuthentication]
        public HttpResponseMessage GetFaCode()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);
            var currentUser = _userService.User_GetByUserId((int)currentUserId);
            var Reply = _action.GetFACode(currentUser);
            if (!string.IsNullOrEmpty(currentUser.FA2Code))
            {
                Reply.IsEnable = true;
            }

            DataResponse response = new DataResponse
            {
                Reply = Reply
            };

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        [HttpPost]
        [HMACAuthentication]
        public HttpResponseMessage SetupFaCode()
        {
            int? currentUserId = GetCurrentUId();
            if (currentUserId == null)
                return Request.CreateResponse(HttpStatusCode.Unauthorized);

            FACodeModel model = GetRequestBody<FACodeModel>();

            int rel = _action.SettingFaCode(currentUserId.Value, model.UserUniqueKey, model.SetupCode, model.CodeDigit);
            DataResponse response = new DataResponse
            {
                Reply = model,
            };

            if (rel > 0)
            {
                response.StatusCode = HttpStatusCode.OK;
                response.Meg = "2FA has been enabled successfully";
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
                response.Meg = "Setting Fail";
            }

            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
        #endregion

    }
}
