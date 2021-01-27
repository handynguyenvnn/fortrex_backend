using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Lib.Domain.User
{
    public class QANote
    {
        [ScaffoldColumn(false)]
        public int Id { get; set; }
        [Required]
        [UIHint("BizText")]
        public string Input_Amount { get; set; }
        [Required]
        [UIHint("BizText")]
        public string Note { get; set; }
        [ScaffoldColumn(false)]
        public decimal Amount {
            get
            {
                if (string.IsNullOrEmpty(Input_Amount))
                {
                    return 0;
                }
                else
                {
                    try
                    {
                        return decimal.Parse(Input_Amount);
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
        }
        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public int UserId { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDelete { get; set; }
        [ScaffoldColumn(false)]
        public string StrCreateDate
        {
            get { return CreateDate.ToString("yyyy/MM/dd HH:mm:ss"); }
        }
    }
}
