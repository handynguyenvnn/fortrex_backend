namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("BuyCoin")]
    public partial class BuyCoin
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal NumberCoin { get; set; }

        public decimal OriginUSD { get; set; }

        public decimal PriceUSD { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int Status { get; set; }

        [StringLength(128)]
        public string Transaction { get; set; }

        public int? ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

        public int? MethodPaymentId { get; set; }
    }
}
