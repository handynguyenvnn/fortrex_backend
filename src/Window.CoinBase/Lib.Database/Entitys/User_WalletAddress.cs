namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_WalletAddress
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(128)]
        public string WalletBTC { get; set; }

        [StringLength(128)]
        public string WalletETH { get; set; }

        [StringLength(128)]
        public string WalletMy { get; set; }

        public decimal MoneyBTC { get; set; }

        public decimal MoneyETH { get; set; }

        public decimal MoneyUSD { get; set; }

        public decimal MoneyGES { get; set; }
        public decimal MoneyELD { get; set; }
        public decimal MoneyBRI { get; set; }

        public decimal BonusBranch { get; set; }

        public decimal BonusLucky { get; set; }

        public decimal BonusCommission { get; set; }

        public decimal MaxInvest { get; set; }

        [StringLength(128)]
        public string WalletStocks { get; set; }

        public decimal TotalBonus { get; set; }

        public decimal BonusSale { get; set; }

        public decimal? MoneyDemo { get; set; }
    }
}
