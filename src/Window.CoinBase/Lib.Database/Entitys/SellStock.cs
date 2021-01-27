namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SellStock")]
    public partial class SellStock
    {
        public int Id { get; set; }

        public decimal RequestAmount { get; set; }

        public decimal ResposeFee { get; set; }

        public decimal ResponseAmount { get; set; }

        public int UserId { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
