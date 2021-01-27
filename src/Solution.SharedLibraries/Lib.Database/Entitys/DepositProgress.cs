namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DepositProgress")]
    public partial class DepositProgress
    {
        [Column(TypeName = "numeric")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        public int UserId { get; set; }

        [StringLength(20)]
        public string WalletType { get; set; }

        public decimal CoinValue { get; set; }

        public decimal AmountUSD { get; set; }

        [StringLength(128)]
        public string WalletAddress { get; set; }

        public string TxHash { get; set; }

        public int? FillConfirm { get; set; }

        public int? Confirmations { get; set; }

        public bool? Success { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateAt { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CompletedAt { get; set; }

        public int? timestamp { get; set; }

        [StringLength(128)]
        public string FromAddress { get; set; }
    }
}
