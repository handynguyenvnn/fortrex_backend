using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Lib.Domain.User
{
    //public class UsersAffiliates
    //{
    //    public string Username { get; set; }
    //    public string Email { get; set; }
    //    public bool IsActive { get; set; }
    //    public string FullName { get; set; }
    //    public string Phone { get; set; }
    //    public string Country { get; set; }
    //    public int? ReferralId { get; set; }
    //    public int Level { get; set; }
    //    public decimal TotalTrading { get; set; }
    //    public string strTotalTrading { get; set; }
    //    public string StrCreateOn { get; set; }
    //    public string UserLevel { get { return "F" + Level; } }
    //}
    [DataContract()]
    public class UsersAffiliates
    {
        public UsersAffiliates()
        {
            CommissionRate = 0;
            Amount = 0;
        }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
        public int? ReferralId { get; set; }
        [DataMember(Name = "level", Order = 3)]
        public string UserLevel { get { return "F" + Level; } }
        [DataMember(Name = "username", Order = 2)]
        public string Username { get; set; }
        [DataMember(Name = "email", Order = 4)]
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        [DataMember(Name = "time", Order = 1)]
        public string StrCreateOn { get; set; }
        public DateTime CreateOn { get; set; }
        [DataMember(Name = "tradingVolume", Order = 4)]
        public string strTotalTrading { get; set; }

        public decimal TotalTrading { get; set; }
        [DataMember(Name = "com.Rate", Order = 5)]
        public decimal CommissionRate { get; set; }
        [DataMember(Name = "amount", Order = 6)]
        public decimal Amount { get; set; }
    }
}