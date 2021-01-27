namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TradeHistoryTransaction")]
    public partial class TradeHistoryTransactionEntitys
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public decimal Id { get; set; }

        [StringLength(50)]
        public string BuyExchange { get; set; }

        [StringLength(50)]
        public string SellExchange { get; set; }

        public decimal? BuyPrice { get; set; }

        public decimal? SellPrice { get; set; }

        public decimal? PercentDifference { get; set; }

        [StringLength(30)]
        public string CoinName { get; set; }

        public DateTime? TradeAt { get; set; }

        [StringLength(50)]
        public string CoinPair { get; set; }

        [StringLength(128)]
        public string TransactionID { get; set; }
    }
}
