using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Data.Repository.Models
{
    public class ScheduleTask
    {
        public int Id { get; set; }
        public int ProjectType { get; set; }
        public string Name { get; set; }
        public int Seconds { get; set; }
        public  string Type { get; set; }
        public bool Enabled { get; set; }
        public DateTime? LastStartUtc { get; set; }
        public DateTime? LastEndUtc { get; set; }
        public DateTime? LastSuccessUtc { get; set; }
        public bool StopOnError { get; set; }
    }
}
