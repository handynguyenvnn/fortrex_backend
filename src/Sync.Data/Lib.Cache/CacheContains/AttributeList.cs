using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lib.Cache
{
    public class AttributeList
    {
        public List<Attri> Attributes { get; set; }
    }

    public class Attri
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}