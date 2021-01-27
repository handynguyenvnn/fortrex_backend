using System;
using System.Linq;

namespace Lib.Cache
{
    public class CacheType
    {
        public CacheType(string name)
        {
            Name = name;
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }

    public delegate KeyReturnType KeyFor(CacheEventType type, CacheKeyParameter param);

    public abstract class CacheKey
    {
        protected CacheKey(CacheType baseType, CacheType subType, string key)
        {
            BaseType = baseType;
            SubType = subType;
            Key = key;
            CacheTime = new TimeSpan(0, 60, 0);
        }

        public KeyFor KeyFor { get; set; }
        public TimeSpan CacheTime { get; set; }
        public CacheType BaseType { get; set; }
        public CacheType SubType { get; set; }
        public string Key { get; set; }

        static System.Text.RegularExpressions.Regex formatRegex = new System.Text.RegularExpressions.Regex(@"\{\d+\}");
        public KeyReturnType ToGenericKeyPattern(params object[] keyParam)
        {

            if (keyParam == null || keyParam.Length == 0)
                return ToRootPattern();

            string keyFormats = string.Empty;

            var keyParamParts = Key.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
            int keyCount = 0;

            for (int i = 0; i < keyParamParts.Length; i++)
            {
                var matchs = formatRegex.Matches(keyParamParts[i]);
                if (keyCount < keyParam.Length)
                {
                    if (matchs.Count > 1)
                    {
                        string[] subParts = keyParamParts[i].Replace("-{", "_{").Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                        string subKeyFormat = "";
                        for (int j = 0; j < subParts.Length; j++)
                        {
                            if (keyCount < keyParam.Length)
                            {
                                subKeyFormat += string.Format("-{0}", subParts[j].Replace("_{", "-{"));
                                keyCount++;
                            }
                            else
                            {
                                break;
                            }
                        }
                        subKeyFormat = subKeyFormat.TrimStart('-');
                        keyFormats += subKeyFormat;
                    }
                    else
                    {
                        keyFormats += string.Format(".{0}", keyParamParts[i]);
                        keyCount++;
                    }
                }
                else
                {
                    break;
                }
            }

            if (keyCount > keyParam.Length)
                keyCount = keyParam.Length;

            keyFormats = keyFormats.TrimStart('.');

            return new KeyReturnType(string.Format("{0}:{1}:{2}", BaseType, SubType, string.Format(keyFormats, keyParam.Take(keyCount).ToArray())), KeyReturnType.KeyType.Pattern);
        }

        public virtual KeyReturnType ToRootPattern()
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}", BaseType, SubType, ""), KeyReturnType.KeyType.Pattern);
        }

        protected string GetTypeName(Type type)
        {
            string result = type.Name;
            if (type.IsGenericType)
            {
                var gtypes = type.GetGenericArguments();
                for (int i = 0; i < gtypes.Length; i++)
                {
                    result += gtypes[i].Name;
                    if (i < gtypes.Length - 1)
                    {
                        result += ",";
                    }
                }
            }

            return result;

        }
    }

    public class CacheKey<T> : CacheKey
    {
        protected CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }
        public static CacheKey<T> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T>(baseType, subType, key);
        }

        public KeyReturnType ToKey()
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, Key, GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }
        public override KeyReturnType ToRootPattern()
        {
            return ToKey();
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, Key, GetTypeName(typeof(T)));
        }
    }

    public class CacheKey<T, P1> : CacheKey
    {
        protected CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }

        public override string ToString()
        {
            return string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, Key, GetTypeName(typeof(T)));
        }
    }

    public class CacheKey<T, P1, P2> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }
        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }
        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CacheKey<T, P1, P2, P3> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2, P3> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2, P3>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2, P3 p3)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2, p3), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }

        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CacheKey<T, P1, P2, P3, P4> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2, P3, P4> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2, P3, P4>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2, P3 p3, P4 p4)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2, p3, p4), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }

        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CacheKey<T, P1, P2, P3, P4, P5> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2, P3, P4, P5> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2, P3, P4, P5>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2, p3, p4, p5), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }

        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CacheKey<T, P1, P2, P3, P4, P5, P6> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2, P3, P4, P5, P6> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2, P3, P4, P5, P6>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2, p3, p4, p5, p6), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }
        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }

    public class CacheKey<T, P1, P2, P3, P4, P5, P6, P7> : CacheKey
    {
        CacheKey(CacheType baseType, CacheType subType, string key)
            : base(baseType, subType, key)
        {
        }

        public static CacheKey<T, P1, P2, P3, P4, P5, P6, P7> CreateKey(CacheType baseType, CacheType subType, string key)
        {
            return new CacheKey<T, P1, P2, P3, P4, P5, P6, P7>(baseType, subType, key);
        }

        public KeyReturnType ToKey(P1 p1, P2 p2, P3 p3, P4 p4, P5 p5, P6 p6, P7 p7)
        {
            return new KeyReturnType(string.Format("{0}:{1}:{2}.{3}", BaseType, SubType, string.Format(Key, p1, p2, p3, p4, p5, p6, p7), GetTypeName(typeof(T))), KeyReturnType.KeyType.Key);
        }
        public KeyReturnType ToKeyPattern(P1 p1)
        {
            return base.ToGenericKeyPattern(p1);
        }

        public override string ToString()
        {
            return base.ToString();
        }
    }
}
