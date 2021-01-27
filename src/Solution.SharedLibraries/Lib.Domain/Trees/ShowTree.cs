using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Trees
{
    public class ShowTree
    {
        public ShowTree()
        {
            IsDraftUser = true;
        }
        public int Node { get; set; }
        public int UserId { get; set; }
        public int? ParentId { get; set; }
        public int ManageId { get; set; }
        public int Level { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime CreateOn { get; set; }
        public bool IsActive { get; set; }
        public bool IsLock { get; set; }
        public bool IsDraftUser { get; set; }
        public decimal Balance { get; set; }
        public int? PackageId { get; set; }
        public bool IsInvest
        {
            get {
                if (PackageId.HasValue && PackageId.Value > 0)
                    return true;
                else
                    return false;
            }
        }
    }
}
