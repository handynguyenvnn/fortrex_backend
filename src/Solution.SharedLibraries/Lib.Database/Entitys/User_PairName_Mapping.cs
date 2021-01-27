namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class User_PairName_Mapping
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        [StringLength(50)]
        public string PairName { get; set; }
    }
}
