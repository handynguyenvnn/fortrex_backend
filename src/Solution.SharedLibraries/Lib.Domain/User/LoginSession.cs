using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public class LoginSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}