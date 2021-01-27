using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class OtOtherList
    {
        public int Id { get; set; }
        public string TypeCode { get; set; }
        public string Code { get; set; }
        public string CodeValue { get; set; }
        public string NameVn { get; set; }
        public string NameEn { get; set; }
        public string Description { get; set; }
        public bool? Status { get; set; }
        public DateTime? CreateDate { get; set; }
        public string CreateBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
