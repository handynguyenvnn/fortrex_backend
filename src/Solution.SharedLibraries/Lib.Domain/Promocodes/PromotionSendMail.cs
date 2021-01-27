using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Promocodes
{
    public class PromotionSendMail
    {
        public int Id { get; set; }
        public int PromotionId { get; set; }
        public int UserId { get; set; }
        public bool IsActive { get; set; }
        public DateTime? CreateDate { get; set; }
    }
}
