namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("HistoryTransaction")]
    public partial class HistoryTransactionEntity
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal? Amount { get; set; }

        public int? FromUserId { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int Type { get; set; }

        public int Status { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? UpdateOn { get; set; }

        [StringLength(128)]
        public string CoinBaseTransactionId { get; set; }

        public decimal? DailyRoi { get; set; }
    }
}
