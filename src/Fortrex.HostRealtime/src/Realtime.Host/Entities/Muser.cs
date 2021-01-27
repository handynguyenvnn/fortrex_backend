using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Muser //: IdentityUser
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int? PasswordFormatId { get; set; }
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
        public int? ReferralId { get; set; }
        public string WalletCoin { get; set; }
        public string WalletEth { get; set; }
        public string Fa2code { get; set; }
        public string Fa3code { get; set; }
        public DateTime? LastActivityDate { get; set; }
        public DateTime? UpdateOn { get; set; }
        public DateTime CreateOn { get; set; }
        public string CountryId { get; set; }
        public int? TotalLoginFaild { get; set; }
        public string CityId { get; set; }
        public string Node { get; set; }
        public string WalletXrp { get; set; }
        public string WalletBch { get; set; }
        public string WalletBnct { get; set; }
    }
}
