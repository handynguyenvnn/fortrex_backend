using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.User
{
    public class UserInfo
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public int Gender { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public List<string> Roles { get; set; }
        public string Password { get; set; }
        public int PasswordFormatId { get; set; }
        public string PasswordSaft { get; set; }
        public string FA2Code { get; set; }
        public string WalletCoin { get; set; }
        public string WalletETH { get; set; }
        public int? ReferralId { get; set; }
        public int UserLevel { get; set; }
    }

    public class ResponseUserProfile
    {

        public string Username { get; set; }
        public string Code { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }
        public decimal TotalInvest { get; set; }
    }
}