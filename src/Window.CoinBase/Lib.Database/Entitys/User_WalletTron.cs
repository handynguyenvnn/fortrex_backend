namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_WalletTron
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Bonus73Percent { get; set; }

        public decimal Bonus20Percent { get; set; }

        public decimal Bonus7Percent { get; set; }

        public DateTime UpdateOn { get; set; }

        public int PackageId { get; set; }

        public bool? IsReinvestment { get; set; }
    }
}
