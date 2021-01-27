namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Packages_Bonus_Transaction
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreateDate { get; set; }

        public decimal Bonus { get; set; }

        public DateTime Day { get; set; }

        public int PackagesId { get; set; }

        public decimal PercentAmount { get; set; }

        public int Status { get; set; }
    }
}
