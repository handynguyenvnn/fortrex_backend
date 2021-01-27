namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class CoinTransaction_Clone
    {
        public int Id { get; set; }

        public int? MethodPayment { get; set; }

        [Required]
        [StringLength(30)]
        public string Type { get; set; }

        [Required]
        [StringLength(30)]
        public string Status { get; set; }

        public decimal PriceCoin { get; set; }

        public decimal PriceUSD { get; set; }

        [StringLength(128)]
        public string AddressWallet { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime UpdateDate { get; set; }

        [Required]
        [StringLength(128)]
        public string HashCode { get; set; }

        [Required]
        [StringLength(128)]
        public string TransactionId { get; set; }
    }
}
