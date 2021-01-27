using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class Ticket
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Messages { get; set; }
        public string ReplyBy { get; set; }
        public string ReplyMessages { get; set; }
        public DateTime? CreateAt { get; set; }
        public DateTime? ModifyData { get; set; }
    }
}
