using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class Blockcypher
    {
        public string[] addresses { get; set; }
        public List<Output> outputs { get; set; }
    }

    public class Output
    {
        public string value { get; set; }
        public string script { get; set; }
        public string[] addresses { get; set; }
        public string script_type { get; set; }
    }
}
