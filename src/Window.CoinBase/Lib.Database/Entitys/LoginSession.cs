namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LoginSession")]
    public partial class LoginSession
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [Required]
        [StringLength(50)]
        public string Token { get; set; }

        public bool? IsActive { get; set; }

        public DateTime? ExpireDate { get; set; }

        public DateTime? CreateDate { get; set; }
    }
}
