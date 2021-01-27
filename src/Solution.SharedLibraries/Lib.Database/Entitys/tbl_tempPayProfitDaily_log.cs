namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class tbl_tempPayProfitDaily_log
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Userid { get; set; }

        [StringLength(128)]
        public string WalletETH { get; set; }

        public DateTime? CreatePay { get; set; }

        public decimal? Amount { get; set; }

        public decimal? TotalInvest { get; set; }

        public decimal? AmountBeforeaDay { get; set; }
    }
}
