using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.User
{
    public class TicketEntity
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Subject { get; set; }
        public string Messages { get; set; }
        public string ReplyBy { get; set; }
        public string ReplyMessages { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? ModifyData { get; set; }
        public string CreateAtstr { get; set; }
        public string ModifyDatastr { get; set; }
    }
}
