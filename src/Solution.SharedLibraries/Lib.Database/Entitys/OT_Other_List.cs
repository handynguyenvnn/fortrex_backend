namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class OT_Other_List
    {
        public int Id { get; set; }

        [StringLength(50)]
        public string Type_Code { get; set; }

        [StringLength(50)]
        public string Code { get; set; }

        [StringLength(10)]
        public string Code_value { get; set; }

        [StringLength(250)]
        public string Name_vn { get; set; }

        [StringLength(250)]
        public string Name_en { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public bool? Status { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? ModifiedDate { get; set; }

        [StringLength(50)]
        public string ModifiedBy { get; set; }
    }
}
