using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages
{
    public class InvestmentList
    {
        public int Id { get; set; }
        public decimal Invested { get; set; }     
        public decimal ShareTotal { get; set; }
        public decimal SharePercent { get; set; }
        public DateTime CreateOn { get; set; }    
        public DateTime StartProfitDate { get; set; }
        public string _invested { get; set; }
        public string _shareTotal { get; set; }
        public string _createOn { get; set; }
        public string _sharePercent { get; set; }
        public string _startProfitDate { get; set; }
        public decimal TempProfit { get; set; }
        public decimal TempStock { get; set; }
        public string _receivedProfit { get; set; }
        public string _receivedStock { get; set; }
        public bool IsActive { get; set; }
        public string _action { get; set; }
        public bool IsProfit { get; set; }
    }
    [DataContract()]
    public class AffiliateTradingList
    {
        public AffiliateTradingList()
        {
            CommissionRate = 0;
            Amount = 0;
        }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        [DataMember(Name = "level", Order = 3)]
        public int Level { get; set; }
        [DataMember(Name = "username", Order = 2)]
        public string Username { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string FullName { get; set; }
        [DataMember(Name = "time", Order = 1)]
        public string StrCreateOn
        {
            get { return CreateOn.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
        public DateTime CreateOn { get; set; }
        [DataMember(Name = "packageValue", Order = 4)]
        public decimal TotalTrading { get; set; }
        [DataMember(Name = "com.Rate", Order = 5)]
        public decimal CommissionRate { get; set; }
        [DataMember(Name = "amount", Order = 6)]
        public decimal Amount { get; set; }
    }

    public class AffiliateMember
    {
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
    }

    public class AffiliateAgencyCom
    {
        public int UserId { get; set; }
        public int TotalAgency { get; set; }
        public int Level { get; set; }
    }
}
