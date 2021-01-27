namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_ExtensionEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int UserId { get; set; }

        [StringLength(128)]
        public string Firstname { get; set; }

        [StringLength(128)]
        public string Lastname { get; set; }

        public int? PhoneNatural { get; set; }

        [StringLength(30)]
        public string PhoneNumber { get; set; }

        [StringLength(10)]
        public string Country { get; set; }

        [StringLength(20)]
        public string IdentificationType { get; set; }

        [StringLength(50)]
        public string IdentificationNumber { get; set; }

        [Required]
        [StringLength(128)]
        public string FontSideUrl { get; set; }

        [Required]
        [StringLength(128)]
        public string BackSideUrl { get; set; }

        [Required]
        [StringLength(128)]
        public string SelfieUrl { get; set; }

        public int Status { get; set; }

        public DateTime CreateOn { get; set; }
    }
}
