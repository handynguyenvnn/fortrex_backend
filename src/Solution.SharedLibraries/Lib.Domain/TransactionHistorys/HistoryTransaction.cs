using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.TransactionHistorys
{
    public class HistoryTransaction
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public string StrAmount { get; set; }
        public decimal DailyRoi { get; set; }
        public int FromUserId { get; set; }
        public string FromUser { get; set; }
        public string Description { get; set; }
        public int Type { get; set; }
        public string TypeName { get; set; }
        public int Status { get; set; }
        public string StatusName { get; set; }
        public DateTime CreateOn { get; set; }
        public DateTime? UpdateOn { get; set; }
        public string CoinBaseTransactionId { get; set; }
        public string ByUserName { get; set; }
        public string StrCreateOn {get { return CreateOn.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
    [DataContract()]
    public class ResponseHistoryTransaction
    {
        [DataMember(Name = "Id", Order = 0)]
        public int Id { get; set; }
        [DataMember(Name = "Amount", Order = 1)]
        public decimal Amount { get; set; }
        [DataMember(Name = "StrAmount", Order = 2)]
        public string StrAmount { get; set; }

        [DataMember(Name = "FromUser", Order = 3)]
        public string FromUser { get; set; }

        [DataMember(Name = "Description", Order = 4)]
        public string Description { get; set; }

        [DataMember(Name = "Type", Order = 5)]
        public int Type { get; set; }
        //[DataMember(Name = "TypeName", Order = 6)]
        public string TypeName { get; set; }
        [DataMember(Name = "Status", Order = 7)]
        public int Status { get; set; }
        [DataMember(Name = "StatusName", Order = 8)]
        public string StatusName { get; set; }

        public DateTime CreateOn { get; set; }

        [DataMember(Name = "StrCreateOn", Order = 9)]
        public string StrCreateOn
        {
            get { return CreateOn.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}
