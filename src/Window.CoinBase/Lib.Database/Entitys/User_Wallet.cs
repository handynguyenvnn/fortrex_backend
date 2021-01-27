namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Wallet
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(128)]
        public string WalletAddress { get; set; }

        public decimal Amount { get; set; }

        public decimal LastAmount { get; set; }

        public string WalletType { get; set; }
    }
}
