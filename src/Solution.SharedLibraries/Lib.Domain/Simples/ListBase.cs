using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    [Serializable]
    public class ListBase
    {
        public int Count { get; set; }
        public int TotalResult { get; set; }
        public int LastId { get; set; }
    }
}
