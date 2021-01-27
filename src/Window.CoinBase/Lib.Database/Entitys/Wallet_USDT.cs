namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Wallet_USDT
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string CoinName { get; set; }

        [StringLength(10)]
        public string CoinSymbol { get; set; }

        [StringLength(128)]
        public string CoinContract { get; set; }

        [StringLength(128)]
        public string CoinAddress { get; set; }

        [StringLength(255)]
        public string CoinPrivateKey { get; set; }

        [StringLength(255)]
        public string CoinPublicKey { get; set; }
    }
}
