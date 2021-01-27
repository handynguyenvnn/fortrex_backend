using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    [Serializable]
    public class SimpleList<T> : ListBase
    {
        public List<T> Items { get; set; }
    }
}
