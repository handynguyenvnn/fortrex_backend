
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Lib.Domain.Packages.Trades
{
    public class UserEntity
    {
        public int AccountId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        
    }
    
}
