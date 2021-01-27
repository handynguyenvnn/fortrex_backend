namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Users_Marketing_Bonus
    {
        public int id { get; set; }

        [StringLength(50)]
        public string username { get; set; }

        [StringLength(50)]
        public string email { get; set; }

        [StringLength(10)]
        public string type { get; set; }

        [Column(TypeName = "ntext")]
        public string description { get; set; }
    }
}
