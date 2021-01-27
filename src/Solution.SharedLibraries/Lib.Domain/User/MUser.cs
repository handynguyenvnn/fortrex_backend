using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public class MUser
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSaft { get; set; }
        public string LastIpAddress { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? LastActiveDate { get; set; }
        public bool IsActive { get; set; }
        public bool IsDelete { get; set; }
        public bool IsLock { get; set; }
        public DateTime? LastLockDate { get; set; }
        public string FullName { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public int? ReferralId { get; set; }
        public string WalletCoin { get; set; }
        public string WalletETH { get; set; }
        public string FA2Code { get; set; }
        public string FA3Code { get; set; }
        public bool IsDemo { get; set; }
        public int UserLevel { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string StrCreateOn { get; set; }
    }
    public class ViewModelUsers
    {

        public int ReferralId { get; set; }
        public string UserCode { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordComfirm { get; set; }
    }
}