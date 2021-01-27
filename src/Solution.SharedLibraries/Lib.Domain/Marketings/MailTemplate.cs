using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Lib.Domain.Marketings
{
    public class MailTemplate
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }

        [Required]
        [UIHint("BizCbo")]
        [AdditionalMetadata("ViewDataSelectList", "LstAccount")]
        [Display(Name = "Account mail")]
        public int AccountId { get; set; }

        [Required]
        [UIHint("BizCbo")]
        [AdditionalMetadata("ViewDataSelectList", "LstMarketingType")]
        [Display(Name = "Type")]
        public int Type { get; set; }

        [ScaffoldColumn(false)]
        public string TypeName { get; set; }

        [Required]
        [UIHint("BizText")]
        public string Title { get; set; }

        [UIHint("BizText")]
        public string Email { get; set; }

        [UIHint("BizCkEditor")]
        public string Body { get; set; }

        [ScaffoldColumn(false)]
        public DateTime? UpdateDate { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        public string CreateDatestr { get; set; }
        [ScaffoldColumn(false)]
        public int CreateBy { get; set; }
        [ScaffoldColumn(false)]
        public string CreateByName { get; set; }

        [UIHint("BizCheckBox")]
        [Display(Name = "IsActive")]
        public bool IsActive { get; set; }

        [UIHint("BizCheckBox")]
        [Display(Name = "Test Only")]
        public bool IsTest { get; set; }

        [ScaffoldColumn(false)]
        public int? LastId { get; set; }
    }
}
