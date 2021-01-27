using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Simples
{
    public class SimpleBase
    {
        public SimpleBase()
        {
            UpdateOn = DateTime.Now;
        }
        public int Id { get; set; }
        public string Title { get; set; }
        public string PhotoUrl { get; set; }
        public DateTime UpdateOn { get; set; }
        public string ReferentUrl { get; set; }
        public bool IsLink { get; set; }
    }
}
