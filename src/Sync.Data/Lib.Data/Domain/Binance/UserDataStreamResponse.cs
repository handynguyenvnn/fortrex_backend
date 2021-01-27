using System.Runtime.Serialization;

namespace Lib.Domain.Coins
{
    /// <summary>
    /// User Data Stream response
    /// </summary>
    [DataContract]
    public class UserDataStreamResponse
    {
        [DataMember(Order = 1)]
        public string ListenKey { get; set; }
    }
}
