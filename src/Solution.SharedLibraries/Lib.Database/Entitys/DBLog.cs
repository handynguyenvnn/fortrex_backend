namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("DBLog")]
    public partial class DBLog
    {
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }

        public string Message { get; set; }

        public DateTime CreateOn { get; set; }

        public int? ReferentId { get; set; }

        public int? Type { get; set; }
    }
}
