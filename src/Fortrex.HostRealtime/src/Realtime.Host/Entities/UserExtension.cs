using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserExtension
    {
        public int UserId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public int? PhoneNatural { get; set; }
        public string PhoneNumber { get; set; }
        public string Country { get; set; }
        public string IdentificationType { get; set; }
        public string IdentificationNumber { get; set; }
        public string FontSideUrl { get; set; }
        public string BackSideUrl { get; set; }
        public string SelfieUrl { get; set; }
        public int Status { get; set; }
        public DateTime CreateOn { get; set; }
    }
}
