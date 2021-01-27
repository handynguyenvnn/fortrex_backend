using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.ContentStatics
{
    public class ContentStatic
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string Title { get; set; }
        public string Meg { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime? ShowDate { get; set; }
        public DateTime? HideDate { get; set; }
    }
}
