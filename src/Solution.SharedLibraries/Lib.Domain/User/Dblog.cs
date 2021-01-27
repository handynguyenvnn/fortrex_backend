using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.User
{
    public class Dblog
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [UIHint("BizDisplayText")]
        public string Name { get; set; }
        [UIHint("BizDisplayLongText")]
        public string Message { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreateOn { get; set; }
        [ScaffoldColumn(false)]
        public int? ReferentId { get; set; }
    }
}
