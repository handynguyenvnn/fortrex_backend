namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class QA_Note
    {
        public int Id { get; set; }

        [Required]
        [StringLength(500)]
        public string Note { get; set; }

        public decimal Amount { get; set; }

        public DateTime CreateDate { get; set; }

        public int UserId { get; set; }

        public bool IsDelete { get; set; }
    }
}
