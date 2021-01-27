namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("TradePair")]
    public partial class TradePair
    {
        public int Id { get; set; }

        [StringLength(20)]
        public string PairName { get; set; }

        [StringLength(10)]
        public string Fsym { get; set; }

        [StringLength(10)]
        public string Tsym { get; set; }

        public decimal? FeeTrade { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }
    }
}
