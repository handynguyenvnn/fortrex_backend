
namespace Lib.Cache
{
    public class KeyReturnType
    {
        public KeyReturnType(KeyType type = KeyType.None)
        {
            this.Type = type;
        }

        public KeyReturnType(string key, KeyType type = KeyType.None)
        {
            this.Key = key;
            this.Type = type;
        }

        public KeyReturnType()
        {
        }

        public enum KeyType
        {
            None,
            Key,
            Pattern
        }
        public string Key { get; set; }
        public KeyType Type { get; set; }
    }   
}
