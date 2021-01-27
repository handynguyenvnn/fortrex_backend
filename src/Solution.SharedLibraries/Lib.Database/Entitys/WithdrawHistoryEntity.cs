namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("WithdrawHistorys")]
    public partial class WithdrawHistory
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(20)]
        public string WalletType { get; set; }

        [StringLength(128)]
        public string WalletAddress { get; set; }

        public decimal AmountSet { get; set; }

        public decimal Fee { get; set; }

        public decimal AmountGet { get; set; }

        public int Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateDate { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? UpdateDate { get; set; }

        public int? ApproveBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ApproveDate { get; set; }

        public int? FillConfirm { get; set; }

        public int? Confirmations { get; set; }

        [StringLength(128)]
        public string TxHash { get; set; }
    }
}
