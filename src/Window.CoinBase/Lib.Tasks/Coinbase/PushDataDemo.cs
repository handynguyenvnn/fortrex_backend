using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Web.Script.Serialization;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using MlkPwgen;

namespace Lib.Tasks.Coinbase
{
    public class PushDataDemo : ITask
    {
        private readonly string connectString;
        public PushDataDemo()
        {
            connectString = ConfigurationManager.AppSettings["DB_SERVER"];
        }
        public void Execute()
        {
            //LibraryLog.WriteErrorLog("Start push");
            this.PushData();
            //LibraryLog.WriteErrorLog("End push");
        }
        public void PushData()
        {
            try
            {
                decimal[] data = new decimal[] { 450, 430, 445, 375, 390, 425, 410, 410, 365, 420, 400, 370, 435, 443, 377, 429, 348, 408, 375, 445, 305, 388, 350, 365, 420, 450, 413, 363, 430, 358 };
                System.Random r = new System.Random();
                int index = r.Next(0, 29);

                //LibraryLog.WriteErrorLog(index.ToString());

                decimal val = data[index];

                string sql = "INSERT INTO dbo.BuyCoin(UserId,NumberCoin,OriginUSD,PriceUSD,CreateDate,UpdateDate,Status,[Transaction],MethodPaymentId)";
                sql = sql + " VALUES(@UserId, @NumberCoin, @OriginUSD, @PriceETH, @CreateDate, @UpdateDate, @Status,@Transaction, @MethodPaymentId)";

                //LibraryLog.WriteErrorLog(sql);

                using (SqlConnection conn = new SqlConnection(connectString))
                {
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add("@UserId", SqlDbType.Int);
                    cmd.Parameters["@UserId"].Value = 1;

                    cmd.Parameters.Add("@NumberCoin", SqlDbType.Decimal);
                    cmd.Parameters["@NumberCoin"].Value = val;

                    cmd.Parameters.Add("@OriginUSD", SqlDbType.Decimal);
                    cmd.Parameters["@OriginUSD"].Value = 0.9;

                    cmd.Parameters.Add("@PriceETH", SqlDbType.Decimal);
                    cmd.Parameters["@PriceETH"].Value = (decimal)0.9 * val;

                    cmd.Parameters.Add("@CreateDate", SqlDbType.DateTime);
                    cmd.Parameters["@CreateDate"].Value = DateTime.Now;

                    cmd.Parameters.Add("@UpdateDate", SqlDbType.DateTime);
                    cmd.Parameters["@UpdateDate"].Value = DateTime.Now;

                    cmd.Parameters.Add("@Status", SqlDbType.Int);
                    cmd.Parameters["@Status"].Value = 1;

                    cmd.Parameters.Add("@Transaction", SqlDbType.NVarChar);
                    cmd.Parameters["@Transaction"].Value = PasswordGenerator.Generate(40);

                    cmd.Parameters.Add("@MethodPaymentId", SqlDbType.Int);
                    cmd.Parameters["@MethodPaymentId"].Value = 2;

                    try
                    {
                        conn.Open();
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        LibraryLog.WriteErrorLog(ex.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                LibraryLog.WriteErrorLog(ex);
            }
        }
    }
}
