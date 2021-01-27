namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Ticket")]
    public partial class Ticket
    {
        public int Id { get; set; }

        public int? UserId { get; set; }

        [StringLength(250)]
        public string FullName { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string PhoneNumber { get; set; }

        [StringLength(150)]
        public string Subject { get; set; }

        public string Messages { get; set; }

        [StringLength(50)]
        public string ReplyBy { get; set; }

        public string ReplyMessages { get; set; }

        public DateTime? CreateAt { get; set; }

        public DateTime? ModifyData { get; set; }
    }
}
