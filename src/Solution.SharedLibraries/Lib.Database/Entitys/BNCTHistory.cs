namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BNCTHistory")]
    public partial class BNCTHistory
    {
        public int Id { get; set; }

        public int UId { get; set; }

        public decimal Coin { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
