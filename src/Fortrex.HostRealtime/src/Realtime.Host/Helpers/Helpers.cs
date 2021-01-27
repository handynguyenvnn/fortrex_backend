
using System;

namespace Web.SourceCoin.Helpers
{
    public class Helper
    {
     

        public string FormatNumber(string number, string format = "{0:#,##0.#####}")
        {
            var a = float.Parse(number);
            return NumberFormat((decimal)a, format);
        }

        public string FormatNumber(decimal number, string format = "{0:#,##0.#####}")
        {
            if (number == 0)
            {
                return "0.00";
            }
            var a = float.Parse(number.ToString());
            return NumberFormat((decimal)a, format);
        }

        public string FormatNumberD(double number, string format = "{0:#,##0.#####}")
        {
            var a = float.Parse(number.ToString());
            return NumberFormat((decimal)a, format);
        }

        private string NumberFormat(decimal? number, string format)
        {
            return string.Format(format, number);
        }

        public string FormatString(string str)
        {
            return str.Substring(0, 20) + "...";
        }

        private int Numbers(int num)
        {
            if (num == 0)
                return num;

            while (num % 10 == 0)
            {
                num = num / 10;
            }
            return num;
        }

      
        public bool IsDayOffWeek(bool isRequest = false)
        {
            int day = (int)DateTime.Now.DayOfWeek;
            if (day == 0 || day == 6)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        public int GetCurrentUnixTimestampSeconds(DateTime date)
        {
            DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0);
            return (Int32)(date - UnixEpoch).TotalSeconds;
        }
    }
}