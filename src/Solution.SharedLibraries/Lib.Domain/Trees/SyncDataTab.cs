using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Trees
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

    public enum SyncDataTabStatus
    {
        PENDING = 1
    }
}
