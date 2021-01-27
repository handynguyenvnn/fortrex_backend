using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain
{
    public static class LockHelper
    {
        public static System.Collections.Hashtable Pool { get; set; }
        static object s_lock = new object();
        static LockHelper()
        {
            Pool = new System.Collections.Hashtable(200);
        }
        public static object GetLock(string key)
        {
            lock (s_lock)
            {
                LockObject lockItem = (LockObject)Pool[key.ToLower()];
                if (lockItem == null)
                {
                    lockItem = new LockObject();
                    lockItem.HoldLock = 1;
                    Pool.Add(key.ToLower(), lockItem);
                }
                else
                {
                    lockItem.HoldLock++;
                }

                return lockItem;
            }
        }

        public static void ReleaseLock(string key)
        {
            lock (s_lock)
            {
                LockObject lockItem = (LockObject)Pool[key.ToLower()];
                if (lockItem != null)
                {
                    lockItem.HoldLock--;
                    if (lockItem.HoldLock <= 0)
                        Pool.Remove(key.ToLower());
                }
            }
        }

        class LockObject
        {
            public int HoldLock { get; set; }
        }
    }
}
