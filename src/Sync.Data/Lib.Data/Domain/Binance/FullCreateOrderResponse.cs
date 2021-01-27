using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// Full Response following a call to the Create Order endpoint
    /// </summary>
    [DataContract]
    public class FullCreateOrderResponse : ResultCreateOrderResponse
    {
        [DataMember(Name = "fills")]
        public List<Fill> Fills { get;set; }
    }
}

    