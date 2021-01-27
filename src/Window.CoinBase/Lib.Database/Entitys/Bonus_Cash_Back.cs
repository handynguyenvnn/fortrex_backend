namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Bonus_Cash_Back
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Bonus { get; set; }

        public int Type { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
