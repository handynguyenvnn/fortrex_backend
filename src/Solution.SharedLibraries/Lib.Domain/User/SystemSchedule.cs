using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.User
{
    public class SystemSchedule
    {
        public string Name { get; set; }
        public DateTime? LastStartUtc { get; set; }
        public DateTime? LastEndUtc { get; set; }
        public DateTime? LastSuccessUtc { get; set; }
        public DateTime TimeSystem { get; set; }
        public int Seconds { get; set; }
        public int Enabled { get; set; }
        public string StrLastStartUtc {
            get { return LastStartUtc.HasValue ? LastStartUtc.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        }
        public string StrLastEndUtc {
            get { return LastEndUtc.HasValue ? LastEndUtc.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        }
        public string StrLastSuccessUtc {
            get { return LastSuccessUtc.HasValue ? LastSuccessUtc.Value.ToString("yyyy-MM-dd HH:mm:ss") : ""; }
        }
        public string StrTimeSystem
        {
            get { return TimeSystem.ToString("yyyy-MM-dd HH:mm:ss"); }
        }
    }
}
