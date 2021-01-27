using Lib.Domain.Trees;
using System.Collections.Generic;

namespace Web.SourceCoin.Models
{
    public class SeededTreeNodes
    {
        public int? Seed { get; set; }
        public IList<ShowTree> TreeNodes { get; set; }
        public int UserId { get; set; }
    }
}