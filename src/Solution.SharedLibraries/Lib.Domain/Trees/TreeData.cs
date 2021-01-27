using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Trees
{
    public class TreeDataItem
    {
        public decimal Invested { get; set; }
        public DateTime CreateOn { get; set; }
        public int Status { get; set; }
    }
    public class TreeData
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ParentId { get; set; }
        public int Level { get; set; }
        public string Username { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Wallet { get; set; }
        public decimal Balance { get; set; }
        public DateTime? CreateOn { get; set; }
        public int Status { get; set; }
        public List<TreeDataItem> TreeDataItem { get; set; }
        public bool IsTransferXRP { get; set; }
    }
}
