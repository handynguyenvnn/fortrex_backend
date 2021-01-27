namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ContentStatic")]
    public partial class ContentStatic
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(250)]
        public string Title { get; set; }

        [Required]
        [StringLength(500)]
        public string Meg { get; set; }

        public DateTime CreateDate { get; set; }

        public DateTime? ShowDate { get; set; }

        public DateTime? HideDate { get; set; }
    }
}
