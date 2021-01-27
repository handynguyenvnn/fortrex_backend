
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Lib.Domain.Coins;

namespace Web.SourceCoin.Common
{
  
    public class CryptoapisClient
    {

        static string  apiKey = "20aa5f80c5a7c2ef14beeab284e78b66e5fdce96";
        
        //Initialise the general client client with config
        private readonly string HOST_LINK_API = "https://api.cryptoapis.io/v1/bc/";

        public CryptoapisClient()
        {
          
        }
        public EthWalletGenerateResponse EthWallet_Generate()
        {
            EthWalletGenerateResponse _wallet = new EthWalletGenerateResponse();
            string urlApi = string.Format("{0}eth/ropsten/address", HOST_LINK_API);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlApi);
            request.Method = "POST";
            request.ContentType = "application/json";
            request.Headers.Add("x-api-key", apiKey);
            using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
            {
                using (Stream stream = response.GetResponseStream())
                {
                    DataContractJsonSerializer dcs = new DataContractJsonSerializer(typeof(EthWalletGenerateResponse));
                    _wallet = (EthWalletGenerateResponse)dcs.ReadObject(stream);
                }
            }
            return _wallet;
        }

       
    }


}