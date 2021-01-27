namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("MailAccount")]
    public partial class MailAccount
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [StringLength(250)]
        public string Email { get; set; }

        [StringLength(250)]
        public string DisplayName { get; set; }

        [StringLength(128)]
        public string Host { get; set; }

        public int? Port { get; set; }

        [Required]
        [StringLength(128)]
        public string Username { get; set; }

        [Required]
        [StringLength(128)]
        public string Password { get; set; }

        public bool? EnableSsl { get; set; }

        public bool? UseDefaultCredentials { get; set; }
    }
}
