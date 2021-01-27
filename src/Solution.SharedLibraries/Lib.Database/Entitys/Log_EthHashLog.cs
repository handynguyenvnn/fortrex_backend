namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log_EthHashLog
    {
        public int Id { get; set; }

        public decimal UserId { get; set; }

        [StringLength(250)]
        public string txHash { get; set; }

        public int timestamp { get; set; }
    }
}
