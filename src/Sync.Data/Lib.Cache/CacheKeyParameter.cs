
namespace Lib.Cache
{
    public class CacheKeyParameter
    {
        public CacheKeyParameter(bool clearByPattern = false)
        {
            this.ClearPattern = clearByPattern;
        }
        public bool ClearPattern { get; private set; }
    }

}