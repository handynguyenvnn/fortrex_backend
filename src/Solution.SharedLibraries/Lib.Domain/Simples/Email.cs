using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    public class Email
    {
        public string Title { get; set; }
        public string Body { get; set; }
        public string EmailTo { get; set; }
        public string cc { get; set; }
    }
}
