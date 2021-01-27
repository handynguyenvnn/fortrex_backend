using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class UserPairNameMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string PairName { get; set; }
    }
}
