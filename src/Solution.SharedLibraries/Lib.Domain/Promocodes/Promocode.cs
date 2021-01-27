using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.Promocodes
{
    public class Promocode
    {
        public Promocode()
        {
            TotalItems = 0;
        }
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [UIHint("BizSmallText")]
        public decimal Percent { get; set; }
        [Required]
        [UIHint("BizText")]
        public string FromDate { get; set; }
        [Required]
        [UIHint("BizText")]
        public string EndDate { get; set; }
        [ScaffoldColumn(false)]
        public int? Package { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public int Status { get; set; }
        [Required]
        [UIHint("BizText")]
        public string Code { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public decimal MinValueBtc { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public decimal MinValueEth { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public int TotalDays { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public decimal TotalReceivedBtc { get; set; }
        [Required]
        [UIHint("BizSmallText")]
        public decimal TotalReceivedEth { get; set; }
        public int TotalItems { get; set; }
    }
}
