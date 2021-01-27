namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_Transfer
    {
        public int Id { get; set; }

        public int FromUid { get; set; }

        public int ToUid { get; set; }

        public decimal Amount { get; set; }

        public int Status { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime? ApplyOn { get; set; }

        public decimal ResFee { get; set; }

        public decimal ResAmount { get; set; }

        [StringLength(5)]
        public string Type { get; set; }
    }
}
