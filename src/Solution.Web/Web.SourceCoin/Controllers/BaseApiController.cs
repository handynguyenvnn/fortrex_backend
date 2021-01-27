using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Http;
using Lib.Domain.User;
using Lib.Service.Service.User;

namespace Web.SourceCoin.Controllers
{
    public class BaseApiController : ApiController
    {
        public const string HEADER_USER_TOKEN = "Fortrex-Option-User-Token";
        protected readonly IUserService _userService;
        public BaseApiController(IUserService userService)
        {
            _userService = userService;
        }
        public int? GetCurrentUId()
        {
            int? currentUserId = null;
            var headers = HttpContext.Current.Request.Headers;
            if (headers != null)
            {
                string userToken = headers[HEADER_USER_TOKEN];
                if (!string.IsNullOrEmpty(userToken))
                {
                    int uid = _userService.Token_GetUserIdByToken(userToken);
                    if (uid > 0)
                        currentUserId = uid;
                }
            }
            return currentUserId;
        }
        public MUser GetCurrentUser()
        {
            try
            {
                var userId = GetCurrentUId();
                if (userId != null)
                {
                    var dataUser = _userService.User_GetByUserId((int)userId);
                    if (dataUser != null)
                    {
                        if (dataUser.IsLock == true || dataUser.IsDelete == true)
                        {
                            return null;
                        }
                        return dataUser;
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
    }
}