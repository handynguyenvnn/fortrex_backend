using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Cache
{
    public partial class CacheKeyManager : ICacheKeyConfig
    {
        public static Dictionary<string, CacheKey> KeyContainer { get; set; }
        public static Dictionary<string, string> CacheKeyTypes { get; set; }
        static CacheKeyManager()
        {
            KeyContainer = new Dictionary<string, CacheKey>();
            CacheKeyTypes = new Dictionary<string, string>();
        }        
    }
}
