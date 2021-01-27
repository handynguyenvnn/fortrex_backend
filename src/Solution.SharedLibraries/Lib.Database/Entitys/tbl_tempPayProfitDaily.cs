namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_tempPayProfitDaily
    {
        public int Id { get; set; }

        public int Userid { get; set; }

        public int Status { get; set; }

        [Required]
        [StringLength(128)]
        public string WalletETH { get; set; }

        public DateTime? CreatePay { get; set; }

        public decimal? Amount { get; set; }

        public decimal? TotalInvest { get; set; }

        public decimal? AmountBeforeaDay { get; set; }

        [StringLength(256)]
        public string txhash { get; set; }

        public bool? ApprovedbyAdmin { get; set; }

        [StringLength(50)]
        public string ByUser { get; set; }

        [StringLength(500)]
        public string Descriptions { get; set; }

        public decimal? AmountUSD { get; set; }
    }
}
