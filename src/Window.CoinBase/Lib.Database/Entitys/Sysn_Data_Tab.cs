namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Sysn_Data_Tab
    {
        public long Id { get; set; }

        public int Status { get; set; }

        [Required]
        [StringLength(4000)]
        public string ExtraData { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
