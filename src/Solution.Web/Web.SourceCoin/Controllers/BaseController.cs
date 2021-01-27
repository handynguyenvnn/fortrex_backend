using Lib.Domain.User;
using Lib.Service.Service.User;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Web.SourceCoin.Controllers
{
    public class BaseController : Controller
    {
        protected readonly IUserService _userService;
        public string cusername;
        public string cpassword;
        public BaseController(IUserService userService)
        {
            _userService = userService;
            cusername = Web.SourceCoin.Common.HelperCommon.GetCookies("Fortrex.Authen_Username", isJs: true);
            cpassword = Web.SourceCoin.Common.HelperCommon.GetCookies("Fortrex.Authen_Password", isJs: true);
        }

        public UserInfo GetCurrentUser()
        {
            try
            {

                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie != null)
                {
                    FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                    if (!string.IsNullOrEmpty(ticket.Name))
                    {
                        var dataUser = _userService.User_GetByUsername(ticket.Name);
                        if (dataUser != null)
                        {
                            if (dataUser.IsLock == true || dataUser.IsDelete == true)
                            {
                                FormsAuthentication.SignOut();
                                return null;
                            }
                            _userService.LastActivityUpdate(dataUser.Id);
                            var mInfo = new UserInfo()
                            {
                                // 
                                Id = dataUser.Id,
                                Code = dataUser.Code,
                                Username = dataUser.Username,
                                Email = dataUser.Email,
                                IsActive = dataUser.IsActive,
                                IsLock = dataUser.IsLock,
                                Password = dataUser.Password,
                                PasswordFormatId = dataUser.PasswordFormatId,
                                PasswordSaft = dataUser.PasswordSaft,
                                FullName = dataUser.FullName,
                                Phone = dataUser.Phone,
                                FA2Code = dataUser.FA2Code,
                                WalletCoin = dataUser.WalletCoin,
                                WalletETH = dataUser.WalletETH,
                                ReferralId = dataUser.ReferralId,
                                UserLevel = dataUser.UserLevel
                            };
                            return mInfo;
                        }
                    }
                }
                return null;
            }
            catch
            {
                FormsAuthentication.SignOut();
                return null;
            }
        }

        public int CurrentUserId()
        {
            var data = GetCurrentUser();
            if (data != null)
            {
                return data.Id;
            }
            return -1;
        }
    }
}
