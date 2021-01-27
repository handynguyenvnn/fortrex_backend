namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LogDervice")]
    public partial class LogDervice
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(20)]
        public string IPAddress { get; set; }

        [StringLength(250)]
        public string UserAgent { get; set; }

        [StringLength(20)]
        public string CreateOn { get; set; }

        [StringLength(20)]
        public string Status { get; set; }
    }
}
