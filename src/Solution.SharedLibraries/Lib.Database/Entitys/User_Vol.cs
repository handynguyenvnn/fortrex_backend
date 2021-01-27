namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Vol
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal TotalTrade { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime UpdateOn { get; set; }
    }
}
