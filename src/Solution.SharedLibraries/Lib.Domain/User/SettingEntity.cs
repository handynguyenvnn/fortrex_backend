using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.User
{
    public class SettingEntity
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [UIHint("BizText")]
        public string Name { get; set; }
        [UIHint("BizText")]
        public string Value { get; set; }
    }
}
