namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MarketingEmail")]
    public partial class MarketingEmailEntity
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        [Required]
        [StringLength(250)]
        public string Title { get; set; }

        [Column(TypeName = "ntext")]
        public string Body { get; set; }

        public DateTime? UpdateDate { get; set; }

        public DateTime CreateDate { get; set; }

        public int CreateBy { get; set; }

        public bool IsActive { get; set; }

        public int? LastId { get; set; }

        public int Type { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        public bool? IsTest { get; set; }
    }
}
