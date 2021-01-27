namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BonusSale")]
    public partial class BonusSale
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Bonus { get; set; }

        public DateTime CreateOn { get; set; }

        public bool IsActive { get; set; }

        public int RankId { get; set; }
    }
}
