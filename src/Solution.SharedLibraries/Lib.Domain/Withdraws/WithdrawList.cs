using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Withdraws
{
    [DataContract()]
    public class WithdrawList
    {
        public WithdrawList()
        {
            HashCode = string.Empty;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int FromType { get; set; }
        [DataMember(Name = "from", Order = 6)]
        public string FromTypeName { get; set; }
        public int ToType { get; set; }
        [DataMember(Name = "to", Order = 6)]
        public string ToTypeName { get; set; }
        public decimal AmountSet { get; set; }
        public string strAmountSet { get; set; }
        [DataMember(Name = "amount", Order = 6)]
        public string strAmountGet { get; set; }
        [DataMember(Name = "createDate", Order = 6)]
        public string strCreateDate { get; set; }
        [DataMember(Name = "approveDate", Order = 6)]
        public string strApproveDate { get; set; }
        [DataMember(Name = "fee", Order = 6)]
        public decimal Fee { get; set; }
     
        public decimal AmountGet { get; set; }
        public int Status { get; set; }
        [DataMember(Name = "statusName", Order = 6)]
        public string StatusName { get; set; }
        public DateTime CreateDate { get; set; }
        
        public DateTime? UpdateDate { get; set; }
        public int? ApproveBy { get; set; }
        public string ApproveName { get; set; }
        public DateTime? ApproveDate { get; set; }
       
        public string Username { get; set; }
        [DataMember(Name = "addressWallet", Order = 6)]
        public string AddressWallet { get; set; }
        [DataMember(Name = "txId", Order = 6)]
        public string HashCode { get; set; }
    }
}
