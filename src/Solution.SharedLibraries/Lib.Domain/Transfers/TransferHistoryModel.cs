using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Domain.Transfers
{
    public class TransferHistoryModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }//
        public string StrAmount { get; set; }
        public string FromUser { get; set; }//
        public string Description { get; set; }//
        public int Type { get; set; }//
        public string TypeName { get; set; }//
        public int Status { get; set; }//
        public string StatusName { get; set; }//
        public DateTime CreateOn { get; set; }//
        public string ByUserName { get; set; }//
        public string StrCreateOn { get; set; }

    }

}


