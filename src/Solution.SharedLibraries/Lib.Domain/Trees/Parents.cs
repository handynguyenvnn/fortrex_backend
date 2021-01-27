using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Trees
{
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
}
