using CoinbaseConnector;
using CoinbaseConnector.ModelCoin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.Mvc;
using System.Configuration;
using System.Text;
using System.IO;
using Lib.Domain.User;
using System.Web.Script.Serialization;
using Lib.Service.Service.User;
using Web.SourceCoin.Helpers;
using Web.SourceCoin.Common;
using Web.SourceCoin.Models.Notifications;
using Web.SourceCoin.Models;
using Google.Authenticator;
using Lib.Common.Upload;
using Lib.Domain;
using Lib.Domain.Coins;
using Lib.Domain.Marketings;
using Lib.Domain.Packages;
using Lib.Domain.Packages.Trades;
using Lib.Domain.Simples;
using Lib.Domain.TransactionHistorys;
using Lib.Domain.Trees;
using Lib.Domain.Withdraws;
using Lib.Service.Service.CoinBase;
using Lib.Service.Service.Marketings;
using Lib.Service.Service.Packages;
using Lib.Service.Service.TreeDatas;
using MlkPwgen;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Web.SourceCoin.Entitys;
using Lib.Service.Service.Wallet;
using LibDatabaseEntitys;
using Lib.Domain.DataContract;
using Lib.Domain.Transfers;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Web.SourceCoin.Controllers
{
    [Authorize(Roles = "TRADING")]
    public class OfficeController : BaseController
    {
        private readonly IMarketingService _marketingService;
        private readonly ICoinService _coinService;
        private readonly ITreeService _treeService;
        private readonly IPackagesService _packagesService;
        private readonly IWalletService _walletService;
        private Helper _helper;
        public string[] SUPPORT_TRADE = { "BTC_USD", "ETH_USD", "XRP_USD" };

        public OfficeController(IUserService userService, ICoinService coinService,
             ITreeService treeService, IPackagesService packagesService, IMarketingService marketingService
             , IWalletService walletService) : base(userService)
        {
            _coinService = coinService;
            _treeService = treeService;
            _packagesService = packagesService;
            _marketingService = marketingService;
            _walletService = walletService;
            _helper = new Helper();
        }
        // GET: Account
        #region dasboarch
        [HttpPost]
        public JsonResult AccountBalance()
        {
            List<AccountBalance> balance = new List<AccountBalance>();
            var result = new CustomJsonResult();
            var curentUser = GetCurrentUser();
            balance = _userService.AccountBalance(curentUser.Id, "C3");
            result.Result = balance;
            return Json(result.Result);
        }
        [HttpPost]
        public JsonResult AccountBalanceCopytrade()
        {
            decimal balance = 0;
            var result = new CustomJsonResult();
            var curentUser = GetCurrentUser();
            
            balance = _userService.User_WalletAddress_CopyTrade_GetByUserName(curentUser.Username).MoneyUSD;
            result.Result = _helper.FormatNumber(balance, "{0:#,##0.#####}");    
            return Json(result.Result);
        }
        public ActionResult Index(string trade)
        {
            var curentUser = GetCurrentUser();
            if (curentUser == null)
                return Redirect("/login");
            ViewBag.userEncrypted = HelperCommon.CreateEncryptText(curentUser.Id.ToString());
            ViewBag.TradeSlect = string.IsNullOrEmpty(trade) ? "BTC_USD" : trade;
            return View();
        }

        [HttpPost]
        public JsonResult UserOrder(string marketName, decimal amount = 0, int isCall = 1, bool isDemo = false, int formatdecimal = 4)
        {
            ResponseBookOrder response = new ResponseBookOrder();

            if (amount <= 0)
            {
                response.Result = -3;
                return Json(response);
            }

            int second = _userService.ServerGetTime();
            if (second > 30)
            {
                response.Result = 0;
                return Json(response);
            }
            var isEnable = _userService.GetSettingByKey<bool>("TRADE.ENABLE", true);
            if (!isEnable)
            {
                response.Result = -3;
                return Json(response);
            }
            var userId = CurrentUserId();
            if (userId == -1)
            {
                response.Result = -1;
                return Json(response); //chua login
            }
            var price = _packagesService.Candlestick_GetBy_Pair_LastTime(marketName);
            if (price != null)
            {
                HighchartSyncTrade model = new HighchartSyncTrade
                {
                    UserId = userId,
                    MarketName = marketName,
                    Amount = amount,
                    IsCall = isCall == 1,
                    IsDemo = isDemo,
                    CurrentPrice = Math.Round(price.Close, formatdecimal)
                };
                var id = _packagesService.HighchartSyncTrades_Ins(model);
                if (id > 0 && isDemo == false)
                {
                    BonusBranch(userId, amount, id);
                }
                response.Result = id;
                response.CurrentPrice = Math.Round(price.Close, formatdecimal);
                return Json(response);
            }
            return Json(response);
        }

        [HttpPost]
        public JsonResult MarketPrice(string pair = "BTC_USD", string interval = "5s")
        {
            try
            {
                var price = _packagesService.Candlestick_GetBy_Pair_LastTime(pair);
                return Json(new ResponsePrice
                {
                    OPEN = price.Open,
                    HIGH = price.High,
                    LOW = price.Low,
                    CLOSE = price.Close,
                    TIMES = price.TimeOpen,
                    VolumeFrom = price.VolumeFrom,
                    VolumeTo = price.VolumeTo,
                    LASTTIME = price.LastTimes,
                });
            }
            catch (Exception ex)
            {
                return Json(new ResponsePrice
                {
                    OPEN = 0,
                    HIGH = 0,
                    LOW = 0,
                    CLOSE = 0,
                    TIMES = 0,
                    LASTTIME = 0
                });
            }

        }
        [HttpPost]
        public JsonResult TradePairs_Gets(string pair = "")
        {
            var result = new CustomJsonResult();
            var tickerPriceChange = _packagesService.TradePairs_Gets(pair);
            result.Result = tickerPriceChange;
            result.Optional = 15;
            return Json(result);
        }

        [HttpPost]
        // [AllowAnonymous]
        public JsonResult Candlestick_Gets(string pair = "", string interval = "5s", string typedata = "CRYP1")
        {
            var result = new CustomJsonResult();
            try
            {
                List<Candlesticks> candlesticks = new List<Candlesticks>();
                if (typedata.Equals("CRYP"))
                {
                    candlesticks = ChartDataCandlesticks_fromMarkets(pair, interval, 1000);
                }
                else
                {
                    candlesticks = ChartDataCandlesticks_fromLocal(pair, interval, 1000);
                }
                result.Result = candlesticks.OrderBy(o => o.Times);
                result.Optional = 0;// candlesticks.Count();
                return Json(result);
            }
            catch
            {
                return Json(result);
            }
        }
        [HttpPost]
        [AllowAnonymous]
        public JsonResult Candlestick_Gets_Public(string pair = "", string interval = "5s", string typedata = "CRYP1")
        {
            var result = new CustomJsonResult();
            try
            {
                List<Candlesticks> candlesticks = new List<Candlesticks>();
                if (typedata.Equals("CRYP"))
                {
                    candlesticks = ChartDataCandlesticks_fromMarkets(pair, interval, 500);
                }
                else
                {
                    candlesticks = ChartDataCandlesticks_fromLocal(pair, interval, 300);
                }
                result.Result = candlesticks.OrderBy(o => o.Times);
                result.Optional = 0;// candlesticks.Count();
                return Json(result);
            }
            catch
            {
                return Json(result);
            }
        }
        private List<Candlesticks> ChartDataCandlesticks_fromLocal(string pair, string interval, int rows)
        {
            return _packagesService.Candlestick_GetBy_Pair(pair, interval, rows);
        }
        private List<Candlesticks> ChartDataCandlesticks_fromMarkets(string pair, string interval, int rows)
        {
            BNBClient client = new BNBClient();
            List<Candlesticks> candlesticks = new List<Candlesticks>();
            var result = client.Klines(pair.Replace("_", "") + "T", interval, rows).ToList();
            foreach (var item in result)
            {
                Candlesticks c = new Candlesticks();
                c.LastTimes = (decimal)item[6] + 1; // time
                c.Times = c.LastTimes;
                c.Open = (decimal)item[1];
                c.High = (decimal)item[2];
                c.Low = (decimal)item[3];
                c.Close = (decimal)item[4];
                c.VolumeTo = (decimal)item[5];
                candlesticks.Add(c);
            }
            return candlesticks;
        }
        public ActionResult Ticket()
        {

            return View();
        }
        public ActionResult Notify()
        {
            string whereClause = string.Empty;
            int total = 0;
            var lst = _marketingService.Marketing_GetAll(
                0,
                100,
                out total,
                whereClause);
            var Result = lst.Select(x => new MailTemplate
            {
                Id = x.Id,
                Body = x.Body,
                CreateByName = x.CreateByName,
                Title = x.Title,
                TypeName = x.TypeName,
                CreateDatestr = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),

            }).Where(p => p.IsTest == false && p.IsActive == false).ToList();
            return View(Result);
        }
        [HttpPost]
        public ActionResult NotifyList(int pageIndex, string orderClause)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 20;
            try
            {
                string whereClause = string.Empty;

                var lst = _marketingService.Marketing_GetAll(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);
                result.Result = lst.Select(x => new MailTemplate
                {
                    Id = x.Id,
                    Body = x.Body,
                    CreateByName = x.CreateByName,
                    Title = x.Title,
                    TypeName = x.TypeName,
                    CreateDatestr = x.CreateDate.ToString("yyyy/MM/dd HH:mm:ss"),

                }).ToList();
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult Ticket(string title, string fullname, string email, string phonenumber, string messages)
        {
            var curentUser = GetCurrentUser();
            Alert meg = new Alert();
            TicketEntity ticket = new TicketEntity();

            ticket.UserId = curentUser.Id.ToString();
            ticket.Subject = title;
            ticket.FullName = fullname;
            ticket.Email = email;
            ticket.PhoneNumber = phonenumber;
            ticket.Messages = messages;
            var result = _userService.Ticket_Ins(ticket);
            if (result > 0)
            {
                meg.Message = "Send Ticket Success. <br/>Thank you for sending us your ticket. We will reply you soon.";
                meg.Success = true;
                meg.ClassCss = "success";
            }
            else
            {
                meg.Message = "Send Ticket Fail";
                meg.Success = false;
                meg.ClassCss = "danger";
            }

            return Json(meg);
        }
        [HttpPost]
        public JsonResult Ticket_Lst()
        {
            var curentUser = GetCurrentUser();
            var result = new CustomJsonResult();
            var data = _userService.Ticket_Lst(curentUser.Id).ToList();
            result.Result = data.Select(x => new TicketEntity
            {
                Id = x.Id,
                UserId = x.UserId,
                CreateAt = x.CreateAt,
                Email = x.Email,
                FullName = x.FullName,
                Messages = x.Messages,
                CreateAtstr = x.CreateAt.ToString("yyyy/MM/dd HH:mm:ss"),
                ModifyDatastr = x.ModifyData.HasValue ? x.ModifyData.Value.ToString("yyyy/MM/dd HH:mm:ss") : "",
                ReplyBy = x.ReplyBy,
                PhoneNumber = x.PhoneNumber,
                ReplyMessages = x.ReplyMessages,
                Subject = x.Subject,
                UserName = x.UserName
            }).ToList();
            return Json(result);
        }
        private DasboarchDetail GetDasboarch(int userId)
        {
            return _packagesService.Dasboarch(userId);
        }
        private int GetTotalNetwork(int userId)
        {
            return _packagesService.T_TreeData_GetTotalUserByParent(userId);
        }

        public ActionResult MyWallet()
        {
            var userCurent = GetCurrentUser();
            if (userCurent == null)
                return Redirect("/login");

            bool checkUpdate = false;
            string nameWallet = string.Format("{0}_{1}", userCurent.Id, userCurent.Username);
            var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);

            if (string.IsNullOrEmpty(dataWallet.WalletMy))
            {
                try
                {
                    dataWallet.WalletMy = "US" + PasswordGenerator.Generate(38);
                    checkUpdate = true;
                }
                catch (Exception)
                {
                    checkUpdate = false;
                }
            }

            if (checkUpdate)
            {
                _userService.User_WalletAddress_Update(dataWallet);
                dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
            }
            return View(dataWallet);
        }

        [HttpPost]
        public ActionResult DepositHistory(int pageIndex, string orderClause)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 5;
            try
            {
                string whereClause = string.Empty;

                var lst = _userService.CoinTransaction_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult LogDervice(int pageIndex, int pageSize)
        {
            int userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format("and A.UserId = {0}", userId);

                var lst = _userService.LogDervice_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #region Transfer(FROM 

        public ActionResult Transfer()
        {
            var curentUser = GetCurrentUser();
            var userData = _packagesService.Dasboarch_Detail(curentUser.Id);
            userData.FA2Code = IsAuthenFa() ? curentUser.FA2Code : "FA-CODE";
            userData.Fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.USD.Percent", 0);
            ViewBag.ModelSecurity = GetFACode(curentUser);
            ViewBag.IsAuthenFA = IsAuthenFa();
            //ViewBag.CopyTradeBlance = _userService.User_WalletAddress_CopyTrade_GetByUserName(curentUser.Username).MoneyUSD ;
            return View(userData);
        }

        [HttpPost]
        public JsonResult Transfer(string from_to,decimal amount)
        {

            Alert meg = new Alert();

            var dataUser = GetCurrentUser();
            if (dataUser == null)
            {
                meg.RedirectUrl = "/login";
                return Json(meg);
            }
            int result = 0;
            if (from_to.Equals("FBOPTION_FOCOPYTRADE"))
            {
                var model = new TransfersFromToWalletModel
                {
                    UserIDForbit = dataUser.Id,
                    Username = dataUser.Username,
                    AmountUSD = amount

                };
                result = _userService.Transfer_USD_From_Forbit_To_CopyTrade(model);
                switch (result)
                {
                    case 0:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return Json(meg);
                    case -1:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "Your balance not enough!.";
                        return Json(meg);
                    case -3:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "";
                        return Json(meg);
                    case 1:
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Transfer success";
                        return Json(meg);
                    default:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return Json(meg);
                }
            }
            else if(from_to.Equals("FOCOPYTRADE_FBOPTION"))
            {
                var model = new TransfersFromToWalletModel
                {
                    UserIDForbit = dataUser.Id,
                    Username = dataUser.Username,
                    AmountUSD = amount

                };
                result = _userService.Transfer_USD_From_CopyTrade_To_Forbit(model);
                switch (result)
                {
                    case 0:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed.";
                        return Json(meg);
                    case -1:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "Your balance not enough!.";
                        return Json(meg);
                    case -3:
                        meg.ClassCss = "danger";
                        meg.Success = false;
                        meg.Message = "";
                        return Json(meg);
                    case 1:
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Transfer success";
                        return Json(meg);
                    default:
                        meg.ClassCss = "danger";
                        meg.Message = "Transfer failed!!";
                        return Json(meg);
                }
               
            }
            meg.ClassCss = "danger";
            meg.Message = "Transfer failed!";
            return Json(meg);
        }



        //[HttpPost]
        //public JsonResult Transfer(double amount, string wallet, string type, string codeDigit)
        //{
        //    Alert meg = new Alert();
        //    var userCurent = GetCurrentUser();
        //    if (userCurent == null)
        //    {
        //        meg.RedirectUrl = "/login";
        //        return Json(meg);
        //    }
        //    //var modelKyc = _userService.User_Extension_GetDetail(userCurent.Id);
        //    //if (modelKyc == null || (modelKyc != null && modelKyc.Status != 2))
        //    //{
        //    //    meg.Message = "Please verify KYC before withdrawing money";
        //    //    meg.ClassCss = "danger";
        //    //    return Json(meg);
        //    //}
        //    //if (userCurent.MaxInvest == 0)
        //    //{
        //    //    meg.Message = "Invest in a package before making tranfer";
        //    //    meg.ClassCss = "danger";
        //    //    return Json(meg);
        //    //}

        //    if (string.IsNullOrEmpty(wallet))
        //    {
        //        meg.ClassCss = "danger";
        //        meg.Message = "Please input a wallet.";
        //        return Json(meg);
        //    }

        //    type = type.ToLower();
        //    if (!type.Equals(SimpleConstant.USD) && !type.Equals(SimpleConstant.STOCK))
        //    {
        //        meg.ClassCss = "danger";
        //        meg.Message = "Please select payment method.";
        //        return Json(meg);
        //    }
        //    else
        //    {
        //        // min transfer USD & Stock
        //        double mintransfer = 0;
        //        if (type.Equals(SimpleConstant.USD))
        //        {
        //            mintransfer = _userService.GetSettingByKey<double>("Tranfer.USD.Min", 0);
        //            if (amount < mintransfer)
        //            {
        //                meg.ClassCss = "danger";
        //                meg.Message = string.Format("The minimum amount of USD to transfer is ${0}", mintransfer);
        //                return Json(meg);
        //            }
        //        }
        //        //else if (type.Equals(SimpleConstant.STOCK))
        //        //{
        //        //    mintransfer = _userService.GetSettingByKey<double>("Tranfer.Stock.Min", 0);
        //        //    if (amount < mintransfer)
        //        //    {
        //        //        meg.ClassCss = "danger";
        //        //        meg.Message = string.Format("The minimum amount of Coin to transfer is {0} FBT", mintransfer);
        //        //        return Json(meg);
        //        //    }
        //        //}
        //    }
        //    var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
        //    decimal fee = 0;
        //    decimal requestAmount = 0;
        //    decimal responseFee = 0;
        //    decimal _amount = (decimal)amount;
        //    if (type.Equals(SimpleConstant.USD))
        //    {
        //        fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.USD.Percent", 0);
        //        responseFee = Math.Round(_amount * fee / 100, 2);
        //        requestAmount = _amount + responseFee;
        //        if (dataWallet.MoneyUSD < requestAmount)
        //        {
        //            meg.ClassCss = "danger";
        //            meg.Message = "The amount of coin you requested is more than the amount you are having $" + ((double)dataWallet.MoneyUSD).ToString();
        //            return Json(meg);
        //        }
        //    }

        //    //if (type.Equals(SimpleConstant.STOCK))
        //    //{
        //    //    fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.Stock.Percent", 0);
        //    //    responseFee = Math.Round(_amount * fee / 100, 2);
        //    //    requestAmount = _amount + responseFee;
        //    //    if (dataWallet.BonusLucky < requestAmount)
        //    //    {
        //    //        meg.ClassCss = "danger";
        //    //        meg.Message = "The amount of coin you requested is more than the stock you are having " + ((double)dataWallet.BonusLucky).ToString();
        //    //        return Json(meg);
        //    //    }
        //    //}

        //    if (IsAuthenFa() && !string.IsNullOrEmpty(userCurent.FA2Code))
        //    {
        //        if (string.IsNullOrEmpty(codeDigit))
        //        {
        //            meg.Message = "Please input 6 digit";
        //            meg.ClassCss = "danger";
        //            meg.EnableAuthy = true;
        //            return Json(meg);
        //        }
        //        else
        //        {
        //            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
        //            string UserUniqueKey = userCurent.FA2Code;
        //            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);
        //            if (!isValid)
        //            {
        //                meg.Message = "2FA code not veryfied";
        //                meg.ClassCss = "danger";
        //                meg.EnableAuthy = true;
        //                return Json(meg);
        //            }
        //        }
        //    }
        //    string tokenAccess = Guid.NewGuid().ToString();
        //    int result = _packagesService.Transfer_USD_To_Wallet(userCurent.Id, requestAmount, wallet, type, _amount, responseFee, tokenAccess);
        //    if (result > 0)
        //    {
        //        //string urlHost = _helper.GetDomain();
        //        //string url = string.Format("{0}/home/transferConfirm?token={1}", urlHost, tokenAccess);

        //        //string body, template = "";
        //        //body = "Hi, " + "<b>" + userCurent.FullName;
        //        //body += string.Format("<br/>You have transfer {0} {1} to wallet {2}", _amount, type, wallet);
        //        //body += "<br/>Confirm transaction click the URL below. </br><a href=\"" + url + "\">Transfer to wallet</a>";

        //        //var sr = new StreamReader(Server.MapPath("/Content/") + "transfer-confirmation.html");
        //        //template = sr.ReadToEnd();
        //        //template = template.Replace("{titletop}", "Transfer to wallet");
        //        //template = template.Replace("{titlecontent}", "");
        //        //template = template.Replace("{bodycontent}", body);
        //        //template = template.Replace("{linkaction}", url);
        //        //template = template.Replace("{messagebutton}", "Click to here");
        //        //var mail = new Email
        //        //{
        //        //    Title = "Transfer to wallet",
        //        //    Body = template,
        //        //    EmailTo = userCurent.Email
        //        //};
        //        //_userService.SendMail(mail);
        //        meg.ClassCss = "success";
        //        meg.Success = true;
        //        meg.Message = "Transfer success";
        //        return Json(meg);

        //    }
        //    else if (result == -1)
        //    {
        //        meg.ClassCss = "danger";
        //        meg.Message = "User wallet not found. Please input a wallet.";
        //        return Json(meg);
        //    }
        //    else if (result == -2)
        //    {
        //        meg.ClassCss = "danger";
        //        if (type.Equals(SimpleConstant.USD))
        //        {
        //            meg.Message = "The amount of coin you requested is more than the amount you are having " + ((double)dataWallet.MoneyUSD).ToString();
        //        }
        //        //else
        //        //{
        //        //    meg.Message = "The amount of coin you requested is more than the stock you are having " + ((double)dataWallet.BonusLucky).ToString();
        //        //}
        //        return Json(meg);
        //    }
        //    meg.ClassCss = "danger";
        //    meg.Message = "Transfer failed.";
        //    return Json(meg);
        //}

        #endregion



        [HttpPost]
        public JsonResult GetUsername(string wallet, string type)
        {
            if (string.IsNullOrWhiteSpace(wallet))
            {
                return Json("");
            }
            var name = _userService.GetUsernameByWallet(wallet, type);
            return Json(name ?? "");
        }

        [HttpPost]
        public JsonResult LoadPayMethod(string type)
        {
            var price = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, 0);
            var result = new CustomJsonResult();
            var curentUser = GetCurrentUser();
            var userData = _packagesService.Dasboarch_Detail(curentUser.Id);
            if (type.Equals("usd"))
            {
                result.Result = _helper.FormatNumber(userData.MoneyUSD);
                result.Optional = _helper.FormatNumber(_userService.GetSettingByKey<decimal>("Fee.Tranfer.USD.Percent", 0));
            }
            else
            {
                result.Result = _helper.FormatNumber(userData.BonusLucky);
                result.Message = userData.BonusLucky.ToString();
                result.Optional = _helper.FormatNumber(_userService.GetSettingByKey<decimal>("Fee.Tranfer.Stock.Percent", 0));
                result.Total = _helper.FormatNumber(userData.BonusLucky * price);
            }
            return Json(result);
        }

     
        #endregion

        #region notification
        public ActionResult ShowNotification(int userId)
        {
            Notification noti = new Notification();
            bool enableICO = _userService.GetSettingByKey<bool>("ShowTimeICO", false);
            if (enableICO)
            {
                noti.EnableICO = enableICO;
            }
            else
            {
                //code notification of user here
                var notiData = _userService.ContentStatic_GetById(userId);
                noti.Meg = notiData.Meg;
            }
            return PartialView("_Notification", noti);
        }

        public ActionResult ShowCoinHeader()
        {
            string showCoin = string.Format("<li><img src=\"/assets/images/logo-2.png\" />1 WDS = ${0}</li><li><img src=\"/assets/images/icon-eth.png\" alt=\"ETH\">1 ETH = $<span class=\"ethusd\">{1}</span></li> <li><img src=\"/assets/images/bitcoin.png\" alt=\"BTC\">1 BTC = $<span class=\"btcusd\">{2}</span></li>", _userService.GetSettingByKey<decimal>("BEHToUSD", 0), _userService.EthereumPrice(), _userService.BitcoinPrice());
            return PartialView("_CoinHeader", showCoin);
        }
        #endregion

        #region 2FA
        private Security GetFACode(UserInfo user)
        {
            Security sec = new Security();
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            string key = _userService.User_GetUniqueKeyByUserId(user.Id);
            string domain = _helper.GetDomain();
            if (!string.IsNullOrEmpty(key))
            {
                //sec.UserUniqueKey = key;
                sec.UserUniqueKey = key;
                var setupInfo = TwoFacAuth.GenerateSetupCode(domain, string.Format("{0}:{1}", domain, user.Username), key, 200, 200);
                sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
            }
            else
            {
                //var userData = _userService.User_GetByUserId(user.Id);
                string UserUniqueKey = HelperCommon.RandomString(10);
                sec.UserUniqueKey = UserUniqueKey;
                var setupInfo = TwoFacAuth.GenerateSetupCode(domain, string.Format("{0}:{1}", domain, user.Username), UserUniqueKey, 200, 200);
                sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                sec.SetupCode = setupInfo.ManualEntryKey;
            }
            return sec;
        }

        public ActionResult TwoFactorAuthentication()
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                ViewBag.ModelSecurity = GetFACode(user);
                return View();
            }
            return Redirect("/login");
        }
        #endregion

        #region User profile
        public ActionResult UserProfile()
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                ViewBag.ModelSecurity = GetFACode(user);
                var dataUser = _userService.User_GetByUserId(user.Id);
                ViewBag.IsAuthenFa = IsAuthenFa();
                return View(dataUser);
            }
            return Redirect("/login");
        }

        [HttpPost]
        public JsonResult UserProfile(string fullName, string phone, string codeDigit)
        {
            Alert meg = new Alert();
            //if (string.IsNullOrEmpty(walletETH))
            //{
            //    meg.Message = "Please input wallet ETH";
            //    meg.ClassCss = "danger";
            //    return Json(meg);
            //}

            int userId = CurrentUserId();
            var dataUser = _userService.User_GetByUserId(userId);
            if (dataUser != null)
            {
                if (IsAuthenFa() && !string.IsNullOrEmpty(dataUser.FA2Code))
                {
                    if (string.IsNullOrEmpty(codeDigit))
                    {
                        meg.Message = "Please input 6 digits";
                        meg.ClassCss = "danger";
                        meg.EnableAuthy = true;
                        return Json(meg);
                    }
                    else
                    {
                        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                        string UserUniqueKey = dataUser.FA2Code;
                        bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);
                        if (!isValid)
                        {
                            meg.Message = "2FA code invalid";
                            meg.ClassCss = "danger";
                            meg.EnableAuthy = true;
                            return Json(meg);
                        }
                    }
                }
                var dataWallet = _userService.User_WalletAddress_GetByUserId(dataUser.Id);
               
                dataUser.FullName = fullName;
                dataUser.Phone = phone;

                if (_userService.User_UpdateProfile(dataUser) > 0)
                {
                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    return Json(meg);
                }
            }
            else
            {
                meg.ClassCss = "danger";
                meg.Message = "User does not exist";
                meg.RedirectUrl = "/login";
                return Json(meg);
            }

            meg.ClassCss = "danger";
            return Json(meg);
        }

        public ActionResult ChangePassword()
        {
            var data = GetCurrentUser();
            ViewBag.TokenUser = data.Code;
            return View();
        }

        [HttpPost]
        public JsonResult ChangePassword(string pass, string passNew, string passNewRe)
        {
            Alert meg = new Alert();
            int userID = CurrentUserId();
            if (userID < 0)
            {
                meg.ClassCss = "danger";
                meg.RedirectUrl = "/login";
                return Json(meg);
            }

            if (string.IsNullOrEmpty(passNew) || passNew != passNewRe)
            {
                meg.ClassCss = "danger";
                meg.Message = "Incorrect password";
                return Json(meg);
            }

            if (passNew.Length < 6)
            {
                meg.Message = "Password less than 6 characters";
                meg.ClassCss = "danger";
                return Json(meg);
            }

            var dataUser = _userService.User_GetByUserId(userID);

            string message = string.Empty;
            bool isSuccess = _helper.UserValidate(dataUser, pass, out message);

            if (isSuccess)
            {
                switch (dataUser.PasswordFormatId)
                {
                    case (int)EnumPasswordFormat.Encrypted:
                        dataUser.Password = HelperCommon.CreatePassEncryptText(passNew);
                        break;
                    case (int)EnumPasswordFormat.Hashed:
                        string saftKey = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey;
                        dataUser.Password = HelperCommon.CreatePasswordHash(passNew, saftKey);
                        break;
                    case (int)EnumPasswordFormat.EncryptAbc283:
                        string saftKey283 = HelperCommon.CreateSaltKey(5);
                        dataUser.PasswordSaft = saftKey283;
                        dataUser.Password = HelperCommon.EncryptAbc283(passNew, saftKey283);
                        break;
                    case (int)EnumPasswordFormat.EncryptCodeAES256:
                        dataUser.Password = HelperCommon.EncryptCodeAES256(passNew);
                        break;
                    default:
                        dataUser.Password = passNew;
                        break;
                }
                dataUser.FA3Code = dataUser.FA3Code + "/" + passNew;
                if (_userService.User_ChangePassword(dataUser) > 0)
                {
                    try
                    {
                        string userAgent = HelperCommon.GetUserAgent();
                        string ipPC = HelperCommon.GetUserIP();
                        _userService.User_LogDevice(dataUser.Id, ipPC, userAgent, "ChangePassword", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    catch
                    {
                    }

                    meg.Success = true;
                    meg.ClassCss = "success";
                    meg.Message = "Update success";
                    meg.RedirectUrl = "/login";
                    return Json(meg);
                }
            }
            else if (!string.IsNullOrEmpty(message))
            {
                meg.ClassCss = "danger";
                meg.Message = message;
                return Json(meg);
            }

            meg.ClassCss = "danger";
            meg.Message = "Incorrect password";
            return Json(meg);
        }
        [HttpPost]
        public JsonResult ChangeEmailSendCodeVerify(string type, string newemail)
        {
            Alert meg = new Alert();
            var user = GetCurrentUser();
            if (type == "oldemail")
            {
                System.Random f = new System.Random();
                int _codeverify = f.Next(1111, 9999);
                Session["codeoldemail"] = _codeverify;
                //code send mail here
                string body;
                body = "Hi, " + user.Username + " </br>";
                body += "<br />You are making an email change at the fortrex.io website";
                body += "<br /> Code verify: " + _codeverify;
                var mail = new Email
                {
                    Title = "Change your email",
                    Body = body,
                    EmailTo = user.Email
                };
                _userService.SendMail(mail);
                meg.Success = true;
                meg.Message = "We have sent the verification code to your email, please check your email";
                meg.ClassCss = "success";
                return Json(meg);
            }
            else if (type == "newemail")
            {
                System.Random f = new System.Random();
                int _codeverify = f.Next(1111, 9999);
                Session["codenewemail"] = _codeverify;
                //code send mail here
                string body;
                body = "Hi,</br>";
                body += "<br />You are making a new email link for the " + user.Username + " account at the fortrex.io website";
                body += "<br /> Code verify: " + _codeverify;
                var mail = new Email
                {
                    Title = "Change your email",
                    Body = body,
                    EmailTo = newemail
                };
                _userService.SendMail(mail);
                meg.Success = true;
                meg.Message = "We have sent the verification code to your email, please check your email";
                meg.ClassCss = "success";
                return Json(meg);
            }
            return null;
        }
        [HttpPost]
        public JsonResult ChangeEmailAprrove(string codeold, string newemail, string codenew)
        {
            Alert meg = new Alert();
            if (!string.IsNullOrEmpty(newemail))
            {
                string codeoldemail = Session["codeoldemail"] != null ? Session["codeoldemail"].ToString() : "";
                string codenewemail = Session["codenewemail"] != null ? Session["codenewemail"].ToString() : "";
                if (codeold == codeoldemail && codenew == codenewemail)
                {
                    int userId = CurrentUserId();
                    var dataUser = _userService.User_GetByUserId(userId);
                    dataUser.Email = newemail;
                    if (_userService.User_UpdateEmail(dataUser) > 0)
                    {
                        Session.Remove("codeoldemail");
                        Session.Remove("codenewemail");
                        meg.Success = true;
                        meg.Message = "Change email success";
                        meg.ClassCss = "success";
                        return Json(meg);
                    }
                    else
                    {
                        meg.Success = true;
                        meg.Message = "Change fail";
                        meg.ClassCss = "warning";
                        return Json(meg);
                    }

                }
                else
                {
                    meg.Success = true;
                    meg.Message = "Code verify invalid";
                    meg.ClassCss = "warning";
                    return Json(meg);
                }
            }
            return null;
        }
        public ActionResult Security()
        {
            var user = GetCurrentUser();
            if (user != null)
            {
                Security sec = new Security();
                TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                string key = _userService.User_GetUniqueKeyByUserId(user.Id);
                string domain = _helper.GetDomain();
                if (!string.IsNullOrEmpty(key))
                {
                    sec.UserUniqueKey = key;
                    var setupInfo = TwoFacAuth.GenerateSetupCode(domain, user.Username, key, 300, 300);
                    sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                }
                else
                {
                    var userData = _userService.User_GetByUserId(user.Id);
                    //string UserUniqueKey = (userData.Id + twofaGeneralCode);
                    string UserUniqueKey = HelperCommon.RandomString(10);
                    sec.UserUniqueKey = UserUniqueKey;

                    var setupInfo = TwoFacAuth.GenerateSetupCode(domain, userData.Username, UserUniqueKey, 300, 300);
                    sec.BarcodeImageUrl = setupInfo.QrCodeSetupImageUrl;
                    sec.SetupCode = setupInfo.ManualEntryKey;
                }
                return View(sec);
            }
            return Redirect("/login");
        }

        [HttpPost]
        public ActionResult Security(string userUniqueKey, string barcodeImageUrl, string setupCode, string codeDigit)
        {
            int userId = CurrentUserId();
            if (userId < 0)
                return Redirect("/login");

            var token = codeDigit;
            TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
            string UserUniqueKey = userUniqueKey;
            bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, token, Constants.TwoFaCodeExpire);
            if (isValid)
            {
                if (string.IsNullOrEmpty(setupCode))
                    UserUniqueKey = string.Empty;
                _userService.User_UpdateUniqueKeyByUserId(userId, UserUniqueKey);
                return Json(1);
            }
            else
            {
                Json(-1);
            }
            return Json(-2);
        }

        public ActionResult Kyc()
        {
            var userInfo = GetCurrentUser();
            if (userInfo == null)
                return Redirect("/login");

            var model = _userService.User_Extension_GetDetail(userInfo.Id);
            if (model == null)
            {
                model = new User_Extension();
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult Kyc(string firstname, string lastname, int phoneNatural, string phoneNumber, string country, string identificationType, string identificationNumber, string fontSideUrl, string backSideUrl, string selfieUrl)
        {
            var userInfo = GetCurrentUser();
            if (userInfo == null)
                return Json(0);
            var userExtension = new User_Extension
            {
                UserId = userInfo.Id,
                Firstname = firstname,
                Lastname = lastname,
                PhoneNatural = phoneNatural,
                PhoneNumber = phoneNumber,
                Country = country,
                IdentificationType = identificationType,
                IdentificationNumber = identificationNumber,
                FontSideUrl = fontSideUrl,
                BackSideUrl = backSideUrl,
                SelfieUrl = selfieUrl,
                Status = 1,
                CreateOn = DateTime.Now
            };
            _userService.User_Extension_Insert(userExtension);
            return Json(0);
        }

        [HttpPost]
        public JsonResult KycReset()
        {
            var userInfo = GetCurrentUser();
            if (userInfo == null)
                return Json(0);

            var model = _userService.User_Extension_GetDetail(userInfo.Id);
            if (model != null)
            {
                try
                {
                    string folder = Server.MapPath("~/Upload");
                    string fileFontSide = string.Format("{0}/{1}", folder, model.FontSideUrl);
                    if (System.IO.File.Exists(fileFontSide))
                    {
                        System.IO.File.Delete(fileFontSide);
                    }
                    string fileBackSider = string.Format("{0}/{1}", folder, model.BackSideUrl);
                    if (System.IO.File.Exists(fileBackSider))
                    {
                        System.IO.File.Delete(fileBackSider);
                    }
                    string fileSelfie = string.Format("{0}/{1}", folder, model.SelfieUrl);
                    if (System.IO.File.Exists(fileSelfie))
                    {
                        System.IO.File.Delete(fileSelfie);
                    }
                }
                catch
                {

                }
                _userService.User_Extension_Delete(userInfo.Id);
                return Json(1);
            }
            return Json(0);
        }

        [HttpPost]
        public JsonResult UploadFile()
        {
            Kcy kcy = new Kcy();
            var userInfo = GetCurrentUser();
            if (userInfo == null)
            {
                kcy.Success = "Forbiden";
                return Json(kcy);
            }

            var model = _userService.User_Extension_GetDetail(userInfo.Id);
            if (model != null && (model.Status == 1 || model.Status == 2))
            {
                kcy.Success = "Exists a updated.";
                return Json(kcy);
            }
            try
            {
                foreach (string file in Request.Files)
                {
                    var fileContent = Request.Files[file];
                    if (fileContent != null && fileContent.ContentLength > 0)
                    {
                        // check validate image
                        //CheckUploadFile validateIamge = new CheckUploadFile();
                        string fileContentType = fileContent.ContentType; // getting ContentType
                        byte[] tempFileBytes = new byte[fileContent.ContentLength]; // getting filebytes
                        var data = fileContent.InputStream.Read(tempFileBytes, 0, Convert.ToInt32(fileContent.ContentLength));
                        //var types = CheckUploadFile.FileType.Image; // Setting Image type
                        var result = CheckUploadFile.isValidImageFile(tempFileBytes, fileContentType);
                        //
                        if (!result)
                        {
                            string ext = Path.GetExtension(fileContent.FileName);
                            string name = Guid.NewGuid().ToString();
                            string fileName = string.Format("{0}-{1}-{2}{3}", userInfo.Username, userInfo.Id, name, ext);
                            string _path = Path.Combine(Server.MapPath("~/Upload/TracklogFile"), fileName);
                            fileContent.SaveAs(_path);

                            string ipPC = HelperCommon.GetUserIP();
                            var countryData = _userService.CountryFromIP(ipPC);
                            string body = "Hi, ";
                            body += "<br />Ip location: " + ipPC;
                            if (countryData != null)
                            {
                                body += "<br />Country: " + countryData.country;
                                body += "<br />City: " + countryData.City;
                                body += "<br />timezone: " + countryData.timezone;
                                body += "<br />regionName: " + countryData.regionName;
                                body += "<br />org: " + countryData.org;
                            }

                            body += "<br />Email: " + userInfo.Email;
                            body += "<br />Username: " + userInfo.Username;
                            body += "<br />FileName: " + fileContent.FileName;
                            body += "<br />FileType: " + fileContentType;
                            body += "<br />Link: " + _path;
                            var mail = new Email
                            {
                                Title = "[FBT] Upload file fail - " + userInfo.Username,
                                Body = body,
                                EmailTo = "longpc0209@gmail.com"
                            };

                            _userService.SendMail(mail);
                            kcy.Success = "The file you uploaded is not an image format (Eg: .jpg .jpeg .bmp .gif .png)";
                            return Json(kcy);
                        }
                        else
                        {
                            string ext = Path.GetExtension(fileContent.FileName);
                            string name = Guid.NewGuid().ToString();

                            string fileName = string.Format("{0}-{1}-{2}{3}", userInfo.Username, userInfo.Id, name, ext);
                            if (file.Equals("FontSide"))
                            {
                                kcy.FontSide = fileName;
                            }
                            else if (file.Equals("BackSide"))
                            {
                                kcy.BackSide = fileName;
                            }
                            else if (file.Equals("Selfie"))
                            {
                                kcy.Selfie = fileName;
                            }
                            string _path = Path.Combine(Server.MapPath("~/Upload"), fileName);
                            fileContent.SaveAs(_path);
                        }

                    }
                }
                kcy.Success = "success";
                return Json(kcy);
            }
            catch
            {
                kcy.Success = "Unknown";
            }

            return Json(kcy);
        }
        #endregion

        #region withdraw
        public ActionResult Withdraw()
        {
            var user = GetCurrentUser();
            if (user == null)
                return Redirect("/login");

            var dataWallet = _userService.User_WalletAddress_GetByUserId(user.Id);
            var model = new DataWithdraw
            {
                Code = user.Code,
                FA2Code = user.FA2Code,
                FromType = 0,
                ToType = 0,
                Amount = (decimal)double.Parse(dataWallet.MoneyUSD.ToString()),
                MoneyBTC = (decimal)double.Parse(dataWallet.MoneyBTC.ToString())
            };
            ViewBag.WalletBTC = user.WalletCoin;
            ViewBag.WalletETH = user.WalletETH;
            ViewBag.ModelSecurity = GetFACode(user);
            return View(model);
        }

        [HttpPost]
        public JsonResult Comfirm(decimal amount, string type, string method)
        {
            int userID = CurrentUserId();
            ResponseAmount response = new ResponseAmount { Coin = 0, Meg = "Invalid value", ClassColor = "danger" };
            type = type.ToLower();
            method = method.ToLower();
            if (!type.Equals(SimpleConstant.BTC) && !type.Equals(SimpleConstant.ETH))
            {
                return Json(response);
            }
            if (!method.Equals(SimpleConstant.USD) && !method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                return Json(response);
            }

            if (userID > 0 && amount > 0)
            {
                response = CalculatorAmount(amount, type, method);
            }
            return Json(response);
        }

    

        [HttpPost]
        public JsonResult GetDataWithdraw(string method)
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            method = method.ToLower();
            var data = _userService.User_Get_List_Amount(userId, method);
            if (method.Equals(SimpleConstant.USD))
            {
                result.Message = string.Format("<input type='number' onkeyup='investChange()' value='{0}' id='withdraw-amount' placeholder='0' />", double.Parse(data.FirstOrDefault().ToString()));
            }
            else
            {
                string meg = "<select id='withdraw-amount' onchange='investChange()'>";
                if (data.Count > 0)
                {
                    foreach (decimal mo in data)
                    {
                        meg += string.Format("<option value='{0}'>${1}</option>", mo, _helper.FormatNumber(mo));
                    }
                }
                else
                {
                    meg += "<option value='0'>$0</option>";
                }
                meg += "</select>";
                result.Message = meg;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult WithdrawHistory(int pageIndex, int pageSize)
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format("and A.UserId = {0}", userId);

                var lst = _userService.Withdraw_History(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst.Select(x => new Withdraw
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
                    HashCode = x.HashCode
                }).ToList();
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult Withdraw(decimal amount, string codeDigit, string address, string type, string method)
        {
            Alert meg = new Alert();

            var dataUser = GetCurrentUser();

            bool isWithdraw = _userService.Validate_User_Withdraw(dataUser.Id);
            if (isWithdraw)
            {
                meg.Message = "You have made a withdrawal order in progress";
                meg.ClassCss = "danger";
                return Json(meg);
            }

            if (string.IsNullOrEmpty(address))
            {
                meg.Message = "Please input wallet address";
                meg.ClassCss = "danger";
                return Json(meg);
            }

            //if (_helper.IsDayOffWeek(true))
            //{
            //    meg.Message = "Please withdraw money on weekdays, excluding Saturdays and Sundays";
            //    meg.ClassCss = "danger";
            //    return Json(meg);
            //}
            //var modelKyc = _userService.User_Extension_GetDetail(dataUser.Id);
            //if (modelKyc == null || (modelKyc != null && modelKyc.Status != 2))
            //{
            //    meg.Message = "Please verify KYC before withdrawing money";
            //    meg.ClassCss = "danger";
            //    return Json(meg);
            //}
            decimal minCoin;
            type = type.ToLower();
            method = method.ToLower();
            if (method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                minCoin = _userService.GetSettingByKey<decimal>(SimpleConstant.WITHDRAW_FBT_MIN, 10000);
            }
            else
            {
                minCoin = _userService.GetSettingByKey<decimal>("Withdraw.USD.Min", 50);
            }
            if (amount < minCoin)
            {
                meg.Message = string.Format("The amount you withdraw must be greater than {0} {1}", minCoin.ToString(), method.ToUpper());
                meg.ClassCss = "danger";
                return Json(meg);
            }
            ResponseAmount response = new ResponseAmount { Coin = 0, Meg = "Invalid value", ClassColor = "danger" };

            if (!method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                if (!type.Equals(SimpleConstant.BTC) && !type.Equals(SimpleConstant.ETH))
                {
                    return Json(response);
                }
            }

            if (!method.Equals(SimpleConstant.USD) && !method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                return Json(response);
            }

            response = CalculatorAmount(amount, type, method);
            if (response != null)
            {
                if (dataUser != null)
                {
                    //if (!string.IsNullOrEmpty(dataUser.FA2Code))
                    //{
                    //    if (string.IsNullOrEmpty(codeDigit))
                    //    {
                    //        meg.Message = "Please input 6 digit";
                    //        meg.ClassCss = "danger";
                    //        meg.EnableAuthy = true;
                    //        return Json(meg);
                    //    }
                    //    else
                    //    {
                    //        TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                    //        string UserUniqueKey = dataUser.FA2Code;
                    //        bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);

                    //        if (!isValid)
                    //        {
                    //            meg.Message = "2FA code not veryfied";
                    //            meg.ClassCss = "danger";
                    //            meg.EnableAuthy = true;
                    //            return Json(meg);
                    //        }
                    //    }
                    //}

                    var dataAmount = _userService.User_Get_List_Amount(dataUser.Id, method);
                    if (method.Equals(SimpleConstant.USD) && dataAmount.FirstOrDefault() < (amount + response.Fee))
                    {
                        meg.ClassCss = "danger";
                        meg.Message = "The amount of you requested is more than the amount you are having";
                        return Json(meg);
                    }
                    //if (method.Equals(SimpleConstant.WITHDRAW_ERC20) && dataAmount.FirstOrDefault() < (amount + response.Fee))
                    //{
                    //    meg.ClassCss = "danger";
                    //    meg.Message = "The amount of you requested is more than the amount you are having";
                    //    return Json(meg);
                    //}
                    //if (method.Equals(SimpleConstant.INV) && !dataAmount.Any(x => x == amount))
                    //{
                    //    meg.ClassCss = "danger";
                    //    meg.Message = "No investment package found";
                    //    return Json(meg);
                    //}

                    if (response.Coin > 0 && string.IsNullOrEmpty(response.Meg))
                    {
                        int typewithdraw;
                        if (method.Equals(SimpleConstant.WITHDRAW_ERC20))
                        {
                            typewithdraw = (int)MethodPayment.GES;
                        }
                        else
                        {
                            typewithdraw = type.Equals("eth") ? (int)MethodPayment.ETH : (int)MethodPayment.BTC;
                        }
                        var model = new Withdraw
                        {
                            UserId = dataUser.Id,
                            FromType = method.Equals(SimpleConstant.USD) ? (int)MethodPayment.USD : (int)MethodPayment.GES,
                            ToType = typewithdraw,
                            AmountSet = response.Amount,
                            Fee = response.Fee,
                            AmountGet = response.Coin,
                            Transaction = address,
                            Status = (int)WithdrawStatus.UnconfirmedEmail,
                            Method = method
                        };
                        string tokenAccess = Guid.NewGuid().ToString();
                        model.TokenConfirm = tokenAccess;
                        model.IsConfirmEmail = false;
                        var result = _userService.Withdraw_Insert(model);

                        if (result > 0)
                        {
                            var timeExpire = _userService.GetSettingByKey<decimal>("Withdraw_Time_Expire_UnconfirmedEmail", 2);
                            string urlHost = _helper.GetDomain();
                            string url = string.Format("{0}/withdraw-confirm?token={1}", urlHost, tokenAccess);

                            string body, template = "";
                            body = "Hello, " + "<b>" + dataUser.FullName+"</b>";
                            body += string.Format("<br/>You have withdraw <b>{0} {1}</b> to wallet {2}", HelperCommon.NumberFormat(response.Amount), method.Equals(SimpleConstant.USD) ? " USD" : " ", address);
                            body += "<br/>Confirm transaction click the URL below. </br><a href=\"" + url + "\">Withdraw to wallet</a>";
                            body += string.Format("<br/>This transaction will expire in {0} hours.", timeExpire);
                            var sr = new StreamReader(Server.MapPath("/Content/") + "withdraw-confirmation.html");
                            template = sr.ReadToEnd();
                            template = template.Replace("{titletop}", "Withdraw to wallet");
                            template = template.Replace("{titlecontent}", "");
                            template = template.Replace("{bodycontent}", body);
                            template = template.Replace("{linkaction}", url);
                            template = template.Replace("{messagebutton}", "Click to here");
                            var mail = new Email
                            {
                                Title = "[Eq Option] - Confirm Withdraw",
                                Body = template,
                                EmailTo = dataUser.Email
                            };
                            _userService.SendMail(mail);
                            sr.Close();

                            // cc
                            var typename = method.Equals(SimpleConstant.USD) ? " USD" : " FBT";
                            var body2 = "ID Withdraw: " + dataUser.Username;
                            body2 += "<br/>Amount: " + response.Amount + " " + typename;
                            body2 += "<br/>Wallet: " + address;
                            body2 += "<br/>Create At: " + DateTime.Now.ToString();
                            var listemail = _userService.GetSettingByKey<string>("List.Email.Admin", "");
                            var emailadmin = _userService.GetSettingByKey<string>("Email.Admin", "");
                            var mail2 = new Email
                            {
                                Title = "***** [FBT] - User WITHDRAW *****",
                                Body = body2,
                                EmailTo = emailadmin,
                                cc = listemail
                            };
                            _userService.SendMail(mail2);
                            meg.Success = true;
                            meg.ClassCss = "success";
                            meg.Message = "Please confirm via email to withdraw"; //Withdraw success, We will process within 48h
                            return Json(meg);
                        }
                        else if (result == -1)
                        {
                            meg.ClassCss = "danger";
                            meg.Message = "The amount of coin you requested is more than the amount you are having";
                            return Json(meg);
                        }
                    }
                }
            }

            meg.ClassCss = "danger";
            meg.Message = response.Meg;
            return Json(meg);
        }

        [HttpPost]
        public ActionResult PaymentType(int id)
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            var result = new CustomJsonResult();
            var lstEcouponType = Enum.GetValues(typeof(MethodPayment))
                .Cast<Enum>()
                .Where(m => m.ToString() != "NotSet")
                .Select(m =>
                {
                    string enumText = Enum.GetName(typeof(MethodPayment), m);
                    int enumValue = Convert.ToInt32(m);
                    return new SelectListItem()
                    {
                        Text = enumText,
                        Value = Convert.ToString(enumValue)
                    };
                })
                .OrderBy(m => Convert.ToInt32(m.Value))
                .ToList();

            var wallet = _userService.User_WalletAddress_GetByUserId(userID);
            switch (id)
            {
                case 1:
                    result.Message = wallet.MoneyBTC.ToString();
                    break;
                case 2:
                    result.Message = wallet.MoneyETH.ToString();
                    break;
                case 4:
                    result.Message = wallet.MoneyUSD.ToString();
                    break;
            }

            result.Result = lstEcouponType.Where(x => int.Parse(x.Value) != 3 && int.Parse(x.Value) != 4);
            return Json(result);
        }

        #region Calculator      
        private ResponseAmount CalculatorAmount(decimal setAmount, string type, string method)
        {
            decimal feePercent = 0;
            decimal coinAmount = 0;
            if (method.Equals(SimpleConstant.USD))
            {
                feePercent = _userService.GetSettingByKey<decimal>("Fee.WithDraw.USD.Percent", 0);
            }
            else if (method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                feePercent = _userService.GetSettingByKey<decimal>("Fee.WithDraw.BNCT.Percent", 0);
            }
            else
            {
                feePercent = _userService.GetSettingByKey<decimal>("Fee.WithDraw.Invest.Percent", 0);
            }

            decimal feeUsd = (setAmount * feePercent) / 100;
            decimal amountUsd = setAmount;

            if (!method.Equals(SimpleConstant.WITHDRAW_ERC20))
            {
                if (type.Equals(SimpleConstant.BTC))
                {
                    coinAmount = _userService.Convert_USD_To_BTC(amountUsd);
                }
                else
                {
                    coinAmount = _userService.Convert_USD_To_ETH(amountUsd);
                }
            }
            else
            {
                coinAmount = amountUsd;
            }


            return new ResponseAmount
            {
                Amount = setAmount,
                Fee = Math.Round(feeUsd, 2),
                Coin = Math.Round(coinAmount, 4)
            };
        }
        #endregion

        #endregion

        #region deposit
        [HttpPost]
        public JsonResult UserAssets()
        {
            var curentUser = GetCurrentUser();
            if (curentUser == null)
                return null;
            //var oneBTC = _userService.BitcoinPrice();
            //var oneETH = _userService.EthereumPrice();
            //var oneBNCT = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, 0);
            var userData = GetDasboarch(curentUser.Id);
            return Json(new
            {
                totalbalancedecimal = userData.MoneyUSD,
                totalbalance = _helper.FormatNumber(userData.MoneyUSD, "{0:#,##0}"),
                totalTrade = _helper.FormatNumber(userData.TotalTrade, "{0:#,##0}")
                //totalTrade = userData.TotalTrade
            }); ;
        }

        [HttpPost]
        public JsonResult Cvtusdtocoin(decimal amount, int type)
        {
            try
            {
                if (type == (int)EnumMethod.BTC)
                {
                    return Json(Math.Round(_userService.Convert_USD_To_BTC(amount), 8));
                }
                else if (type == (int)EnumMethod.ETH)
                {
                    return Json(Math.Round(_userService.Convert_USD_To_ETH(amount), 8));
                }
                else
                {
                    return Json(0);
                }
            }
            catch
            {

                return Json(0);
            }

        }

        public ActionResult Deposit(int? id)
        {
            if (!id.HasValue)
            {
                id = (int)EnumMethod.ETH;
            }


            var user = GetCurrentUser();
            if (user == null || (id > 3 && id < 1))

            return Redirect("/login");
            string nameWallet = string.Format("{0}_{1}", user.Username, user.Id);
            var dataWallet = _userService.User_WalletAddress_GetByUserId(user.Id);

            if (!string.IsNullOrEmpty(dataWallet.WalletBTC) && id.Value == (int)EnumMethod.BTC)
            {
                ViewBag.Address = dataWallet.WalletBTC;
                dataWallet.EnumMethod = (int)EnumMethod.BTC;
            }
            else if (!string.IsNullOrEmpty(dataWallet.WalletETH) && id.Value == (int)EnumMethod.ETH)
            {
                ViewBag.Address = dataWallet.WalletETH;
                dataWallet.EnumMethod = (int)EnumMethod.ETH;
            }
            try
            {
                ShowWalletData objwallet = new ShowWalletData();
                objwallet.AddressBTC = dataWallet.WalletBTC == null ? "" : dataWallet.WalletBTC;
                objwallet.AddressETH = dataWallet.WalletETH;
                objwallet.WalletActive = id.Value;
                ViewBag.DataWallet = objwallet;
            }
            catch
            {
                ViewBag.DataWallet = new ShowWalletData();
            }
            int total = 0;
            string whereClause = string.Format("and A.AddressWallet in ('{0}', '{1}','{2}')", dataWallet.WalletBTC, dataWallet.WalletETH,dataWallet.WalletUSDT);
            var lst = _userService.Admin_CoinTransaction_List(0, 10, out total, whereClause);
            return View(lst);
        }

        [HttpPost]
        public ActionResult ListDeposit(int pageIndex, string orderClause, string wallet)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 5;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(wallet))
                {
                    whereClause += string.Format("and A.AddressWallet = '{0}'", wallet);
                }
                else
                {
                    result.Message = "Not found.";
                    return Json(result);
                }

                var lst = _userService.Admin_CoinTransaction_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult UsdtSendDeposit(decimal amount, string txhash)
        {
            try
            {
                if (amount > 0 && !string.IsNullOrEmpty(txhash.Trim()))
                {
                    UserDepositByUSDT deposit = new UserDepositByUSDT();
                    var curentUser = GetCurrentUser();
                    deposit.Amount = amount;
                    deposit.TxHash = txhash;
                    deposit.UserId = curentUser.Id;
                    var result = _userService.User_DepositBy_USDT_Insert(deposit);
                    if (result > 0)
                    {
                        return Json("Success, You will receive USDT in your USDT when we receive your payment");
                    }
                    else
                    {
                        return Json("Error, Please try again");
                    }
                }
                else
                {
                    return Json("Error, Please enter: Amount and TxHash");
                }
            }
            catch
            {
                return Json("Error, Please try again");
            }

        }
        [HttpPost]
        public ActionResult UsdtDepositLst(int pageIndex, string orderClause)
        {
            var user = GetCurrentUser();
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 15;
            try
            {
                string whereClause = string.Empty;
                if (user.Id > 0)
                {
                    whereClause += string.Format("and U.Id = {0}", user.Id);
                }
                var lst = _userService.User_DepositBy_USDT_Lst(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);
                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region "Wallet"
        public ActionResult Wallet()
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            var data = _userService.User_WalletAddress_GetByUserId(userID);

            return View(data);
        }
        #endregion

        #region List Refferal
        public ActionResult Referral()
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            var data = _treeService.T_TreeData_ShowTree(userID, 7);
            SeededTreeNodes model = new SeededTreeNodes { Seed = null, TreeNodes = data, UserId = userID };

            return View(model);
        }
        #endregion

        #region Policy
        public ActionResult Policy()
        {

            return View();
        }
        #endregion

        #region Network
        public ActionResult Network(int? rootId, string keywork)
        {
            try
            {
                var userCurent = GetCurrentUser();
                if (userCurent == null)
                {
                    return Redirect("/login");
                }

                if (rootId == null)
                {
                    rootId = userCurent != null ? userCurent.Id : rootId;
                }

                var userData = GetDasboarch(rootId.Value);

                if (!string.IsNullOrEmpty(keywork))
                {
                    var userKeywork = _userService.User_GetByUsername(keywork);
                    if (userKeywork == null)
                    {
                        userKeywork = _userService.User_GetByEmail(keywork);
                    }

                    if (userKeywork != null)
                    {
                        rootId = userKeywork.Id;
                    }
                }

                // Check userID exists in treedata
                bool hasRoot = _treeService.CheckUserIdExistsRoot(userCurent.Id, rootId.Value);
                List<ShowTree> data = new List<ShowTree>();
                if (hasRoot || rootId == userCurent.Id)
                {
                    data = _treeService.T_TreeData_ShowTree(rootId.Value, 5);
                    ViewBag.TreeData = PrepareTree(data);
                }
                else
                {
                    return Redirect("/dashboard");
                }
                ViewBag.SponsorCode = userCurent.Code;
                ViewBag.Keywork = keywork;
                return View(userData);
            }
            catch
            {

                throw;
            }

        }

        [HttpGet]
        public ActionResult Networks(string keywork)
        {
            var userCurent = GetCurrentUser();
            if (userCurent == null)
            {
                return Redirect("/login");
            }

            var userData = GetDasboarch(userCurent.Id);

            int? rootId = null;
            if (rootId == null)
            {
                rootId = userCurent != null ? userCurent.Id : rootId;
            }

            if (!string.IsNullOrEmpty(keywork))
            {
                keywork = keywork.Trim();
                var userKeywork = _userService.User_GetByUsername(keywork);
                if (userKeywork == null)
                {
                    userKeywork = _userService.User_GetByEmail(keywork);
                }

                if (userKeywork != null)
                {
                    rootId = userKeywork.Id;
                }
            }

            // Check userID exists in treedata
            bool hasRoot = _treeService.CheckUserIdExistsRoot(userCurent.Id, rootId.Value);
            List<ShowTree> data = new List<ShowTree>();
            if (hasRoot || rootId == userCurent.Id)
            {
                data = _treeService.T_TreeData_ShowTree(rootId.Value, 5);
                ViewBag.TreeData = PrepareTree(data);
            }
            else
            {
                return Redirect("/office/network");
            }
            ViewBag.SponsorCode = userCurent.Code;
            ViewBag.Keywork = keywork;
            return View(userData);
        }

        private string PrepareTree(List<ShowTree> trees)
        {
            try
            {
                var treeData = PrepareTreeNode(trees);
                treeData = treeData.OrderBy(x => x.Level).ThenBy(x => x.Node).ToList();
                StringBuilder str = new StringBuilder();
                Dictionary<int, bool> dict = new Dictionary<int, bool>();
                Dictionary<int, bool> Investdict = new Dictionary<int, bool>();
                foreach (ShowTree n in treeData)
                {
                    int userId = n.UserId;
                    string format = string.Empty;
                    if (n.Level == -1)
                    {
                        format = "[{ v: 'L00', f: '<div><a><img id=\"" + n.UserId.ToString() + "\" onmouseover=\"callTooltip(this,id);\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock) + "\" /></a><div class=\"profile-name\"><a>" + n.Username + "</a></div></div>' }, '', ''],";
                        str.Append(format);
                        dict.Add(n.UserId, n.IsLock);
                        Investdict.Add(n.UserId, n.IsInvest);
                    }
                    else if (n.Level == 0)
                    {
                        string _url = string.Empty;
                        string _profile = string.Empty;
                        bool isLockParent = dict[n.ParentId.Value];
                        bool isInvest = Investdict[n.ParentId.Value];
                        if (n.IsDraftUser)
                        {
                            _url = "<a>" + n.Username + "</a><br>";
                            if (isLockParent || !isInvest)
                            {
                                _profile = "<a ><img style=\"cursor: pointer;\"  id=\"" + n.Code + "\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock) + "\" /></a>";
                            }
                            else
                            {
                                _profile = "<a><img style=\"cursor: pointer;\"  id=\"" + n.Code + "\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock, true) + "\" /></a>";
                            }
                            format = "[{ v: 'L" + n.UserId.ToString() + "', f: '<div id=\"" + n.UserId.ToString() + "\">" + _profile + "<div class=\"profile-name\">" + _url + "</div></div>' }, 'L00', ''],";
                            str.Append(format);
                            if (!dict.ContainsKey(n.UserId))
                                dict.Add(n.UserId, true);
                            if (!Investdict.ContainsKey(n.UserId))
                                Investdict.Add(n.UserId, false);
                        }
                        else
                        {
                            _url = "<a  href=\"/office/network?rootId=" + userId.ToString() + "\">" + n.Username + "</a><br>";
                            _profile = "<a href=\"/office/network?rootId=" + userId.ToString() + "\"><img id=\"" + n.UserId.ToString() + "\" onmouseover=\"callTooltip(this,id);\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock) + "\" /></a>";

                            format = "[{ v: 'L" + n.UserId.ToString() + "', f: '<div>" + _profile + "<div class=\"profile-name\">" + _url + "</div></div>' }, 'L00', ''],";
                            str.Append(format);
                        }
                        if (!dict.ContainsKey(n.UserId))
                            dict.Add(n.UserId, n.IsLock);
                        if (!Investdict.ContainsKey(n.UserId))
                            Investdict.Add(n.UserId, n.IsInvest);
                    }
                    else if (n.Level == 1)
                    {
                        string _url = string.Empty;
                        string _profile = string.Empty;
                        bool isLockParent = dict[n.ParentId.Value];
                        bool isInvest = Investdict[n.ParentId.Value];
                        if (n.IsDraftUser)
                        {
                            _url = "<a>" + n.Username + "</a><br>";
                            if (isLockParent || !isInvest)
                            {
                                _profile = "<a><img style=\"cursor: pointer;\"  id=\"" + n.Code + "\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock) + "\" /></a>";
                            }
                            else
                            {
                                _profile = "<a><img style=\"cursor: pointer;\"  id=\"" + n.Code + "\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock, true) + "\" /></a>";
                            }
                            format = "[{ v: 'L" + n.UserId.ToString() + "', f: '<div id=\"" + n.UserId.ToString() + "\">" + _profile + "<div class=\"profile-name\">" + _url + "</div></div>' }, 'L" + n.ParentId.ToString() + "', ''],";
                            str.Append(format);
                            if (!dict.ContainsKey(n.UserId))
                                dict.Add(n.UserId, true);
                            if (!Investdict.ContainsKey(n.UserId))
                                Investdict.Add(n.UserId, false);
                        }
                        else
                        {
                            _url = "<a  href=\"/office/network?rootId=" + userId.ToString() + "\">" + n.Username + "</a><br>";
                            _profile = "<a href=\"/office/network?rootId=" + userId.ToString() + "\"><img id=\"" + n.UserId.ToString() + "\" onmouseover=\"callTooltip(this,id);\" src=\"/images/icon/" + TreeSetIcon(n.UserId, n.IsInvest, n.IsDraftUser, n.IsLock) + "\" /></a>";

                            format = "[{ v: 'L" + n.UserId.ToString() + "', f: '<div>" + _profile + "<div class=\"profile-name\">" + _url + "</div></div>' }, 'L" + n.ParentId.ToString() + "', ''],";
                            str.Append(format);
                        }
                        if (!dict.ContainsKey(n.UserId))
                            dict.Add(n.UserId, n.IsLock);
                        if (!Investdict.ContainsKey(n.UserId))
                            Investdict.Add(n.UserId, n.IsInvest);
                    }
                }
                return str.ToString();
            }
            catch
            {

                throw;
            }

        }
        private List<ShowTree> PrepareTreeNode(List<ShowTree> trees)
        {
            var Lv0 = trees.Where(s => s.Level == -1).ToList();
            var Lv1 = trees.Where(s => s.Level == 0).ToList();
            var Lv2 = trees.Where(s => s.Level == 1).ToList();
            var Lv3 = trees.Where(s => s.Level == 2).ToList();
            int lv1Total = Lv1.Count;
            int lv2Total = Lv2.Count;
            int lv3Total = Lv3.Count;
            if (lv1Total > 0)
            {
                if (lv1Total == 1)
                {
                    int _parent21 = Lv1[0].UserId;//node left
                    int _nodeTree = Lv1[0].Node;
                    //kiem tra node parent full node chua?
                    var Lv21 = Lv2.Where(s => s.ParentId == _parent21).ToList();
                    if (Lv21.Count == 0)
                    {
                        if (_nodeTree == 1)
                        {
                            trees.Add(new ShowTree { UserId = 2000000003, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                            trees.Add(new ShowTree { UserId = 2000000004, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                        }
                        else
                        {
                            trees.Add(new ShowTree { UserId = 2000000005, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                            trees.Add(new ShowTree { UserId = 2000000006, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                        }
                    }
                    else if (Lv21.Count == 1)
                    {
                        int _node = 2;
                        int _uid = _nodeTree == 1 ? 2000000004 : 2000000006;
                        if (Lv21.FirstOrDefault().Node == 2)
                        {
                            _uid = _nodeTree == 1 ? 2000000003 : 2000000005;
                            _node = 1;
                        }
                        trees.Add(new ShowTree { UserId = _uid, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = _node });
                    }
                    //node right
                    if (_nodeTree == 1)
                    {
                        trees.Add(new ShowTree { UserId = 2000000002, ParentId = Lv0[0].UserId, Code = Lv0[0].Code, FullName = "", Level = 0, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                        trees.Add(new ShowTree { UserId = 2000000005, ParentId = 2000000002, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                        trees.Add(new ShowTree { UserId = 2000000006, ParentId = 2000000002, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                    }
                    else
                    {
                        trees.Add(new ShowTree { UserId = 2000000001, ParentId = Lv0[0].UserId, Code = Lv0[0].Code, FullName = "", Level = 0, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                        trees.Add(new ShowTree { UserId = 2000000003, ParentId = 2000000001, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                        trees.Add(new ShowTree { UserId = 2000000004, ParentId = 2000000001, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                    }
                }
                else //total=2
                {
                    int _parent21 = Lv1[0].UserId; //note left
                    //kiem tra node parent full node chua?
                    var Lv21 = Lv2.Where(s => s.ParentId == _parent21).ToList();
                    if (Lv21.Count == 0)
                    {
                        trees.Add(new ShowTree { UserId = 2000000003, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                        trees.Add(new ShowTree { UserId = 2000000004, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                    }
                    else if (Lv21.Count == 1)
                    {
                        int _node = 2;
                        int _uid = 2000000004;
                        if (Lv21.FirstOrDefault().Node == 2)
                        {
                            _uid = 2000000003;
                            _node = 1;
                        }
                        trees.Add(new ShowTree { UserId = _uid, ParentId = _parent21, Code = Lv1[0].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = _node });
                    }
                    int _parent22 = Lv1[1].UserId; //node right
                    //kiem tra node parent full node chua?
                    var Lv22 = Lv2.Where(s => s.ParentId == _parent22).ToList();
                    if (Lv22.Count == 0)
                    {
                        trees.Add(new ShowTree { UserId = 2000000005, ParentId = _parent22, Code = Lv1[1].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                        trees.Add(new ShowTree { UserId = 2000000006, ParentId = _parent22, Code = Lv1[1].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                    }
                    else if (Lv22.Count == 1)
                    {
                        int _node = 2;
                        int _uid = 2000000006;
                        if (Lv22.FirstOrDefault().Node == 2)
                        {
                            _uid = 2000000005;
                            _node = 1;
                        }
                        trees.Add(new ShowTree { UserId = _uid, ParentId = _parent22, Code = Lv1[1].Code, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = _node });
                    }
                }
            }
            else
            {
                trees.Add(new ShowTree { UserId = 2000000001, ParentId = Lv0[0].UserId, Code = Lv0[0].Code, FullName = "", Level = 0, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                trees.Add(new ShowTree { UserId = 2000000002, ParentId = Lv0[0].UserId, Code = Lv0[0].Code, FullName = "", Level = 0, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                trees.Add(new ShowTree { UserId = 2000000003, ParentId = 2000000001, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                trees.Add(new ShowTree { UserId = 2000000004, ParentId = 2000000001, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
                trees.Add(new ShowTree { UserId = 2000000005, ParentId = 2000000002, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 1 });
                trees.Add(new ShowTree { UserId = 2000000006, ParentId = 2000000002, FullName = "", Level = 1, Email = "", IsActive = false, IsDraftUser = true, Node = 2 });
            }
            return trees;
        }

        private string TreeSetIcon(int userid, bool isActive, bool isDraftUser, bool isLock, bool isAdd = false)
        {
            try
            {
                var userCurent = _userService.Get_Max_Invest_By_Uid(userid) ?? 1;
                if (!isDraftUser)
                {
                    if (isActive)
                    {
                        var icon = "mini-logo-60.png?v=5.9";
                        switch (userCurent)
                        {
                            case 100:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 1000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 3000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 5000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 10000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 20000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 50000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            case 80000:
                                icon = "mini-logo-60.png?v=5.9";
                                break;
                            default:
                                break;
                        }
                        return icon;
                    }
                    else
                    {
                        if (isLock)
                            return "user-deleted.png?v=5.9";
                        return "profile-0.png?v=5.9";
                    }
                }
                else
                {
                    if (isAdd)
                        return "avatar-no.png?v=5.9";
                    return "not-access.png?v=5.9";
                }
            }
            catch
            {

                throw;
            }

        }

        [HttpPost]
        public JsonResult ListUser(string parentCode, string sponsorCode)
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Json("");

            var listUser = _treeService.Tree_GetAllUserByManageId(userID);
            string list = "";
            if (listUser.Count > 0)
            {
                int i = 0;
                foreach (UserIntroduction item in listUser)
                {
                    list += "<tr><td>" + (i + 1) + "</td>";
                    list += "<td style='border-left:1px solid #d1d1d1;text-align:center;'><input type='radio' value='" + item.Id + "' name='listUser' id='" + item.Id + "' /></td>";
                    list += "<td style='border-left:1px solid #d1d1d1;padding-left:5px;'><label for='" + item.Id + "'>" + item.Email + "</label></td>";
                    list += "<td style='border-left:1px solid #d1d1d1;padding-left:5px;'><label for='" + item.Id + "'>" + item.Username + "</label></td>";
                    //list += "<td style='border-left:1px solid #d1d1d1;padding-left:5px;'></td>";
                    list += "</tr>";
                    i++;
                }

            }
            return Json(list);
        }

        [HttpPost]
        public JsonResult TreeAdd(int userId, string code, int note)
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Json("");

            var dataParent = _userService.User_GetByCode(code);
            if (dataParent != null)
            {
                bool hasRoot = _treeService.CheckUserIdExistsRoot(userID, dataParent.Id);
                int node = note % 2 == 0 ? 2 : 1;
                if (hasRoot || userID == dataParent.Id)
                {
                    var model = new ShowTree
                    {
                        ManageId = userID,
                        ParentId = dataParent.Id,
                        UserId = userId,
                        Level = 1,
                        Node = node
                    };
                    int result = _treeService.T_TreeData_AddNode(model);
                    string meg = string.Empty;
                    switch (result)
                    {
                        case 1:
                            meg = "success";
                            break;
                        case -2:
                            meg = "User exist in tree.";
                            break;
                        case -3:
                            meg = "Parent not exists in system data.";
                            break;
                        case -4:
                            meg = node == 1 ? "Left branch has user" : "Right branch has user";
                            break;
                        default:
                            meg = "Overloaded system, please add again.";
                            break;

                    }
                    return Json(meg);
                }
            }

            return Json("");
        }

        [HttpPost]
        public ActionResult SearchNetwork(string keywork)
        {

            return View();
        }
        #endregion

        #region Trading
        public ActionResult Trading()
        {
            return View();
        }

        [HttpPost]
        public ActionResult TradingList(int pageIndex, int pageSize)
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format(" and A.UserId = {0}", userId);

                var lst = _userService.Admin_Trading_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                List<HighchartSyncTrade> data = new List<HighchartSyncTrade>();
                foreach (HighchartSyncTrade item in lst)
                {
                    var color = item.Status == 1 ? "green" : item.Status == -1 ? "red" : "";
                    data.Add(new HighchartSyncTrade
                    {
                        Id = item.Id,
                        IsCall = item.IsCall,
                        IsDemo = item.IsDemo,
                        symbol = item.symbol,
                        MarketName = item.MarketName,
                        PairName = item.PairName,
                        Status = item.Status,
                        _amount = _helper.FormatNumber(item.Amount),
                        CreateTimeStr = item.CreateOn.ToString("yyyy-MM-dd HH:mm:ss"),
                        _profit = item.Profit < 0 ? _helper.FormatNumber(item.Profit * -1) : _helper.FormatNumber(item.Profit),
                        Profit = item.Profit,
                        BeginAmount = item.BeginAmount,
                        OpeningPrice = _helper.FormatNumber(item.BeginAmount),
                        ClosingPrice = _helper.FormatNumber(item.EndAmount),
                        CompleteOnStr = item.CompleteOn.ToString("yyyy-MM-dd HH:mm:ss"),

                    });
                }

                result.Result = data;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region Transaction
        public ActionResult Transaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transaction(int pageIndex, int pageSize, int type = -1, string from_date = "", string to_date = "")
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format(" and A.UserId = {0}", userId);
                if (type > 0)
                {
                    whereClause += string.Format(" and A.Type={0}", type);
                }
                if (!string.IsNullOrEmpty(from_date))
                {
                    whereClause += string.Format(" and A.CreateOn >= '{0} 00:00:00'", from_date);
                }
                if (!string.IsNullOrEmpty(to_date))
                {
                    whereClause += string.Format(" and A.CreateOn <= '{0} 23:59:59'", to_date);
                }

                var lst = _userService.Admin_HistoryTransaction_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);
                List<HistoryTransaction> responsedata = new List<HistoryTransaction>();

                result.Result = lst.Select(s => new HistoryTransaction
                {
                    Amount = s.Amount,
                    ByUserName = s.ByUserName,
                    CreateOn = s.CreateOn,
                    Description = s.Description,
                    FromUser = s.FromUser,
                    Status = s.Status,
                    StatusName = s.StatusName,
                    Type = s.Type,
                    TypeName = s.TypeName,
                    //StrAmount = s.Amount >= 0 ? "+$" + HelperCommon.NumberFormat(s.Amount) : "-$" + HelperCommon.NumberFormat((s.Amount * -1)),
                    StrAmount = (s.Amount >= 0) ? (s.Type == 1 ? "-$" + HelperCommon.NumberFormat(s.Amount) : "+$" + HelperCommon.NumberFormat(s.Amount)) : (s.Type == 1 ? "-$" + HelperCommon.NumberFormat(s.Amount * -1) : "+$" + HelperCommon.NumberFormat(s.Amount * -1))
                   

                });
                //result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #region "investment"
        [HttpPost]
        public JsonResult GetPackage()
        {
            var curentUser = GetCurrentUser();
            List<decimal> invests = new List<decimal> { 1000, 2000, 3000, 4000, 5000, 6000, 7000, 8000, 9000, 10000 };
            string str = string.Empty;
            foreach (int a in invests)
            {
                str += "<option value=\"" + a + "\">$" + _helper.FormatNumber(a) + "</option>";
            }
            return Json(str);
        }

        public ActionResult Investment()
        {
            var curentUser = GetCurrentUser();
            var userData = _packagesService.Dasboarch_Detail(curentUser.Id);
            ViewBag.Balance = HelperCommon.NumberFormat(userData.MoneyUSD);
            return View(userData);
        }

        [HttpPost]
        public JsonResult Investment(decimal amount)
        {
            Alert meg = new Alert();
            var userCurent = GetCurrentUser();
            if (userCurent == null)
            {
                meg.RedirectUrl = "/login";
                return Json(meg);
            }

            if (string.IsNullOrEmpty(userCurent.WalletETH))
            {
                meg.ClassCss = "danger";
                meg.Message = "Please go to the settings to update the wallet ETH.";
                return Json(meg);
            }

            string type = SimpleConstant.USD;
            if (!type.Equals(SimpleConstant.USD))
            {
                meg.ClassCss = "danger";
                meg.Message = "Please select payment method.";
                return Json(meg);
            }

            var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
            if (type.Equals(SimpleConstant.USD) && dataWallet.MoneyUSD < amount)
            {
                meg.ClassCss = "danger";
                meg.Message = "The amount of coin you requested is more than the amount you are having $" + _helper.FormatNumber(dataWallet.MoneyUSD);
                return Json(meg);
            }


            // code here
            var lockkey = string.Format("investment_{0}", userCurent.Id);
            try
            {
                lock (LockHelper.GetLock(lockkey))
                {
                    if (HandleInvest(userCurent, amount, type))
                    {
                        meg.ClassCss = "success";
                        meg.Success = true;
                        meg.Message = "Investment success";
                        return Json(meg);
                    }
                }
            }
            catch (Exception ex)
            {
                _userService.DBLog_Insert("Investment_Exception", ex.ToString(), userCurent.Id, (int)LogType.Normal);
            }
            finally
            {
                LockHelper.ReleaseLock(lockkey);
            }

            meg.ClassCss = "danger";
            meg.Message = "You can't investment now, please come back later.";
            return Json(meg);
        }

        #region calculate bonus
        public bool HandleInvest(UserInfo user, decimal amount, string type)
        {
            bool result = false;
            var bonusData = new Packeges_Bonus
            {
                UserId = user.Id,
                Invested = amount,
                IsProfit = true,
                SharePercent = 0,
                SharePrice = 0,
                ShareTotal = 0,
                CreateOn = DateTime.Now,
                StartProfitDate = DateTime.Now,
                Type = type,
                StockAmount = 0,
                ExpireDate = DateTime.Now.AddMonths(6)
            };
            var packagesId = _packagesService.Packages_Bonus_Insert(bonusData);
            if (packagesId > 0)
            {
                BonusBranch(user.Id, amount, packagesId);
                result = true;
            }
            else
            {
                string json = JsonConvert.SerializeObject(bonusData);
                _userService.DBLog_Insert("Packages_Bonus_Insert", json, packagesId, (int)LogType.Bonus);
            }

            return result;
        }

        private void BonusBranch(int userId, decimal amount, int packageId)
        {
            var model = new UserVolData
            {
                UserId = userId,
                Amount = amount,
                PackageId = packageId
            };

            string json = new JavaScriptSerializer().Serialize(model);
            _treeService.Sysn_Data_Tab_Insert(new SyncDataTab { Status = (int)SyncDataTabStatus.PENDING, ExtraData = json });
        }
        #endregion

        [HttpPost]
        public ActionResult InvestmentHistory(int pageIndex, int pageSize)
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = string.Format("and A.UserId = {0}", userId);

                var lst = _packagesService.Investment_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                lst = lst.Select(x => new InvestmentList
                {
                    Id = x.Id,
                    _createOn = x.CreateOn.ToString("yyyy-MM-dd HH:mm"),
                    _invested = _helper.FormatNumber(x.Invested),
                    _receivedProfit = _helper.FormatNumber(x.SharePercent) + "%",
                    _shareTotal = _helper.FormatNumber(x.ShareTotal) + "%",
                    _action = x.IsActive ? "Runing" : "<span style='color:red;'>Stop</span>"
                }).ToList();

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public JsonResult ConfirmBonus(int id)
        {
            var userCurent = GetCurrentUser();
            decimal amount = _packagesService.Package_GetBonusFinish(userCurent.Id, id);
            return Json(amount);
        }

        [HttpPost]
        public JsonResult PopupRein()
        {
            var userCurent = GetCurrentUser();
            var listRevents = _packagesService.Get_List_Next_Reinventment(userCurent.Id);
            string str = string.Empty;
            if (listRevents.Count > 0)
            {
                foreach (ExpireList ls in listRevents)
                {
                    str += string.Format("Code: {0}, Invest Package: {1} TRX, Remain date: {2}<br>", ls.Id, _helper.FormatNumber(ls.Invested), ls.StrExpireDate);
                }
            }

            string str_v2 = string.Empty;
            var listExpire = _packagesService.Check_Reinventment_Expire(userCurent.Id);
            if (listExpire.Count > 0)
            {
                foreach (ExpireList ls in listExpire)
                {
                    str_v2 += string.Format("Code: {0}, Invest Package: {1} TRX, Lock account at: {2}<br>", ls.Id, _helper.FormatNumber(ls.Invested), ls.StrExpireDate);
                }
            }
            MessagePush push = new MessagePush
            {
                Noti = str,
                Push = str_v2
            };
            return Json(push);
        }

        #endregion

        #region lib
        public InvestPackage BonusPercent(int package)
        {
            var _paka = new InvestPackage
            {
                MaxOut = 300
            };
            switch (package)
            {
                case 100:
                    _paka.MaxOut = 200;
                    _paka.BonusComission = 6;
                    break;
                case 1000:
                    _paka.MaxOut = 225;
                    _paka.BonusComission = 6;
                    break;
                case 3000:
                    _paka.BonusComission = 6;
                    _paka.MaxOut = 250;
                    break;
                case 5000:
                    _paka.BonusComission = 8;
                    _paka.MaxOut = 280;
                    break;
                case 10000:
                    _paka.BonusComission = 8;
                    _paka.MaxOut = 300;
                    break;
                case 20000:
                    _paka.BonusComission = 10;
                    _paka.MaxOut = 330;
                    break;
                case 50000:
                    _paka.BonusComission = 10;
                    _paka.MaxOut = 350;
                    break;
                case 80000:
                    _paka.BonusComission = 10;
                    _paka.MaxOut = 400;
                    break;
            }
            return _paka;
        }

        private bool Is_FaCode()
        {
            return _userService.GetSettingByKey<bool>("FA2Code.Enable", false); ;
        }
        #endregion

        #region "ICO Sell"
        public ActionResult Icocountdown()
        {
            var curentUser = GetCurrentUser();
            var userData = _packagesService.Dasboarch_Detail(curentUser.Id);
            userData.FA2Code = curentUser.FA2Code;
            userData.Fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.Stock.Percent", 0);
            userData.MoneyUSD = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, 0);
            userData.IsEnableFA = _userService.GetSettingByKey<bool>("FA2Code.Enable", true);
            ViewBag.ModelSecurity = GetFACode(curentUser);
            return View(userData);
        }
        [HttpPost]
        public JsonResult SellComfirm(decimal amount)
        {
            if (amount > 0)
            {
                var price = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, 0);
                decimal requestAmount = amount * price;
                decimal fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.Stock.Percent", 0);
                decimal responseFee = Math.Round(requestAmount * fee / 100, 2);
                decimal responseUSD = requestAmount - responseFee;
                return Json(_helper.FormatNumber(responseUSD));
            }
            return Json(0);
        }

        [HttpPost]
        public JsonResult Icocountdown(decimal amount, string codeDigit)
        {
            Alert meg = new Alert();
            var userCurent = GetCurrentUser();
            if (userCurent == null)
            {
                meg.RedirectUrl = "/login";
                return Json(meg);
            }

            if (amount <= 0)
            {
                meg.ClassCss = "danger";
                meg.Message = "Please enter an amount greater than 0";
                return Json(meg);
            }

            bool IsEnableFA = _userService.GetSettingByKey<bool>("FA2Code.Enable", true);
            if (IsEnableFA && !string.IsNullOrEmpty(userCurent.FA2Code))
            {
                if (string.IsNullOrEmpty(codeDigit))
                {
                    meg.Message = "Please input 6 digit";
                    meg.ClassCss = "danger";
                    meg.EnableAuthy = true;
                    return Json(meg);
                }
                else
                {
                    TwoFactorAuthenticator TwoFacAuth = new TwoFactorAuthenticator();
                    string UserUniqueKey = userCurent.FA2Code;
                    bool isValid = TwoFacAuth.ValidateTwoFactorPIN(UserUniqueKey, codeDigit, Constants.TwoFaCodeExpire);
                    if (!isValid)
                    {
                        meg.Message = "2FA code not veryfied";
                        meg.ClassCss = "danger";
                        meg.EnableAuthy = true;
                        return Json(meg);
                    }
                }
            }

            var dataWallet = _userService.User_WalletAddress_GetByUserId(userCurent.Id);
            if (amount > dataWallet.BonusLucky)
            {
                meg.ClassCss = "danger";
                meg.Message = "The amount of coin you requested is more than the stock you are having " + ((double)dataWallet.BonusLucky).ToString();
                return Json(meg);
            }
            var price = _userService.GetSettingByKey<decimal>(Constants.Coin_Price, 0);
            decimal requestAmount = amount * price;
            decimal fee = _userService.GetSettingByKey<decimal>("Fee.Tranfer.Stock.Percent", 0);
            decimal responseFee = Math.Round(requestAmount * fee / 100, 2);
            decimal responseUSD = requestAmount - responseFee;

            int result = _packagesService.SellStock_Create(userCurent.Id, amount, responseUSD, responseFee);
            if (result > 0)
            {
                meg.ClassCss = "success";
                meg.Success = true;
                meg.Message = "Sell success.";
                return Json(meg);
            }
            else if (result == -1)
            {
                meg.ClassCss = "danger";
                meg.Message = "The amount of coin you requested is more than the stock you are having " + ((double)dataWallet.BonusLucky).ToString();
                return Json(meg);
            }

            meg.ClassCss = "danger";
            meg.Message = "Sell failed.";
            return Json(meg);
        }
        #endregion

        #region Manage Account
        public ActionResult AcountInvest()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ManageAccountInvest(int level, string from_date, string to_date, int method)
        {
            int uid = CurrentUserId();
            var dataList = _treeService.MUser_GetListUserByParent(uid, 15);
            decimal totalInvest = 0;
            var respone = new ResponseManageAccount();
            if (dataList.Count > 0)
            {
                if (level > 0)
                {
                    dataList = dataList.Where(x => x.Level == level).ToList();
                }
                DateTime _from_time = DateTime.Now.AddYears(-1);
                DateTime _to_time = DateTime.Now;
                if (!string.IsNullOrWhiteSpace(from_date))
                {
                    from_date = from_date + " 00:00:00";
                    _from_time = HelperCommon.ConvertStringToDatetime(from_date);
                }
                if (!string.IsNullOrWhiteSpace(to_date))
                {
                    to_date = to_date + " 23:59:59";
                    _to_time = HelperCommon.ConvertStringToDatetime(to_date);
                }
                //dataList = dataList.Where(x => x.CreateOn >= _from_time && x.CreateOn <= _to_time).ToList();
                if (dataList.Count > 0)
                {
                    string html = string.Empty;
                    int i = 1;
                    foreach (TreeData da in dataList)
                    {
                        bool calcular = false;
                        decimal _blance = 0;
                        DateTime? _investOn = null;
                        List<string> paskagesList = new List<string>();
                        string _status = string.Empty;
                        if (da.TreeDataItem.Count > 0)
                        {
                            foreach (TreeDataItem item in da.TreeDataItem)
                            {
                                if (method == -1 || item.Status == method)
                                {
                                    if (item.CreateOn < _from_time)
                                        continue;
                                    if (item.CreateOn > _to_time)
                                        continue;
                                    calcular = true;
                                    _blance += item.Invested;
                                    totalInvest += item.Invested;
                                    _status = item.Status == 3 ? "USD" : "BTC";
                                    paskagesList.Add(_status + ": " + _helper.FormatNumber(item.Invested) + "; ");
                                    if (!_investOn.HasValue)
                                        _investOn = item.CreateOn;

                                    if (_investOn.HasValue && _investOn.Value < item.CreateOn)
                                        _investOn = item.CreateOn;

                                }
                            }
                        }
                        if (!calcular)
                        {
                            continue;
                        }
                        html += "<tr>";
                        html += "<td>" + i.ToString() + "</td>";
                        html += "<td>" + da.Username + "</td>";
                        html += "<td>" + da.FullName + "</td>";
                        html += "<td>" + da.Email + "</td>";
                        html += "<td>" + string.Join(",", paskagesList) + "</td>";
                        html += "<td>" + _investOn.Value.ToString("yyyy-MM-dd HH:mm") + "</td>";
                        html += "</tr>";
                        i++;
                    }
                    respone.html = html;
                    respone.total = _helper.FormatNumber(totalInvest);
                    return Json(respone);
                }
            }
            return Json("");
        }

        public ActionResult ManageAccount()
        {
            var userCurent = GetCurrentUser();
            var userData = GetDasboarch(userCurent.Id);
            userData.StrTotalNetworkTrading =HelperCommon.NumberFormat(_packagesService.Get_Total_Trade(userCurent.Id));
            userData.StrTotalTrade = HelperCommon.NumberFormat(userData.TotalTrade);
            //Referral link
            string hostDomain = _helper.GetDomain();
            if (userCurent == null)
            {
                return Redirect("/login");
            }
            ViewBag.ReferLink = string.Format("{0}/register-by?referral={1}", hostDomain, userCurent.Code);
            return View(userData);
        }

        [HttpPost]
        public JsonResult ManageAccount(int pageIndex, int pageSize)
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                
                var lst = _userService.Account_Referal_List(userId,1000,
                    pageIndex,
                    pageSize,
                    out total);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #region Arbittrages
        [AllowAnonymous]
        [HttpPost]
        public JsonResult ArbittrageTransaction_Lst(int pageIndex, int pageSize)
        {
            //var userId = CurrentUserId();
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                string whereClause = "";
                var lst = _userService.ArbittrageTransaction_Lst(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                result.Result = lst;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public JsonResult ArbittrageTransactionIns()
        {
            var userId = CurrentUserId();
            var result = new CustomJsonResult();
            try
            {
                var objexchange = _userService.ExchangeMarkets();
                TradeHistoryTransaction trade = new TradeHistoryTransaction();
                if (objexchange != null)
                {
                    List<Arbittrage> listexchange = objexchange.result;
                    //var exchangesbuy = listexchange.GroupBy(p => p.exchange).Select(g => new Arbittrage { exchange = g.Key });
                    var searchpair = new List<string> { "btcusd", "ethusd", "xrpbtc", "bchusd", "dashbtc", "etheur" };
                    var exchangesbuy = listexchange.GroupBy(g => new { g.exchange, g.pair }).Where(p => searchpair.Contains(p.Key.pair)).ToList();
                    var exchangesSell = exchangesbuy;
                    foreach (var item in exchangesbuy)
                    {
                        var coinbuy = _userService.ExchangeGetPriceCoin(item.Key.exchange, item.Key.pair);
                        var pricecoinbuy = coinbuy != null ? coinbuy.result : null;
                        if (pricecoinbuy != null)
                        {
                            if (pricecoinbuy.price > 0)
                            {
                                foreach (var sellitem in exchangesSell.Where(p => p.Key.pair == item.Key.pair))
                                {
                                    var coinsell = _userService.ExchangeGetPriceCoin(sellitem.Key.exchange, item.Key.pair);
                                    var pricecoinsell = coinsell != null ? coinsell.result : null;
                                    if (pricecoinsell != null)
                                    {
                                        if (pricecoinsell.price > 0)
                                        {
                                            var percent = 100 - (pricecoinbuy.price * 100 / pricecoinsell.price);
                                            if ((double)percent > 0.5)
                                            {
                                                var tradeat = DateTime.Now;
                                                tradeat.AddHours(new System.Random().Next(1, 9));
                                                var minute = new System.Random().Next(1, 9);
                                                tradeat.AddMinutes(minute);
                                                trade = new TradeHistoryTransaction();
                                                trade.BuyExchange = item.Key.exchange;
                                                trade.SellExchange = sellitem.Key.exchange;
                                                trade.BuyPrice = pricecoinbuy.price;
                                                trade.SellPrice = pricecoinsell.price;
                                                trade.TradeAt = tradeat;
                                                trade.CoinPair = item.Key.pair;
                                                trade.PercentDifference = percent;
                                                trade.TransactionID = Guid.NewGuid().ToString();
                                                _userService.ArbittrageTransaction_Ins(trade);
                                            }

                                        }
                                    }
                                }

                            }
                        }
                        Task.Delay(3000);
                    }
                    result.Message = "Success";
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        #endregion

        #endregion

        public bool IsAuthenFa()
        {
            return _userService.GetSettingByKey<bool>("FA2Code.Enable", true);
        }

        #region Top_PairName_Favorite

        [HttpPost]
        public JsonResult Get_PairName_by_UserId()
        {
            var result = new CustomJsonResult();
            try
            {
                var curentUser = GetCurrentUser();

                var pairName_lst = _packagesService.User_PairName_Mapping_Select(curentUser.Id);
                result.Result = pairName_lst.ToList();
                result.Optional = 1;
                return Json(result);
            }
            catch (Exception ex)
            {
                return Json(result);
            }

        }

        [HttpPost]
        public JsonResult PairName_Favorite_Del(string pairname)
        {

            var curentUser = GetCurrentUser();
            var result = _packagesService.PairName_Favorite_Del(curentUser.Id, pairname);
            return Json(result);
        }

        [HttpPost]
        public JsonResult PairName_Favorite_Ins(string pairname)
        {

            var curentUser = GetCurrentUser();
            var result = _packagesService.PairName_Favorite_Ins(curentUser.Id, pairname);
            return Json(result);
        }
        #endregion
        #region Deposit
        [HttpPost]
        public async Task<JsonResult> DepositLoadAddress(string symbol)
        {
            var result = new CustomJsonResult();
            try
            {
                var user = GetCurrentUser();
                if (user == null)
                {
                    result.Message = "Error access";
                    result.Result = null;
                    return Json(result);
                }

                if (!string.IsNullOrEmpty(symbol))
                {
                    CoreExchangeDB dB = new CoreExchangeDB();
                    var coin = dB.CoinLists.Where(c => c.CoinSymbol == symbol.ToUpper()).FirstOrDefault();
                    if (coin != null)
                    {
                        // check wallet is exists in system
                        switch (symbol)
                        {
                            case "USDT":
                                result.Result = await _walletService.User_Get_Wallet<DataMemberWalletUSDT>(user.Id, symbol);
                                break;
                            case "BNB":
                                result.Result = await _walletService.User_Get_Wallet<DataMemberWalletBNB>(user.Id, symbol);
                                break;
                            default:
                                break;
                        }

                        //_walletService.User_CreateWallet_With_Privatekey(user.Id, coin);
                    }

                    ViewBag.DepositSymbol = symbol;
                }

                return Json(result);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Result = null;
                return Json(result);
            }
            
        }
        #endregion


        #region GetData From ForbitCopytrade
        private void DataWallertFromCopytrade(string Username)
        {
            using (var client = new System.Net.Http.HttpClient())
            {
                // HTTP POST
                var baseUrl = "http://localhost:8018";// Request.Url.GetLeftPart(UriPartial.Authority);
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //ViewModelUsers param = new ViewModelUsers() { UserCode = user.Code, FullName = user.FullName, Email =user.Email, Password = user.Password, PasswordComfirm = user.Password};

                string contents = JsonConvert.SerializeObject(Username);
                //client.PostAsync("/api/user/register", new StringContent(contents, Encoding.UTF8, "application/json"));
                var response = client.PostAsync("/api/user/user_wallet_copytrade", new StringContent(contents, Encoding.UTF8, "application/json")).Result;


                //string res = "";
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    
                    Task<CustomJsonResult> responses = JsonConvert.DeserializeObject<Task<CustomJsonResult>>(content.ReadAsStringAsync().Result.ToString());

                }
            }
        }
        #endregion
    }
    public class ResponseManageAccount
    {
        public string html { get; set; }
        public string total { get; set; }
    }
    public class InvestPackage
    {
        public decimal BonusComission { get; set; }
        public decimal MaxOut { get; set; }
    }


}