using Lib.Cache;
using Lib.Domain;
using Lib.Domain.Marketings;
using Lib.Domain.Packages;
using Lib.Domain.Promocodes;
using Lib.Domain.Simples;
using Lib.Domain.Trees;
using Lib.Domain.User;
using Lib.Domain.Withdraws;
using Lib.Service.Service.Marketings;
using Lib.Service.Service.Packages;
using Lib.Service.Service.TreeDatas;
using Lib.Service.Service.User;
using LibDatabaseEntitys;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using Web.SourceCoin.Common;
using Web.SourceCoin.Helpers;
namespace Web.SourceCoin.Controllers
{
    [Authorize(Roles = "ADMIN")]
    public class ManageController : BaseController
    {
        private readonly IMarketingService _marketingService;
        private readonly IPackagesService _packagesService;
        private readonly ITreeService _treeService;
        private Helper _helper;
        //private string userHost = "";
        public ManageController(IUserService userService, IMarketingService marketingService, IPackagesService packagesService, ITreeService treeService) : base(userService)
        {
            _marketingService = marketingService;
            _packagesService = packagesService;
            _treeService = treeService;
            _helper = new Helper();
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult VolumeBuySell()
        {
            return View();
        }
        public ActionResult ClearCache()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ClearCache(string Setting, string Category, string Videos)
        {
            MemoryCacheManager memory = new MemoryCacheManager();
            if (!string.IsNullOrEmpty(Setting))
            {
                memory.ClearKeyFor("SettingValue");
            }
            if (!string.IsNullOrEmpty(Category))
            {
                memory.ClearKeyFor("GetByCategory");
            }
            if (!string.IsNullOrEmpty(Videos))
            {
                memory.ClearKeyFor("GetByVideoTop");
            }
            return View();
        }
        #region setting
        [OverrideAuthorization()]
        [Authorize(Roles = "SUPPERADMIN")]
        public ActionResult GetConfig()
        {
            return View(new SettingEntity());
        }

        [HttpPost]
        public ActionResult ListSettings(int pageIndex, string orderClause, string name)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 100;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    whereClause += string.Format("and (A.Name LIKE '%{0}%' OR A.Value LIKE '%{0}%')", name);
                }

                var lst = _userService.Manage_Setting_GetAll(
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
        public ActionResult GetDetailSetting(int id)
        {
            var result = new CustomJsonResult();

            try
            {
                var detail = new SettingEntity();

                if (id != -1)
                {
                    detail = _userService.Manage_Setting_GetById(id);
                }
                else
                {
                    detail.Id = -1;
                }

                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult ManageSetting(SettingEntity detail)
        {
            var result = new CustomJsonResult();
            MemoryCacheManager memory = new MemoryCacheManager();
            try
            {
                if (detail.Id == -1) // create
                {
                    if (_userService.Manage_Setting_Insert(detail) > 0)
                    {
                        memory.ClearKeyFor("SettingValue");
                    }
                    else
                    {
                        result.Message = "Setting name exists";
                    }
                }
                else // update
                {
                    if (_userService.Manage_Setting_Update(detail) > 0)
                    {
                        memory.ClearKeyFor("SettingValue");
                    }
                    else
                    {
                        result.Message = "Update faild";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        #endregion

        #region Promocode
        public ActionResult Promocode()
        {
            return View(new Lib.Domain.Promocodes.Promocode());
        }
        public ActionResult PromotionSend(int id)
        {
            if (id > 0)
            {
                var listId = _packagesService.Promocode_User_Mapping_By_Promotion(id);
                if (listId.Count > 0)
                {
                    foreach (int userId in listId)
                    {
                        var send = new PromotionSendMail
                        {
                            PromotionId = id,
                            UserId = userId,
                            IsActive = true
                        };
                        _packagesService.PromotionSendMail_Insert(send);
                    }
                }
            }
            return View();
        }
        [HttpPost]
        public ActionResult ListPromocode(int pageIndex, string orderClause, string code)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 20;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(code))
                {
                    whereClause += string.Format("and A.Code='{0}'", code);
                }

                var lst = _packagesService.Promocode_List(
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
        public ActionResult GetDetailPromocode(int id)
        {
            var result = new CustomJsonResult();

            try
            {
                var detail = new Lib.Domain.Promocodes.Promocode();

                if (id != -1)
                {
                    detail = _packagesService.Promocode_GetById(id);
                }
                else
                {
                    detail.Id = -1;
                }

                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult ManagePromocode(Lib.Domain.Promocodes.Promocode detail)
        {
            var result = new CustomJsonResult();
            try
            {
                _packagesService.Promocode_InsertUpdate(detail);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        public ActionResult PromocodeItems(int id)
        {
            return View(new Promocode_User_Mapping { PromocodeId = id });
        }
        [HttpPost]
        public ActionResult ListPromocodeItems(int pageIndex, string orderClause, int proId)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 200;
            try
            {
                string whereClause = string.Empty;
                if (proId > 0)
                {
                    whereClause += string.Format("and A.PromocodeId={0}", proId);
                }
                var lst = _packagesService.PromocodeItems_List(
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
        public ActionResult GetDetailPromocodeItems(int id, int promocodeId)
        {
            var result = new CustomJsonResult();

            try
            {
                var detail = new Promocode_User_Mapping { PromocodeId = promocodeId };

                if (id != -1)
                {
                    detail = _packagesService.PromocodeItems_GetById(id);
                }
                else
                {
                    detail.Id = -1;
                }

                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult ManagePromocodeItems(Promocode_User_Mapping detail)
        {
            var result = new CustomJsonResult();
            try
            {
                _packagesService.PromocodeItems_InsertUpdate(detail);
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        #endregion

        #region MUser
        public ActionResult UserList()
        {
            return View();
        }
        public ActionResult UserDepositUSDT()
        {
            return View();
        }
        public ActionResult KYC()
        {
            return View();
        }
        [HttpPost]
        public ActionResult UserList(int pageIndex, string orderClause, string username)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 15;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(username))
                {
                    whereClause += string.Format("and (U.Username like '{0}%' OR U.Email like '{0}%')", username);
                }

                var lst = _userService.UserData_List(
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
        public ActionResult UsdtDepositLst(int pageIndex, string orderClause, string username, int status, string fromdate, string todate)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 15;
            try
            {
                string whereClause = string.Empty;
                whereClause += string.Format(" and (status={0} or {0} = -1)", status);
                if (!string.IsNullOrEmpty(username))
                {
                    whereClause += string.Format(" and (U.Username like '{0}%' OR U.Email like '{0}%')", username);
                }
                string _fromtime = HelperCommon.ConvertDatetimToString(DateTime.Now.AddYears(-1));
                string _totime = HelperCommon.ConvertDatetimToString(DateTime.Now);
                if (!string.IsNullOrWhiteSpace(fromdate))
                {
                    fromdate = fromdate + " 00:00:00";
                    _fromtime = fromdate;
                }
                if (!string.IsNullOrWhiteSpace(todate))
                {
                    todate = todate + " 23:59:59";
                    _totime = todate;
                }
                whereClause += string.Format(" and CreateDate>='{0}'", _fromtime);
                whereClause += string.Format(" and CreateDate<='{0}'", _totime);

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

        [HttpPost]
        public JsonResult Change_Email(int uid, string email)
        {
            if (string.IsNullOrEmpty(email) || !HelperCommon.IsValidEmail(email) || string.IsNullOrWhiteSpace(email))
            {
                return Json(-1);
            }

            var dataUser = _userService.User_GetByEmail(email);
            if (dataUser != null && dataUser.Id != uid)
            {
                return Json(-2);
            }

            Lib.Domain.User.MUser data = new Lib.Domain.User.MUser
            {
                Id = uid,
                Email = email
            };
            int rel = _userService.User_UpdateEmail(data);

            return Json(rel);
        }

        [HttpPost]
        public JsonResult UsdtSendDeposit_Approve(int id, int type)
        {
            try
            {
                if (id > 0)
                {
                    UserDepositByUSDT deposit = new UserDepositByUSDT();
                    var curentUser = GetCurrentUser();
                    //deposit.UserId = curentUser.Id;
                    deposit.Id = id;

                    var result = _userService.User_DepositBy_USDT_ApproveOrCancel(deposit, type);
                    if (result > 0)
                    {
                        return Json(1);
                    }
                    else
                    {
                        return Json(0);
                    }
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
        public ActionResult Detail(int id)
        {
            var model = _userService.User_Extension_GetDetail(id);
            if (model == null)
            {
                model = new User_Extension();
            }
            return View(model);
        }

        [HttpPost]
        public JsonResult KycRemove(int userId)
        {
            if (userId <= 0)
            {
                return Json(0);
            }
            var model = _userService.User_Extension_GetDetail(userId);
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
                    string fileBackSide = string.Format("{0}/{1}", folder, model.BackSideUrl);
                    if (System.IO.File.Exists(fileBackSide))
                    {
                        System.IO.File.Delete(fileBackSide);
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
                _userService.User_Extension_Delete(userId);
                return Json(1);
            }
            return Json(0);
        }

        [HttpPost]
        public JsonResult KycApprove(int userId)
        {
            if (userId <= 0)
            {
                return Json(0);
            }
            var model = _userService.User_Extension_GetDetail(userId);
            if (model != null && model.Status == 1)
            {
                _userService.User_Extension_UpdateStatus(model.UserId);
                return Json(1);
            }
            return Json(0);
        }

        [HttpPost]
        public JsonResult UnLock(int id)
        {
            if (id <= 0)
            {
                return Json(0);
            }
            int rel = _userService.UnLock_When_Not_Reinvestment(id);
            return Json(rel);
        }
        #endregion

        #region Log
        [OverrideAuthorization()]
        [Authorize(Roles = "SUPPERADMIN")]
        public ActionResult Log()
        {
            var lstType = Enum.GetValues(typeof(LogType))
                .Cast<Enum>()
                .Where(m => m.ToString() != "NotSet")
                .Select(m =>
                {
                    string enumText = Enum.GetName(typeof(LogType), m);
                    int enumValue = Convert.ToInt32(m);
                    return new SelectListItem()
                    {
                        Text = enumText,
                        Value = Convert.ToString(enumValue)
                    };
                })
                .OrderBy(m => Convert.ToInt32(m.Value))
                .ToList();
            ViewBag.LstLogType = new SelectList(lstType, "Value", "Text");

            return View();
        }

        public ActionResult ListLog(int pageIndex, string orderClause, string name, int type)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 20;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(name))
                {
                    whereClause += string.Format(" and (A.Name LIKE '%{0}%' or A.Message like '%{0}%') ", name);
                }
                if (type > -1)
                {
                    whereClause += string.Format(" and A.Type = {0}", type);
                }

                var lst = _userService.Manage_DBLog_GetAll(
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
        public ActionResult GetDetailBDLog(int id)
        {
            var result = new CustomJsonResult();
            try
            {
                var detail = new Dblog();

                if (id != -1)
                {
                    detail = _userService.Manage_DBLog_GetById(id);
                }
                else
                {
                    detail.Id = -1;
                }

                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteLog(int[] arrId)
        {
            CustomJsonResult result = new CustomJsonResult();

            try
            {
                if (_userService.Manage_Delete_LogById(arrId) <= 0)
                {
                    throw new Exception("delete error");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        #endregion

        #region mail marketting
        public ActionResult Marketing()
        {
            var accountMail = _marketingService.MailAccount_List();
            ViewBag.LstAccount = new SelectList(accountMail, "Id", "Email", accountMail);

            var lstType = Enum.GetValues(typeof(MarketingEmailType))
                .Cast<Enum>()
                .Where(m => m.ToString() != "NotSet")
                .Select(m =>
                {
                    string enumText = Enum.GetName(typeof(MarketingEmailType), m);
                    int enumValue = Convert.ToInt32(m);
                    return new SelectListItem()
                    {
                        Text = enumText,
                        Value = Convert.ToString(enumValue)
                    };
                })
                .OrderBy(m => Convert.ToInt32(m.Value))
                .ToList();
            ViewBag.LstMarketingType = new SelectList(lstType, "Value", "Text");

            return View(new MailTemplate());
        }

        [HttpPost]
        public ActionResult MarketingList(int pageIndex, string orderClause)
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
        public ActionResult MarketingDetail(int id)
        {
            var result = new CustomJsonResult();

            try
            {
                var detail = new MailTemplate();
                if (id > 0)
                {
                    detail = _marketingService.Marketing_GetDetail(id);
                }
                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult MarketingManage(MailTemplate detail)
        {
            CustomJsonResult result = new CustomJsonResult();
            detail.CreateBy = CurrentUserId();
            try
            {
                if (detail.Id < 1) // create
                {
                    _marketingService.Marketing_Insert(detail);
                }
                else // update
                {
                    _marketingService.Marketing_Update(detail);
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult DeleteMarketing(int[] arrId)
        {
            CustomJsonResult result = new CustomJsonResult();

            try
            {
                if (_marketingService.Manage_Delete_MarketingById(arrId) <= 0)
                {
                    throw new Exception("delete error");
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        #endregion

        #region Approve eth
        public ActionResult ApproveEth()
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            return View();
        }

        [HttpPost]
        public ActionResult ApproveEthManage(int pageIndex, string orderClause, int status)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 15;
            try
            {
                int type = 1;
                if (status == 0)
                {
                    status = 1;
                    type = 0;
                }
                string whereClause = string.Format(" and B.Status={0} ", status);

                var lst = _userService.PayProfitDaily_List(
                    pageIndex,
                    pageSize,
                    out total,
                    whereClause);

                DateTime now = DateTime.Now;
                int hour = _userService.GetSettingByKey<int>("Auto.Processing.ETH", 1);
                List<WithdrawETH> response = new List<WithdrawETH>();
                foreach (WithdrawETH item in lst)
                {
                    var createPay = item.CreatePay.AddHours(hour);
                    if (type == 0)
                    {
                        if (now > createPay)
                        {
                            total = 0;
                            continue;
                        }
                    }
                    else
                    {
                        if (now < createPay)
                        {
                            item.Status = 0;
                        }
                    }
                    response.Add(item);
                }

                result.Result = response;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult WithdrawTranfer(int id)
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            var tranfer = _userService.PayProfitDaily_Get(id);
            if (tranfer == null)
            {
                return Json(-1);
            }

            int result = _userService.Withdraw_Update_Tranfer_Status(id, 2); //2: update processing
            if (result > 0)
            {
                //xu ly code tranfer o day
                bool isSuccess = TranferETH(tranfer.WalletETH, tranfer.Amount);
                if (isSuccess)
                {
                    result = _userService.Withdraw_Update_Tranfer_Status(id, 3); //3: update success
                }
                else
                {
                    _userService.Withdraw_Update_Tranfer_Status(id, 4); //4: fail
                    result = -1;
                }
            }
            return Json(result);
        }
        #endregion

        private bool TranferETH(string address, decimal amount)
        {
            return true;
        }

        #region Approve withdraw
        public ActionResult Withdraw()
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            return View();
        }

        [HttpPost]
        public ActionResult WithdrawManage(int pageIndex, string orderClause, string username, int status)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 15;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(username))
                {
                    whereClause += string.Format("and (U.Username like '{0}%' OR U.Email like '{0}%')", username);
                }
                if (status > 0)
                {
                    whereClause += string.Format("and B.Status = {0}", status);
                }

                var lst = _userService.Admin_WithdrawManage_List(
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
        public ActionResult WithdrawStatus(int id, int status, string hash)
        {
            int userID = CurrentUserId();
            if (userID < 0)
                return Redirect("/login");

            int result = _userService.Withdraw_UpdateStatus(id, status, userID, DateTime.Now, hash);
            if (result == 1)
            {
                var dataBonus = _userService.GetBonusById(id);
                if (dataBonus != null)
                {
                    using (var db = new CoreExchangeDB())
                    {
                        var withdrawItem = db.WithdrawEntitys.Where(p=>p.Id== id).FirstOrDefault();
                        if (withdrawItem!=null)
                        {
                            var bonus = _helper.FormatNumber((dataBonus.AmountSet - dataBonus.Fee));
                            //string type = ((MethodPayment)dataBonus.ToType).ToString(); //dataBonus.ToType == (int)MethodPayment.BTC ? "BTC" : "ETH";
                            string url = @"https://fortrex.io/withdraw";
                            //code send mail here
                            string fromTypeName = Enum.GetName(typeof(MethodPayment), withdrawItem.FromType);
                            string body = "";
                            body += "<br/>You've successfully withdrawn " + HelperCommon.NumberFormat(dataBonus.AmountSet) +" "+ fromTypeName;
                            body += string.Format("<br/>Your withdrawal address is {0}, txid is {1}", dataBonus.Transaction, dataBonus.HashCode);
                            body += "<br/>If you don't recognize this activity, please contact us immediately.";
                            body += "<br/>Fortrex Exchange";
                            //code send mail here

                            string template = "";

                            var sr = new StreamReader(Server.MapPath("/Content/") + "withdraw-confirmation.html");
                            template = sr.ReadToEnd();
                            template = template.Replace("{titletop}", string.Format("Hello {0}, <br/>", dataBonus.Username));
                            template = template.Replace("{titlecontent}", "");
                            template = template.Replace("{bodycontent}", body);
                            template = template.Replace("{linkaction}", url);
                            template = template.Replace("{messagebutton}", "Congratulations");
                            var mail = new Email
                            {
                                //                        Title = string.Format("[Eq Option] - Congratulations! You have successfully withdraw ${0} to address  {1}", bonus, dataBonus.Transaction),
                                Title = "Withdrawal Successful",
                                Body = template,
                                EmailTo = dataBonus.Email
                            };
                            _userService.SendMail(mail);
                        }
                    }
                    
                }
            }
            return Json(result);
        }
        #endregion

        #region for manager
        [HttpPost]
        public ActionResult UserSearch(string username)
        {
            try
            {
                UserSearch data = new UserSearch();
                var user = _userService.User_GetByUsername(username);
                var userData = _packagesService.Dasboarch(user.Id);
                userData.TotalNetwork = _packagesService.T_TreeData_GetTotalUserByParent(user.Id);
                data.Dasboarch = userData;;
                data.Target = HelperCommon.CalculatorData(userData.TotalBranchLeft, userData.TotalBranchRight, userData.MaxInvest, userData.Reinvestment, userData.MoneyBTC);

                return Json(data);
            }
            catch
            {
                return Json(null);
            }
        }

        [HttpPost]
        public ActionResult TransactionList(int pageIndex, int pageSize, int type = -1, string from_date = "", string to_date = "", string username = "")
        {
            var result = new CustomJsonResult();
            int total = 0;
            try
            {
                var userData = _userService.User_GetByUsername(username);
                if (userData != null)
                {
                    string whereClause = string.Format("and A.UserId = {0}", userData.Id);
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

                    result.Result = lst;
                    result.Optional = total;
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }

        [HttpPost]
        public ActionResult InvestmentHistory(int pageIndex, string orderClause, string username)
        {
            var result = new CustomJsonResult();
            if (!string.IsNullOrWhiteSpace(username))
            {
                var user = _userService.User_GetByUsername(username);
                int total = 0;
                int pageSize = 5;
                try
                {
                    string whereClause = string.Format("and A.UserId = {0}", user.Id);

                    var lst = _packagesService.Investment_List(
                        pageIndex,
                        pageSize,
                        out total,
                        whereClause);

                    lst = lst.Select(x => new InvestmentList
                    {
                        Id = x.Id,
                        _createOn = x.CreateOn.ToString("yyyy-MM-dd HH:mm"),
                        _startProfitDate = x.StartProfitDate.ToString("yyyy-MM-dd HH:mm"),
                        _invested = _helper.FormatNumber(x.Invested),
                        _shareTotal = _helper.FormatNumber(x.ShareTotal),
                    }).ToList();

                    result.Result = lst;
                    result.Optional = total;
                }
                catch (Exception ex)
                {
                    result.Message = ex.Message;
                }
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult WithdrawList(int pageIndex, string orderClause, string username)
        {
            var result = new CustomJsonResult();
            if (!string.IsNullOrEmpty(username))
            {
                int total = 0;
                int pageSize = 500;
                try

                {
                    var user = _userService.User_GetByUsername(username);
                    string whereClause = string.Empty;
                    if (!string.IsNullOrEmpty(user.Code))
                    {
                        whereClause += string.Format("and U.Code = '{0}'", user.Code);
                    }
                    else
                    {
                        result.Message = "Not found.";
                        return Json(result);
                    }

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
                        AmountSet = x.AmountSet,
                        Fee = x.Fee,
                        AmountGet = x.AmountGet,
                        StatusName = ((WithdrawStatus)x.Status).ToString(),
                        Status = x.Status,
                        CreateDate = x.CreateDate,
                        ApproveName = x.ApproveName,
                        ApproveDate = x.ApproveDate,
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
            else
            {
                return Json(result);
            }
        }

        [HttpPost]
        public ActionResult ListUserKYC(int pageIndex, string orderClause, string username)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 20;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(username))
                {
                    whereClause += string.Format("and (U.Username like '{0}%' OR U.Email like '{0}%')", username);
                }

                var lst = _userService.UserData_List_KYC(
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

        #region Manage Account
        public ActionResult ManageAccount()
        {
            return View();
        }

        [HttpPost]
        public JsonResult ManageAccount(int level, string from_date, string to_date, int method, string name)
        {
            try
            {
                if (string.IsNullOrEmpty(name))
                {
                    return Json("");
                }
                var dataUser = _userService.User_GetByUsername(name);
                if (dataUser == null)
                {
                    dataUser = _userService.User_GetByEmail(name);
                }
                if (dataUser == null)
                {
                    return Json("");
                }
                int uid = dataUser.Id;
                var dataList = _treeService.MUser_GetListUserByParent_V2(uid, 15).Where(p => p.Status == method || method == -1).ToList();
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
                decimal totalInvest = 0;
                var respone = new ResponseManageAccount();
                if (dataList != null && dataList.Count > 0)
                {
                    if (level > 0)
                    {
                        dataList = dataList.Where(x => x.Level == level).ToList();
                    }
                    dataList = dataList.Where(x => x.CreateOn >= _from_time && x.CreateOn <= _to_time).ToList();
                    if (dataList.Count > 0)
                    {
                        string html = string.Empty;
                        int i = 1;
                        foreach (TreeData da in dataList)
                        {
                            List<string> paskagesList = new List<string>();
                            html += "<tr>";
                            html += "<td>" + i.ToString() + "</td>";
                            html += "<td>" + da.Username + "</td>";
                            html += "<td>" + da.FullName + "</td>";
                            html += "<td>" + da.Wallet + "</td>";

                            if (da.CreateOn.HasValue)
                            {
                                string typeInvest = "";
                                if (da.Status == 1)
                                {
                                    typeInvest += " BTC";
                                }
                                else if (da.Status == 3)
                                {
                                    typeInvest += " USD";
                                }
                                else if (da.Status == 4)
                                {
                                    typeInvest += " USDT";
                                }
                                html += "<td>" + _helper.FormatNumber(da.Balance) + typeInvest + " </td>";
                                html += "<td>" + da.CreateOn.Value.ToString("yyyy-MM-dd HH:mm") + "</td>";
                                if (da.IsTransferXRP)
                                {
                                    html += "<td><div style=\"color:green;\">Success</div></td>";
                                }
                                else
                                {
                                    html += "<td><div style=\"color:red;cursor:pointer;\" id=\"tranfer-" + da.Id + "\" onclick=\"tranfer_xrp(" + da.Id + ")\">tranfer-xrp</div></td>";
                                }
                                totalInvest += da.Balance;
                            }
                            else
                            {
                                html += "<td>--</td>";
                                html += "<td>--</td>";
                                html += "<td>--</td>";
                            }
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
            catch (Exception ex)
            {
                return Json(ex.Message);
            }


        }
        #endregion

        #region DashBoards Income
        public ActionResult Dashboard()
        {
            var result = _userService.ManageDasboard_Detail();
            ViewBag.TotalInvestment = _helper.FormatNumber(result.TotalInvestment);
            ViewBag.TotalWithdrawn = _helper.FormatNumber(result.TotalWithdrawn);
            ViewBag.TotalDeposit = _helper.FormatNumber(result.TotalDeposit);
            ViewBag.TotalCoin = _helper.FormatNumber(result.TotalCoin);
            return View();
        }
        [HttpPost]
        public ActionResult Dashboard(int type = 7)
        {

            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 2000;
            try
            {
                string whereClause = string.Format("and A.Type = {0}", type);

                var lst = _userService.Admin_HistoryTransaction_List(
                    0,
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

        [OverrideAuthorization()]
        [Authorize(Roles = "SUPPERADMIN")]
        public ActionResult Sys()
        {
            var data = _userService.System_GetTool();
            return View(data);
        }

        [OverrideAuthorization()]
        [Authorize(Roles = "STAFF")]
        public ActionResult CoinTransaction()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CoinTransaction(int pageIndex, string orderClause, string status)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 10;
            try
            {
                string whereClause = string.Empty;
                if (!string.IsNullOrEmpty(status))
                {
                    whereClause += string.Format("and T.Status = '{0}'", status);
                }

                var lst = _userService.Admin_CoinTransactionList(
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
        public ActionResult CoinApprove(string addressWallet, string transactionId, int methodPayment)
        {
            int rel = _userService.Admin_CoinApprove(addressWallet, transactionId, methodPayment);
            return Json(rel);
        }

        public ActionResult QaNote()
        {
            var data = _userService.AQ_GetTotal();
            ViewBag.QATotal = data;
            return View(new QANote());
        }

        [HttpPost]
        public ActionResult ListQaNote(int pageIndex, string orderClause)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 200;
            try
            {
                string whereClause = string.Empty;
                var lst = _userService.Manage_QANote_GetAll(
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
        public ActionResult GetDetailNote(int id)
        {
            var result = new CustomJsonResult();
            try
            {
                var detail = new QANote();

                if (id != -1)
                {
                    detail = _userService.Manage_QANote_GetById(id);
                }
                else
                {
                    detail.Id = -1;
                }

                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        [HttpPost]
        public ActionResult ManageQANote(QANote detail)
        {
            var result = new CustomJsonResult();
            try
            {
                if (detail.Id == -1) // create
                {
                    detail.UserId = CurrentUserId();
                    if (_userService.Manage_QANote_Insert(detail) <= 0)
                    {
                        result.Message = "Create fail";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }

        public ActionResult Ticket()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Ticket_Update(int id, string messages)
        {
            var curentUser = GetCurrentUser();
            int rel = _userService.Ticket_Update(id, "Eq Option Team", messages);
            return Json(rel);
        }

        #region total volume buy-sell
        [HttpPost]
        public JsonResult Totalvolumebuysell()
        {
            var result = _userService.Totalvolumebuysells();
            return Json(result);
        }
        [HttpPost]
        public JsonResult Random_Orders_WinLose_Update(string pairname,bool isTypeRandom)
        {
            var result = _packagesService.Random_Orders_WinLose_Update(pairname, isTypeRandom,true);
            return Json(result);
        }
        #endregion

        #region Wallet manager
        public ActionResult WalletUser()
        {

            return View(new WalletAddressTemplate());
        }
        [HttpPost]
        public ActionResult WalletUserDetail(int id)
        {
            var result = new CustomJsonResult();

            try
            {
                var detail = new WalletAddressTemplate();
                if (id > 0)
                {
                    using (var _db=new CoreExchangeDB())
                    {
                        var userdata = _db.User_WalletAddress.Where(p => p.UserId == id)
                            .Select(s => new WalletAddressTemplate
                            {
                                BonusCommission = s.BonusCommission,
                                BonusLucky = s.BonusLucky,
                                BonusSale = s.BonusSale,
                                Userid = s.UserId,
                                MasterIB = s.MasterIB,
                                MaxInvest = s.MaxInvest,
                                MoneyUSD = s.MoneyUSD,
                                MoneyDemo = s.MoneyDemo ?? 0,
                                TotalBonus = s.TotalBonus,
                                WalletStocks = s.WalletStocks,
                                LevelId = s.LevelId
                            })
                            .FirstOrDefault();
                             detail = userdata;
                    }
                   
                }
                result.Result = detail;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        [HttpPost]
        public ActionResult WalletUserList(int pageIndex, string orderClause)
        {
            var result = new CustomJsonResult();
            int total = 0;
            int pageSize = 20;
            try
            {
                var users = new List<WalletAddressTemplate>();
                using (var _db = new CoreExchangeDB())
                {
                    users = _db.User_WalletAddress
                        .Join(
                        _db.MUsers.Where(p => p.Username.ToLower().Equals(orderClause.ToLower()) || string.IsNullOrEmpty(orderClause)),
                        s => s.UserId,
                        u => u.Id,
                        (s,u) => new WalletAddressTemplate
                        {
                            Username = u.Username,
                            BonusCommission = s.BonusCommission,
                            BonusLucky = s.BonusLucky,
                            BonusSale = s.BonusSale,
                            Userid = s.UserId,
                            MasterIB = s.MasterIB,
                            MaxInvest = s.MaxInvest,
                            MoneyUSD = s.MoneyUSD,
                            MoneyDemo = s.MoneyDemo ?? 0,
                            TotalBonus = s.TotalBonus,
                            WalletStocks = s.WalletStocks,
                            LevelId = s.LevelId
                        }
                        )
                        .OrderByDescending(o => o.Userid)
                        .Skip(pageIndex).Take(pageSize).ToList();
                    
                }
                result.Result = users;
                result.Optional = total;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }
            return Json(result);
        }
        [HttpPost]
        public ActionResult WalletUserManage(WalletAddressTemplate detail)
        {
            CustomJsonResult result = new CustomJsonResult();
           
            try
            {
                if (detail.Userid > 0) // update
                {
                    using (var _db = new CoreExchangeDB())
                    {
                        var user = _db.User_WalletAddress.Where(p => p.UserId == detail.Userid).FirstOrDefault();
                        if (user!=null)
                        {
                            user.TotalBonus = detail.TotalBonus;
                            user.MoneyUSD = detail.MoneyUSD;
                            user.MoneyDemo = detail.MoneyDemo;
                            user.MasterIB = detail.MasterIB;
                            user.LevelId = detail.LevelId;
                            user.MaxInvest = detail.MaxInvest;
                            user.BonusLucky = detail.BonusLucky;
                            user.BonusSale = detail.BonusSale;
                            _db.Entry(user).State = System.Data.Entity.EntityState.Modified;
                            _db.SaveChanges();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
            }

            return Json(result);
        }
        #endregion
    }
}