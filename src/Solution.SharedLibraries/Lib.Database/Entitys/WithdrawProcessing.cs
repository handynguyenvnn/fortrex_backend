namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WithdrawProcessing")]
    public partial class WithdrawProcessing
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int FromType { get; set; }

        public int ToType { get; set; }

        public decimal AmountSet { get; set; }

        public decimal Fee { get; set; }

        public decimal AmountGet { get; set; }

        public int Status { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? ApproveBy { get; set; }

        public DateTime? ApproveDate { get; set; }

        [StringLength(128)]
        public string Transaction { get; set; }

        [StringLength(128)]
        public string HashCode { get; set; }
    }
}
