using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class TransactionSession
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpireDate { get; set; }
        public DateTime CreateDate { get; set; }
        public int ReferentId { get; set; }
        public int Status { get; set; }
        public string TypeTransaction { get; set; }
    }
}
