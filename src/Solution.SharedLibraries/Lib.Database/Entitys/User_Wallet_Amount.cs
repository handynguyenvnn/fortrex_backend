namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Wallet_Amount
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Amount40 { get; set; }

        public decimal Amount60 { get; set; }

        public int PackageId { get; set; }

        public bool IsTransferXRP { get; set; }

        public decimal AmountXRP { get; set; }
    }
}
