namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Log_Status
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal BeforeTRX { get; set; }

        public decimal NowTRX { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
