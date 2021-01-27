using System.Runtime.Serialization;


namespace Lib.Domain.Coins
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
   
    public class EthWalletGenerate
    {
        [DataMember(Order = 1)]
        public string address { get; set; }
        
        [DataMember(Order = 2)]
        public string privateKey { get; set; }

        [DataMember(Order = 3)]
        public string publicKey { get; set; }

    }
    public class EthWalletGenerateResponse
    {
        public EthWalletGenerate payload { get; set; }

    }
}