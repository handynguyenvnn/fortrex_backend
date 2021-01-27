namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Packeges_BonusEntitys
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Invested { get; set; }

        public bool IsProfit { get; set; }

        public decimal SharePercent { get; set; }

        public decimal SharePrice { get; set; }

        public decimal ShareTotal { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime StartProfitDate { get; set; }

        public bool IsActive { get; set; }

        public DateTime ExpireDate { get; set; }

        [Required]
        [StringLength(5)]
        public string Type { get; set; }

        public decimal TempStock { get; set; }

        public decimal TempProfit { get; set; }
    }
}
