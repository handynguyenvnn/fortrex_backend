namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("SessionLogin")]
    public partial class SessionLogin
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(200)]
        public string Token { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime CreateOn { get; set; }

        public bool IsActive { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ExpireDate { get; set; }
    }
}
