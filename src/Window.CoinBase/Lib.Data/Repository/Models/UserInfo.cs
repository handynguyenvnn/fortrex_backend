using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
    }

    public class ViewModelUsers
    {


        public int ReferralId { get; set; }
        public string ReferralCode { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordFormatId { get; set; }
        public string PasswordSaft { get; set; }
    }

    public class ParameterViewModelUsers
    {


        public int ReferralId { get; set; }
        public string ReferralCode { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordFormatId { get; set; }
        public string PasswordSaft { get; set; }
    }
}
