using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class MailMarketing
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool EnableSsl { get; set; }
        public bool UseDefaultCredentials { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public int? LastId { get; set; }
        public int Type { get; set; }
        public int MarketingId { get; set; }
        public string EmailTest { get; set; }
        public bool IsTest { get; set; }
    }
}
