using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class MarketingEmail
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime? UpdateDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int CreateBy { get; set; }
        public bool IsActive { get; set; }
        public int? LastId { get; set; }
        public int Type { get; set; }
        public string Email { get; set; }
        public bool? IsTest { get; set; }
    }
}
