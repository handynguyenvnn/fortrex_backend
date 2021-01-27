using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using Lib.Domain;
using Lib.Domain.Coins;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Web.SourceCoin.Common
{
  
    public class BNBClient
    {
        static string  apiKey = "vmPUZE6mv9SD5VNHk4HlWFsOr6aKE2zvsw0MuIgwCIPy6utIco14y7Ju91duEh8A";
        static string secretKey = "NhqPtmdSJYdKjVHjA7PZj4Mge3R5YNiP1e3UZjInClVN65XAbvqqM6A7H5fATj0j";
        //Initialise the general client client with config
       
        public BNBClient()
        {
          
        }
        public List<SymbolPriceChangeTickerResponse> GetDailyTickers(string symbol="")
        {
            string urlApi = "";
            List<SymbolPriceChangeTickerResponse> lstTonKho = new List<SymbolPriceChangeTickerResponse>();
            if (!string.IsNullOrEmpty(symbol))
            {
                 urlApi = "https://api.binance.com/api/v1/" + "/ticker/24hr?symbol=" + symbol;
            }
            else
            {
                urlApi = "https://api.binance.com/api/v1/" + "/ticker/24hr";
            }
            
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
            var obj = JArray.Parse(resultToken);
            lstTonKho = JsonConvert.DeserializeObject<List<SymbolPriceChangeTickerResponse>>(resultToken);
            //return (decimal)obj[0]["price_usd"];
            return lstTonKho;
        }
       
        public List<SymbolPriceResponse> TickersPrices()
        {
            List<SymbolPriceResponse> lstobj = new List<SymbolPriceResponse>();
            string urlApi = "https://api.binance.com/api/" + "/v3/ticker/price";
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
            //var obj = JArray.Parse(resultToken);
            lstobj = JsonConvert.DeserializeObject<List<SymbolPriceResponse>>(resultToken);
            //return (decimal)obj[0]["price_usd"];
            return lstobj;
        }
        public decimal PriceBuySymbol(string symbol)
        {
            string urlApi = "https://api.binance.com/api/" + "/v3/ticker/price?symbol="+ symbol;
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
           var lstobj = JsonConvert.DeserializeObject<SymbolPriceResponse>(resultToken);
            return lstobj.Price;
            
        }
        public decimal GetPriceBNCT()
        {
            string urlApi = "https://bitchainnet.io/api/user/GetPrice";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
           
            var resultToken = client.DownloadString(urlApi);
            var obj =JsonConvert.DeserializeObject<double>(resultToken);
            return (decimal)obj;
          
        }
        //public async Task<List<SymbolPriceResponse>> GetSymbolsPriceTicker()
        //{
        //    var dailyTickers = await client.GetSymbolsPriceTicker();
        //    return dailyTickers;
        //}
        public List<OrderResponse> order()
        {
            List<OrderResponse> orders = new List<OrderResponse>();
            string urlApi = "https://api.binance.com/api/" + "/v3/ticker/price";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
            var obj = JArray.Parse(resultToken);
            orders = JsonConvert.DeserializeObject<List<OrderResponse>>(resultToken);
            //return (decimal)obj[0]["price_usd"];
            
            return orders;
        }
        public List<object> Klines(string symbol, string interval= "5m", int limit=1000, long startTime=0, long endTime=0)
        {
            List<object> lstobj = new List<object>();
            string urlApi = "https://api.binance.com/api/" + string.Format("/v3/klines?symbol={0}&interval={1}",symbol,interval);
            if (limit>0)
            {
                urlApi += "&limit="+ limit.ToString();
            }
            if (startTime > 0)
            {
                urlApi += "&startTime=" + startTime.ToString();
            }
            if (endTime > 0)
            {
                urlApi += "&endTime=" + endTime.ToString();
            }
            //ServicePointManager.Expect100Continue = true;
            //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
            //var obj = JArray.Parse(resultToken);
            lstobj = JsonConvert.DeserializeObject<List<object>>(resultToken);
            //return (decimal)obj[0]["price_usd"];
            return lstobj;
        }
    }

}