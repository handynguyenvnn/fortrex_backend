using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Lib.Domain.CommonClass 
{
    public static class Commons
    {
        public static string SerializeObject<T>(this T toSerialize)
        {
            DataContractJsonSerializer js = new DataContractJsonSerializer(typeof(T));
            MemoryStream msObj = new MemoryStream();
            js.WriteObject(msObj, toSerialize);
            msObj.Position = 0;
            string json = "";
            using (StreamReader sr = new StreamReader(msObj))
            {
                 json = sr.ReadToEnd();

                sr.Close();
                msObj.Close();
            }
            return json;

            
        }
        public static DateTime TimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }
        public static DateTime getDateTimeFromUnixTimeStamp(uint timestamp)
        {
            return new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(timestamp);
        }
        public static string FormatNumber(string number, string format = "{0:#,##0.#####}")
        {
            var a = float.Parse(number);
            return NumberFormat((decimal)a, format);
        }

        public static string FormatNumber(decimal number, string format = "{0:#,##0.#####}")
        {
            if (number == 0)
            {
                return "0.00";
            }
            var a = float.Parse(number.ToString());
            return NumberFormat((decimal)a, format);
        }

        public static string FormatNumberD(double number, string format = "{0:#,##0.#####}")
        {
            var a = float.Parse(number.ToString());
            return NumberFormat((decimal)a, format);
        }

        private static string NumberFormat(decimal? number, string format)
        {
            return string.Format(format, number);
        }

        public static string FormatString(string str)
        {
            return str.Substring(0, 20) + "...";
        }
    }
    
}
