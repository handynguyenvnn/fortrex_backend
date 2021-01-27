namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Package
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        public decimal PriceFrom { get; set; }

        public decimal PriceTo { get; set; }

        public decimal PercentOnDay { get; set; }

        public decimal PercentTotal { get; set; }

        public decimal PlusPercent { get; set; }

        public decimal FinishDay { get; set; }

        public int DisplayOrder { get; set; }

        public int? Status { get; set; }
    }
}
