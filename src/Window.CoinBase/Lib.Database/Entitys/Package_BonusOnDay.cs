namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Package_BonusOnDay
    {
        [Key]
        public DateTime CreateOn { get; set; }

        public decimal BonusPercent { get; set; }
    }
}
