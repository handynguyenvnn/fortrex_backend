using System.Runtime.Serialization;


namespace Lib.Domain.Coins
{
    [DataContract]
    public class DepositAddressResponse 
    {
        [DataMember(Order = 1)]
        public string Address { get; set; }

        [DataMember(Order = 2)]
        public string AddressTag { get; set; }

        [DataMember(Order = 3)]
        public string Assett { get; set; }

        [DataMember(Order = 4)]
        public bool Success { get; set; }
    }
}