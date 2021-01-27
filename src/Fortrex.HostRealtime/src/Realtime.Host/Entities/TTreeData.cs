using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TTreeData
    {
        public int Id { get; set; }
        public int Node { get; set; }
        public int ParentId { get; set; }
        public int UserId { get; set; }
        public int Level { get; set; }
        public int ManageId { get; set; }
        public DateTime? CreateOn { get; set; }
    }
}
