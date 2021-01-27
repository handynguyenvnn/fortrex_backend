using System.Runtime.Serialization;


namespace Lib.Domain.Coins
{
    /// <summary>
    /// Balance respomse providing information on assets
    /// </summary>
   
    public class EthWalletGenerate
    {
       
        public string address { get; set; }
        
     
        public string privateKey { get; set; }

       
        public string publicKey { get; set; }

    }
    public class EthWalletGenerateResponse
    {
        public EthWalletGenerate payload { get; set; }

    }
}