using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    public class ResponseError
    {
        public ResponseError()
        {
            ClassColor = "success";
        }
        public string Meg { get; set; }
        public string ClassColor { get; set; }
    }
}
