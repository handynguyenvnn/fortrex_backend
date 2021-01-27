using System;
using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.Promocodes
{
    public class Promocode_User_Mapping
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public int UserId { get; set; }
        [UIHint("BizDisplayText")]
        public int PromocodeId { get; set; }
        [ScaffoldColumn(false)]
        public string Username { get; set; }
        [ScaffoldColumn(false)]
        public string Email { get; set; }
        [ScaffoldColumn(false)]
        public string Code { get; set; }
    }
}
