using Lib.Domain.User;
using System;
using System.Web;
using Web.SourceCoin.Common;

namespace Web.SourceCoin.Helpers
{
    public class Helper
    {
        #region Check login
        public bool UserValidate(MUser user, string Password, out string meg)
        {
            meg = string.Empty;
            if (!user.IsActive)
            {
                meg = "Your account has not been activated";
            }
            if (user.IsLock)
            {
                meg = "your account has been locked, please contact the admin";
            }
            if (user.IsDelete)
            {
                meg = "Your account has been canceled";
            }

            if (!string.IsNullOrEmpty(meg))
            {
                return false;
            }

            if (user.IsActive && !user.IsDelete)
            {
                string pwd = string.Empty;
                switch (user.PasswordFormatId)
                {
                    case (int)EnumPasswordFormat.Encrypted:
                        pwd = HelperCommon.CreatePassEncryptText(Password);
                        break;
                    case (int)EnumPasswordFormat.Hashed:
                        pwd = HelperCommon.CreatePasswordHash(Password, user.PasswordSaft); //SHA1
                        break;
                    case (int)EnumPasswordFormat.EncryptAbc283:
                        pwd = HelperCommon.EncryptAbc283(Password, user.PasswordSaft); //SHA1
                        break;
                    case (int)EnumPasswordFormat.EncryptCodeAES256:
                        pwd = HelperCommon.EncryptCodeAES256(Password); //SHA1
                        break;
                    default:
                        pwd = Password;
                        break;
                }
                bool isValid = pwd == user.Password;
                if (isValid)
                    return true;
            }
            return false;
        }
        #endregion

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

        public string GetDomain()
        {
            var scheme = HttpContext.Current.Request.Url.Scheme;
            var host = HttpContext.Current.Request.Url.Host;
            string domain = string.Format("{0}://{1}", scheme, host);
            var port = HttpContext.Current.Request.Url.Port;
            if (port > 0 && port != 80 && port != 443)
            {
                domain += ":" + port.ToString();
            }
            domain = "https://trading.fortrex.io";// _helper.GetDomain();
            return domain;
        }
        public string GetDomainStatis()
        {
            string domain = "https://static.fortrex.io";// _helper.GetDomain();
            return domain;
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