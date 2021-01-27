using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Lib.Domain.Core;

namespace Lib.Domain.Simples
{
    public class Setting
    {
        public Setting()
        {
        }

        public Setting(string name, string value)
        {
            Name = name;
            Value = value;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public virtual string Name { get; set; }

        /// <summary>
        /// Gets or sets the value
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Returns the setting value as the specified type
        /// </summary>
        public virtual T As<T>()
        {
            return CoreHelper.To<T>(Value);
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
