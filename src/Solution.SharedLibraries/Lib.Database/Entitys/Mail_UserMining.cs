namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Mail_UserMining
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public decimal Bonus { get; set; }

        public int Status { get; set; }

        public DateTime CreateOn { get; set; }

        public DateTime NextTimeOn { get; set; }

        public bool IsFinish { get; set; }
    }
}
