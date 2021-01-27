
using System;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;
using Lib.Domain.Coins;
using Newtonsoft.Json;

namespace Web.SourceCoin.Common
{

    public class Erc20ApisClient
    {

        static string apiKey_etherscan = "CPHC2UWWCCET2FQFUAH3TH3P5HT8W1J3TJ";
        static string apiKey_ethplorer = "EK-2xziJ-DAaDLLj-os9Eb";
        //Initialise the general client client with config
        private readonly string HOST_LINK_API_EHTERSCAN = "https://api.etherscan.io/";
        private readonly string HOST_LINK_API_ETHPLORER = "https://api.ethplorer.io/";

        public Erc20ApisClient()
        {

        }

        /// <summary>
        /// get transaction by token erc20
        /// </summary>
        /// <returns></returns>
        //public ERC20TokenTransferByAddressResponse ERC20TokenTransferByAddress(string contract,string address)
        //{
        //    string urlApi = "";
        //    ERC20TokenTransferByAddressResponse _wallet = new ERC20TokenTransferByAddressResponse();
        //    if (!string.IsNullOrEmpty(contract))
        //    {
        //        urlApi = string.Format("{0}api?module=account&action=tokentx&contractaddress={1}&address={2}&page=1&offset=100&sort=desc&apikey={3}"
        //                                 , HOST_LINK_API_EHTERSCAN, contract, address, apiKey_etherscan);
        //    }
        //    else
        //    {
        //        urlApi = string.Format("{0}api?module=account&action=tokentx&address={1}&page=1&offset=100&sort=desc&apikey={2}"
        //                                 , HOST_LINK_API_EHTERSCAN, address, apiKey_etherscan);
        //    }

        //    //ServicePointManager.Expect100Continue = true;
        //    //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
        //    var client = new WebClient();
        //    //client.Headers.Add
        //    var resultToken = client.DownloadString(urlApi);
        //    var obj = JsonConvert.DeserializeObject<ERC20TokenTransferByAddressResponse>(resultToken);
        //    return obj;
        //}


        /// <summary>
        /// get transaction by coin ethereum
        /// </summary>
        /// <returns></returns>
        public EthTransactionByAddressResponse EthTransactionByAddress(string address)
        {
            string urlApi = "";
            EthTransactionByAddressResponse _wallet = new EthTransactionByAddressResponse();
            urlApi = string.Format("{0}api?module=account&action=txlist&address={1}&startblock=0&endblock=99999999&page=1&offset=10&sort=desc&apikey={2}"
                                        , HOST_LINK_API_EHTERSCAN, address, apiKey_etherscan);
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            //client.Headers.Add
            var resultToken = client.DownloadString(urlApi);
            var obj = JsonConvert.DeserializeObject<EthTransactionByAddressResponse>(resultToken);
            return obj;
        }
        public Erc20Gettransactioninfo GetTransactionInfo(string hash)
        {
            var prefitHash = hash.IndexOf("0x");
            if (prefitHash < 0 && !string.IsNullOrEmpty(hash))
            {
                hash = "0x" + hash;
            }
            Erc20Gettransactioninfo _wallet = new Erc20Gettransactioninfo();
            string urlApi = string.Format("{0}getTxInfo/{1}?apiKey={2}"
                                           , HOST_LINK_API_ETHPLORER, hash, apiKey_ethplorer);
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            //client.Headers.Add
            var resultToken = client.DownloadString(urlApi);
            try
            {
                var obj = JsonConvert.DeserializeObject<Erc20Gettransactioninfo>(resultToken);
                return obj;
            }
            catch (System.Exception)
            {
                return null;
            }

        }
        /// <summary>
        ///token:     show only specified token address operations
        ///type:      show operations of specified type only
        ///limit:     maximum number of operations[1 - 10, default = 10]
        ///timestamp: starting offset for operations[optional, unix timestamp]
        /// </summary>
        /// <returns></returns>
        public ERC20GetAddressHistoryListResponse Getlastaddressoperations(string contract, string address)
        {
            string urlApi = "";

            if (!string.IsNullOrEmpty(contract))
            {
                urlApi = string.Format("{0}getAddressHistory/{1}?token={2}&apiKey={3}"
                                         , HOST_LINK_API_ETHPLORER, address, contract, apiKey_ethplorer);
            }
            else
            {
                urlApi = string.Format("{0}getAddressHistory/{1}?apiKey={2}"
                                        , HOST_LINK_API_ETHPLORER, address, apiKey_ethplorer);
            }

            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            //client.Headers.Add
            var resultToken = client.DownloadString(urlApi);
            var obj = JsonConvert.DeserializeObject<ERC20GetAddressHistoryListResponse>(resultToken);
            return obj;
        }
    }

}