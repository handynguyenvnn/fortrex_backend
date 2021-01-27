using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using MySql.Data.MySqlClient;
using Lib.Domain.Packages.Trades;
using Lib.Domain;

namespace Web.SourceCoin.Helpers
{
    public class WebServices
    {
        private readonly string connStr = ConfigurationManager.ConnectionStrings["WebApi.MySql"].ToString();

        public Candlesticks Candlestick_GetBy_Pair_LastTime(string pair)
        {
            MySqlConnection conn = new MySqlConnection(connStr);
            conn.Open();

            Candlesticks response = new Candlesticks();

            string sql = string.Format(" SELECT * FROM candlestick_data_5s_tab where pair_name='{0}' order by time_close desc, id desc limit 1; ", pair);
            MySqlCommand cmd = new MySqlCommand(sql, conn);
            using (MySqlDataReader rdr = cmd.ExecuteReader())
            {
                while (rdr.Read())
                {
                    response.PairName = rdr.GetString(1);
                    response.Open = (decimal)(rdr.GetInt64(4) / Constants.DEFILE_ROUND);
                    response.High = (decimal)(rdr.GetInt64(5) / Constants.DEFILE_ROUND);
                    response.Low = (decimal)(rdr.GetInt64(6) / Constants.DEFILE_ROUND);
                    response.VolumeFrom = (decimal)(rdr.GetInt64(7) / Constants.DEFILE_ROUND);
                    response.VolumeTo = (decimal)(rdr.GetInt64(8) / Constants.DEFILE_ROUND);
                    response.Close = (decimal)(rdr.GetInt64(9) / Constants.DEFILE_ROUND);
                    response.TimeClose = rdr.GetInt64(10);
                    response.TimeOpen = rdr.GetInt64(11);
                    response.LastTimes = rdr.GetInt64(10) * 1000;
                    response.Times = rdr.GetInt64(11) * 1000;
                }
            }

            return response;
        }
    }
}