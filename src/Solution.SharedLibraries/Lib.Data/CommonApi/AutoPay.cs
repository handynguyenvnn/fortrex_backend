
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Lib.Domain.Coins;

namespace Web.SourceCoin.Common
{
  
    public class WithdrawAutoPay
    {

        static string secret_key = "bo@gate!qwertyuio0987654321";
        
        //Initialise the general client client with config
        private readonly string HOST_LINK_API = "https://api.gescoin.io/api/blockchain";

        public WithdrawAutoPay()
        {
          
        }
        public EthWalletGenerateResponse EthWallet_Generate()
        {
            EthWalletGenerateResponse _wallet = new EthWalletGenerateResponse();
            string urlApi = string.Format("{0}/pay_withdraw", HOST_LINK_API);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(urlApi);
            request.Method = "POST";
            request.ContentType = "application/json";
            //request.Headers.Add("x-api-key", apiKey);
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