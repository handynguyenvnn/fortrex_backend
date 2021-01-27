namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("CoinList")]
    public partial class CoinList
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string CoinName { get; set; }

        [StringLength(10)]
        public string CoinSymbol { get; set; }

        [StringLength(128)]
        public string CoinContract { get; set; }

        public bool? IsActive { get; set; }

        public bool? IsDelete { get; set; }

        public decimal Decimals { get; set; }

        [StringLength(20)]
        public string TypeCoin { get; set; }
    }
}
