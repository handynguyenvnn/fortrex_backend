using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.IO;
using RestSharp;
using System.Configuration;
using System.Globalization;
using Lib.Tasks.Coinbase;
using System.Net;
using Newtonsoft.Json;

namespace CoinbaseConnector
{
    public class HelperCoin
    {
        private static string API_KEY = ConfigurationManager.AppSettings["API_KEY"];
        private static string API_SECRET = ConfigurationManager.AppSettings["API_SECRET"];
        private static string API_KEY_ETH = ConfigurationManager.AppSettings["API_KEY_ETH"];
        private static string API_SECRET_ETH = ConfigurationManager.AppSettings["API_SECRET_ETH"];
        private static string VERSION = ConfigurationManager.AppSettings["VERSION"];
        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        public static void ExcuteRestRequest(IRestClient client, IRestRequest request)
        {
            var uri = client.BuildUri(request);
            var path = uri.AbsolutePath;

            string timestamp = GetCurrentUnixTimestampSeconds().ToString(CultureInfo.InvariantCulture);
            var method = request.Method.ToString().ToUpper(CultureInfo.InvariantCulture);

            var body = string.Empty;
            if (request.Method != Method.GET)
            {
                var param = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
                if (param != null && param?.Value?.ToString() != "null" && !string.IsNullOrWhiteSpace(param?.Value?.ToString()))
                {
                    body = param.Value.ToString();
                }
            }
            else
            {
                path = uri.PathAndQuery;
            }

            var hmacSig = GenerateSignature(timestamp, method, path, body, API_SECRET);

            request.AddHeader("CB-ACCESS-KEY", API_KEY);
            request.AddHeader("CB-ACCESS-SIGN", hmacSig);
            request.AddHeader("CB-ACCESS-TIMESTAMP", timestamp);
            request.AddHeader("CB-VERSION", VERSION);
        }

        public static void ExcuteRestRequestETH(IRestClient client, IRestRequest request)
        {
            var uri = client.BuildUri(request);
            var path = uri.AbsolutePath;

            string timestamp = GetCurrentUnixTimestampSeconds().ToString(CultureInfo.InvariantCulture);
            var method = request.Method.ToString().ToUpper(CultureInfo.InvariantCulture);

            var body = string.Empty;
            if (request.Method != Method.GET)
            {
                var param = request.Parameters.FirstOrDefault(p => p.Type == ParameterType.RequestBody);
                if (param != null && param?.Value?.ToString() != "null" && !string.IsNullOrWhiteSpace(param?.Value?.ToString()))
                {
                    body = param.Value.ToString();
                }
            }
            else
            {
                path = uri.PathAndQuery;
            }

            var hmacSig = GenerateSignature(timestamp, method, path, body, API_SECRET_ETH);

            request.AddHeader("CB-ACCESS-KEY", API_KEY_ETH);
            request.AddHeader("CB-ACCESS-SIGN", hmacSig);
            request.AddHeader("CB-ACCESS-TIMESTAMP", timestamp);
            request.AddHeader("CB-VERSION", VERSION);
        }

        public static string GenerateSignature(string timestamp, string method, string url, string body, string appSecret)
        {
            return GetHMACInHex(appSecret, timestamp + method + url + body);
        }

        internal static string GetHMACInHex(string key, string data)
        {
            var hmacKey = Encoding.UTF8.GetBytes(key);

            using (var signatureStream = new MemoryStream(Encoding.UTF8.GetBytes(data)))
            {
                var hex = new HMACSHA256(hmacKey).ComputeHash(signatureStream)
                    .Aggregate(new StringBuilder(), (sb, b) => sb.AppendFormat("{0:x2}", b), sb => sb.ToString());

                return hex;
            }
        }
        
        public static long GetCurrentUnixTimestampMillis()
        {
            return (long)(DateTime.UtcNow - UnixEpoch).TotalMilliseconds;
        }

        public static DateTime DateTimeFromUnixTimestampMillis(long millis)
        {
            return UnixEpoch.AddMilliseconds(millis);
        }

        public static long GetCurrentUnixTimestampSeconds()
        {
            //LibraryLog.WriteErrorLog(string.Format("{0} - {1}", DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"), UnixEpoch.ToString("yyyy-MM-dd HH:mm:ss")));
            //return (long)(DateTime.UtcNow - UnixEpoch).TotalSeconds;
            var gettime = CoinbaseTime();
            return gettime.data.epoch;
        }
        public static DateTime DateTimeFromUnixTimestampSeconds(long seconds)
        {
#if STANDARD
            return DateTimeOffset.FromUnixTimeSeconds(seconds);
#else
            return UnixEpoch.AddSeconds(seconds);
#endif
        }

        public static CoinbaseTimeResponse CoinbaseTime()
        {
            CoinbaseTimeResponse time = new CoinbaseTimeResponse();
            string urlApi = "https://api.coinbase.com/v2/time";
            ServicePointManager.Expect100Continue = true;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
            var client = new WebClient();
            var resultToken = client.DownloadString(urlApi);
            time = JsonConvert.DeserializeObject<CoinbaseTimeResponse>(resultToken);
            return time;
        }
    }
}
