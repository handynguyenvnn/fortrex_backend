namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("ScheduleTask")]
    public partial class ScheduleTask
    {
        public int Id { get; set; }

        public int ProjectType { get; set; }

        [Required]
        [StringLength(350)]
        public string Name { get; set; }

        public int Seconds { get; set; }

        [Required]
        [StringLength(350)]
        public string Type { get; set; }

        public bool Enabled { get; set; }

        public DateTime? LastStartUtc { get; set; }

        public DateTime? LastEndUtc { get; set; }

        public DateTime? LastSuccessUtc { get; set; }

        public bool StopOnError { get; set; }
    }
}
