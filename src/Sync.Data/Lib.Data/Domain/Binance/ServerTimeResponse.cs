using System;
using System.Runtime.Serialization;


using Newtonsoft.Json;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// The current server time
    /// </summary>
    [DataContract]
    public class ServerTimeResponse
    {
        [DataMember(Order = 1)]
        
        public DateTime ServerTime { get; set; }
    }
}