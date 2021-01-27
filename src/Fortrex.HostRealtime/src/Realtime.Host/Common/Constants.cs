using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain
{
    public static class Constants
    {
        public static string TOKEN_KEY = "FO-USER-";
        public static int PAGE_INDEX = 0;
        public static int PAGE_SIZE = 1000;
        public static int PAGE_SIZE_DEFAULT = 20;
        public static TimeSpan TwoFaCodeExpire = TimeSpan.FromSeconds(50);
        
    }
}
