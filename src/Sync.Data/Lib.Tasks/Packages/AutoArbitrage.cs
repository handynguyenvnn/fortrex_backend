using System;
using Newtonsoft.Json;
using RestSharp;
using Lib.Data.Repository.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Lib.Tasks.Packages
{
    public class AutoArbitrage : ITask
    {
        const string BTC = "BTC";
        const string ETH = "ETH";
        const string LTC = "LTC";
        const string XRP = "XRP";

        public AutoArbitrage()
        {
        }
        public void Execute()
        {
            CalculatorPackeges();
        }
        public void CalculatorPackeges()
        {
            CoinAsync(BTC, "USDT");
            CoinAsync(ETH, "USDT");
        }

        public async void CoinAsync(string coin, string to)
        {
            TaskRepository _task = new TaskRepository();
            try
            {
                string hostUrl = string.Format("{0}min-api.cryptocompare.com/data/v2/histominute?fsym={1}&tsym=USD&limit=10", "https://", coin);
                var restClient = new RestClient(hostUrl);
                var request = new RestRequest(Method.GET);
                var response = await restClient.ExecuteTaskAsync(request);
                dynamic result = JsonConvert.DeserializeObject(response.Content);
                var data = result["Data"]["Data"];
                string querySQL = "INSERT INTO [HighchartSync].[dbo].[CoinPriceSync] ([FromCoin], [Open], [Close], [High], [Low], [UpdateTime], [CreateOn]) values ('{0}_{1}', {2}, {3}, {4}, {5}, {6}, '{7}'); ";
                foreach (dynamic item in data)
                {
                    Task<bool> task = CreateDataync(item, querySQL, coin, to, _task);
                    task.Wait();
                    //await System.Threading.Tasks.Task.Run(() => CreateData(item, querySQL, coin, to, _task)).ConfigureAwait(true);
                }
            }
            catch
            { }
        }

        public async Task<bool> CreateDataync(dynamic item, string querySQL, string coin, string to, TaskRepository task)
        {
            double high = (double)item["high"];
            double low = (double)item["low"];
            int time = (int)item["time"];

            var _low = low + low * 0.3 / 100;
            if (high < _low)
            {
                high = _low;
            }

            int second = 0;
            do
            {
                try
                {
                    string sqlQuery = GetQuerySync(querySQL, coin, to, time, low, high);
                    task.HighchartSync_InsertData(sqlQuery);
                }
                catch { }
                Thread.Sleep(990);
                second += 1000;
            }
            while (second < 10000);

            return true;
        }

        private string GetQuerySync(string querySQL, string coin, string to, int time, double low, double high)
        {
            DateTime currentTime = DateTime.Now;
            Random reandom = new Random();
            int z = reandom.Next(-5, 5);
            int k = 1;
            if (z < 0)
            {
                k = -1;
            }

            var x = reandom.NextDouble();

            var _high = high + high * x * k / 200;
            Thread.Sleep(5);
            var _loww = low + low * x * k / 200;

            double _open = GetRandomNumber(_loww, _high);
            Thread.Sleep(5);
            double _close = GetRandomNumber(_loww, _high);

            return string.Format(querySQL, coin, to, _open, _close, _high, _loww, time, currentTime);
        }

        public double GetRandomNumber(double v1, double v2)
        {
            if (v1 == 0)
            {
                return v2;
            }
            var min = Math.Min(v1, v2);
            var max = Math.Max(v1, v2);
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
    }
}
