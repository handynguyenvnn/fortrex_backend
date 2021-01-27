using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Models
{
    public class UserProfileModel
    {
        public string Code { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
    }
    public class FACodeModel
    {
        public string UserUniqueKey { get; set; }
        public string SetupCode { get; set; }
        public string CodeDigit { get; set; }
    }

    public class UserProfile
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public string Country { get; set; }
    }

    
}
