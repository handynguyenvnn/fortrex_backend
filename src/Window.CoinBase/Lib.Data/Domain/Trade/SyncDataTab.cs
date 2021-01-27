using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Domain.Trade
{
    public class SyncDataTab
    {
        public UInt64 Id { get; set; }
        public int Status { get; set; }
        public string ExtraData { get; set; }
    }

    public class UserVolData
    {
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int PackageId { get; set; }
    }

    public class Parents
    {
        public Parents()
        {
            IsLock = false;
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public int Node { get; set; }
        public int Level { get; set; }
        public decimal MaxInvest { get; set; }
        public bool IsLock { get; set; }
    }

    public class Sync_User_Branch_Balance
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal LeftAmount { get; set; }
        public decimal RightAmount { get; set; }
        public decimal LeftReset { get; set; }
        public decimal RightReset { get; set; }
        public int Status { get; set; }
        public DateTime CreateDate { get; set; }
        public int ByUid { get; set; }
        public int PackageId { get; set; }
        public decimal MaxInvest { get; set; }
    }
}
