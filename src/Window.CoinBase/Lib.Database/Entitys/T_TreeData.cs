namespace LibDatabaseEntitys
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class T_TreeData
    {
        public int Id { get; set; }

        public int Node { get; set; }

        public int ParentId { get; set; }

        public int UserId { get; set; }

        public int Level { get; set; }

        public int ManageId { get; set; }

        public DateTime? CreateOn { get; set; }
    }
}
