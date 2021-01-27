using System;
using System.Collections.Generic;

namespace Realtime.Host.Entities
{
    public partial class PackagesBonusF
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Percent { get; set; }
        public int Type { get; set; }
        public int Level { get; set; }
    }
}
