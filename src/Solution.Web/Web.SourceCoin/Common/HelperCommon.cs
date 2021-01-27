using HtmlAgilityPack;
using Lib.Domain;
using Lib.Domain.Packages;
using Lib.Domain.Simples;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Web.SourceCoin.Common
{
    public class HelperCommon
    {
        public static string CreatePasswordHash(string password, string saltkey, string passwordFormat = "SHA1")
        {
            if (String.IsNullOrEmpty(passwordFormat))
                passwordFormat = "SHA1";
            string saltAndPassword = String.Concat(password, saltkey);
            string hashedPassword = FormsAuthentication.HashPasswordForStoringInConfigFile(saltAndPassword, passwordFormat);
            return hashedPassword;
        }

        public static string CreatePassEncryptText(string plainText, string encryptionPrivateKey = "MAKV2SPBNI9921221")
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;
            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));
            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            return Convert.ToBase64String(encryptedBinary);
        }

        public static string CreateEncryptText(string plainText)
        {
            if (string.IsNullOrEmpty(plainText))
                return plainText;

            string encryptionPrivateKey = "EQOPTIONMAKV2SPBNI9921221";

            var tDESalg = new TripleDESCryptoServiceProvider();
            tDESalg.Key = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(0, 16));
            tDESalg.IV = new ASCIIEncoding().GetBytes(encryptionPrivateKey.Substring(8, 8));

            byte[] encryptedBinary = EncryptTextToMemory(plainText, tDESalg.Key, tDESalg.IV);
            string result = Convert.ToBase64String(encryptedBinary);
            result = Regex.Replace(result, "[^0-9a-zA-Z]+", "");
            return result;
        }

        public static string ComputeSha256Hash(string rawData)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                string encryptionPrivateKey = Constants.TOKEN_KEY;
                // ComputeHash - returns byte array  
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(encryptionPrivateKey+rawData));

                // Convert byte array to a string   
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        public static string CreateSaltKey(int size = 5)
        {
            // Generate a cryptographic random number
            var rng = new RNGCryptoServiceProvider();
            var buff = new byte[size];
            rng.GetBytes(buff);

            // Return a Base64 string representation of the random number
            return Convert.ToBase64String(buff);
        }

        private static byte[] EncryptTextToMemory(string data, byte[] key, byte[] iv)
        {
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, new TripleDESCryptoServiceProvider().CreateEncryptor(key, iv), CryptoStreamMode.Write))
                {
                    byte[] toEncrypt = new UnicodeEncoding().GetBytes(data);
                    cs.Write(toEncrypt, 0, toEncrypt.Length);
                    cs.FlushFinalBlock();
                }
                return ms.ToArray();
            }
        }

        #region thuat toan tampv
        public static System.Security.Cryptography.RijndaelManaged GetRijndaelManaged(String secretKey)
        {
            var keyBytes = new byte[16];
            var secretKeyBytes = System.Text.Encoding.UTF8.GetBytes(secretKey);
            Array.Copy(secretKeyBytes, keyBytes, Math.Min(keyBytes.Length, secretKeyBytes.Length));
            return new System.Security.Cryptography.RijndaelManaged
            {
                Mode = System.Security.Cryptography.CipherMode.CBC,
                Padding = System.Security.Cryptography.PaddingMode.PKCS7,
                KeySize = 128,
                BlockSize = 128,
                Key = keyBytes,
                IV = keyBytes
            };
        }

        public static byte[] Encrypt(byte[] plainBytes, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateEncryptor()
                .TransformFinalBlock(plainBytes, 0, plainBytes.Length);
        }

        public static byte[] Decrypt(byte[] encryptedData, System.Security.Cryptography.RijndaelManaged rijndaelManaged)
        {
            return rijndaelManaged.CreateDecryptor()
                .TransformFinalBlock(encryptedData, 0, encryptedData.Length);
        }

        public static String EncryptAbc283(String plainText, String key)
        {
            var plainBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return Convert.ToBase64String(Encrypt(plainBytes, GetRijndaelManaged(key)));
        }

        public static String DecryptAbc283(String encryptedText, String key)
        {
            var encryptedBytes = Convert.FromBase64String(encryptedText);
            return System.Text.Encoding.UTF8.GetString(Decrypt(encryptedBytes, GetRijndaelManaged(key)));
        }
        #endregion

        #region thuat toan AES256 (256 bits = 32 bytes)
        public static string EncryptCodeAES256(string code)
        {
            string encryptkey = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            string iVector = "hochiminh1234";
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException("Code");
            }
            CryptLib _crypt = new CryptLib();
            string key = CryptLib.getHashSha256(encryptkey, 32);
            return _crypt.encrypt(code, key, iVector);
        }

        public static string DecryptCodeAES256(string code)
        {
            string encryptkey = "abcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()";
            string iVector = "hochiminh1234";
            if (string.IsNullOrWhiteSpace(code))
            {
                throw new ArgumentNullException("Code");
            }
            CryptLib _crypt = new CryptLib();
            string key = CryptLib.getHashSha256(encryptkey, 32);
            return _crypt.decrypt(code, key, iVector);
        }
        #endregion

        public static string GetUserAgent()
        {
            HttpRequest httpReq = HttpContext.Current.Request;
            return httpReq.Headers["User-Agent"].ToString();
        }

        public static string GetUserIP()
        {
            string strIP = String.Empty;
            HttpRequest httpReq = HttpContext.Current.Request;

            //test for non-standard proxy server designations of client's IP
            if (httpReq.ServerVariables["HTTP_CLIENT_IP"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_CLIENT_IP"].ToString();
            }
            else if (httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"] != null)
            {
                strIP = httpReq.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString();
            }
            //test for host address reported by the server
            else if ((httpReq.UserHostAddress.Length != 0) && ((httpReq.UserHostAddress != "::1") || (httpReq.UserHostAddress != "localhost")))
            {
                strIP = httpReq.UserHostAddress;
            }
            //finally, if all else fails, get the IP from a web scrape of another server
            else
            {
                WebRequest request = WebRequest.Create("http://checkip.dyndns.org/");
                using (WebResponse response = request.GetResponse())
                using (StreamReader sr = new StreamReader(response.GetResponseStream()))
                {
                    strIP = sr.ReadToEnd();
                }
                //scrape ip from the html
                int i1 = strIP.IndexOf("Address: ") + 9;
                int i2 = strIP.LastIndexOf("</body>");
                strIP = strIP.Substring(i1, i2 - i1);
            }
            return strIP;
        }

        public static string GetUserCountryByIp()
        {
            IpInfo ipInfo = new IpInfo();
            try
            {
                string ip = GetUserIP();
                string info = new WebClient().DownloadString("http://ipinfo.io/" + ip);
                ipInfo = JsonConvert.DeserializeObject<IpInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
            }
            catch
            {
                ipInfo.Country = null;
            }
            if (!string.IsNullOrEmpty(ipInfo.Country))
                return ipInfo.Country.ToUpper();
            return null;
        }

        public static string GetFormatAmount(decimal? amount)
        {
            return string.Format("{0:n0}", amount);
            //string price = string.Empty;
            //if (amount.HasValue)
            //    price = amount.Value.ToString("#,##0", new CultureInfo("en-EN"));
            //return price;
        }

        public static string GetUserName(int id)
        {
            return (id + 1).ToString("U00000000");
        }

        public static int SendEMailSync(string to, string title, string body, string host, string from, string formTitle, string pass)
        {
            try
            {
                string mailfrom = from;
                string passmail = pass;
                MailAddress ma = new MailAddress(mailfrom, formTitle);
                MailAddress maTo = new MailAddress(to);
                using (MailMessage mm = new MailMessage(ma, maTo))
                {
                    mm.Subject = title;
                    mm.Body = body;
                    mm.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = host;
                    NetworkCredential NetworkCred = new NetworkCredential(mailfrom, passmail);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = NetworkCred;
                    smtp.Port = 80;
                    smtp.Send(mm);
                    return 1;
                }
            }
            catch (Exception)
            {
                return 0; throw;
            }
        }

        public static bool IsValidEmail(string email)
        {
            try
            {
                MailAddress m = new MailAddress(email);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        public static string GetError(int code = 0)
        {
            string meg = string.Empty;
            switch (code)
            {
                case 1:
                    meg = "Email not empty.";
                    break;
                case 2:
                    meg = "Password confirmation is incorrect or less than 6 characters.";
                    break;
                case 3:
                    meg = "Email invalid format.";
                    break;
                case 4:
                    meg = "Email already exists.";
                    break;
                case 5:
                    meg = "Password is incorrect";
                    break;
                case 6:
                    meg = "Token expire";
                    break;
                case 7:
                    meg = "";
                    break;
                case 8:
                    meg = "";
                    break;
                case 9:
                    meg = "";
                    break;
                default:
                    meg = "System busy.";
                    break;
            }
            return meg;
        }

        public static string NumberFormat(decimal? number, string type = "")
        {
            if (number == null)
                return "0";
            switch (type)
            {
                case "g":
                    return string.Format("{0:g}", number);
                default:
                    return string.Format("{0:n}", number);
            }
        }

        public static DateTime TimeStampToDateTime(double unixTimeStamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Local);
            dtDateTime = dtDateTime.AddSeconds(unixTimeStamp).ToLocalTime();
            return dtDateTime;
        }

        public static int getTimeStreamLive(DateTime createOn)
        {
            DateTime timeNow = DateTime.Now.ToUniversalTime();
            TimeSpan span = timeNow.Subtract(createOn);
            int minute = span.Minutes;
            if (minute < 45)
            {
                return minute > 0 ? minute : 1;
            }
            return 0;
        }

        public static void SetCookies(string cookiesName, string value, int minute = 60, bool isJs = false)
        {
            HttpCookie cookie = new HttpCookie(cookiesName);
            cookie.Value = isJs ? EncryptCodeAES256(value) : value;
            cookie.Expires = DateTime.Now.AddMinutes(minute);
            HttpContext.Current.Response.Cookies.Add(cookie);
        }

        public static void ClearCookies(string cookiesName)
        {
            HttpCookie myCookie = new HttpCookie(cookiesName);
            myCookie.Expires = DateTime.Now.AddDays(-1d);
            HttpContext.Current.Response.Cookies.Add(myCookie);
        }

        public static string GetCookies(string cookiesName, bool isJs = false)
        {
            HttpCookie myCookie = new HttpCookie(cookiesName);
            myCookie = HttpContext.Current.Request.Cookies[cookiesName];

            // Read the cookie information and display it.
            if (myCookie != null)
            {
                return isJs ? DecryptCodeAES256(myCookie.Value) : myCookie.Value;
            }
            return null;
        }

        public static string ShowTextLimitLength(string txt, int length)
        {
            if (txt.Length < length)
                return txt;

            txt = txt.Substring(0, length);

            var arr = txt.Trim().Split(' ');
            string chEnd = arr[arr.Count() - 1];
            int i = 2;
            while (chEnd.Length < 3)
            {
                chEnd = arr[arr.Count() - i];
                i++;
            }

            txt = txt.Substring(0, txt.IndexOf(chEnd) - 1);

            return txt + "...";
        }

        public static string RemoveUnicodeCharactersFromString(string inputValue)
        {
            StringBuilder newStringBuilder = new StringBuilder();
            newStringBuilder.Append(inputValue.Normalize(NormalizationForm.FormKD).Where(x => x < 128).ToArray());
            string titleUrl = newStringBuilder.ToString();
            titleUrl = Regex.Replace(titleUrl, "[^0-9a-zA-Z ]+", "");
            titleUrl = titleUrl.Replace(" ", "-");
            titleUrl = titleUrl.Replace("--", "-");
            return titleUrl.ToLower();
        }

        public static string ReFormatAppHtmlContent(string content, int imgSize = 0)
        {
            if (!string.IsNullOrWhiteSpace(content))
            {
                content = RemoveTagEmptyDataFromHtml(content, "p");
                return string.Format("<!DOCTYPE html><meta name=\"viewport\" content=\"initial-scale=1.0\" /><div style=\"color:#444;font-family:arial,helvetica,sans-serif;\">{0}</div>", ResizeImageFromHtml(Regex.Replace(content, "times new roman,times,serif", "arial,helvetica,sans-serif", RegexOptions.IgnoreCase), imgSize));
            }
            else
                return string.Empty;
        }

        public static string RemoveTagEmptyDataFromHtml(string html, string tagName)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes(string.Format("//{0}", tagName));

                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        if (string.IsNullOrWhiteSpace(item.InnerText) && string.IsNullOrWhiteSpace(item.InnerHtml))
                            item.Remove();
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }

        public static string ResizeImageFromHtml(string html, int maxWidth)
        {
            if (!string.IsNullOrEmpty(html))
            {
                var htmlDoc = new HtmlDocument();
                htmlDoc.LoadHtml(html);
                var lstElement = htmlDoc.DocumentNode.SelectNodes("//img");
                if (lstElement != null && lstElement.Count > 0)
                {
                    foreach (var item in lstElement)
                    {
                        var style = item.Attributes["style"];
                        if (maxWidth == 0)
                        {
                            item.SetAttributeValue("style", "width:100% !important;");
                        }
                        else
                        {
                            item.SetAttributeValue("style", string.Format("width:{0}px;", maxWidth));
                        }
                        var divNode = item.ParentNode;

                        while (divNode != null && divNode.OriginalName != "div")
                        {
                            divNode = divNode.ParentNode;
                        }
                        if (divNode != null)
                            divNode.SetAttributeValue("style", "text-align:center");
                    }
                }

                return htmlDoc.DocumentNode.InnerHtml;
            }
            return html;
        }

        public static DateTime ConvertStringToDatetime(string strDate, string format = "yyyy-MM-dd HH:mm:ss")
        {
            try
            {
                return DateTime.ParseExact(strDate, format, null);
            }
            catch
            {
                return DateTime.Now;
            }
        }

        public static string ConvertDatetimToString(DateTime date, string format = "yyyy-MM-dd HH:mm:ss")
        {
            return date.ToString(format);
        }

        public static TargetBonus CalculatorData(decimal left, decimal right, decimal maxInvest, decimal receid, decimal maxPercent)
        {
            decimal maxout = Math.Round(maxInvest * maxPercent / 100, 2);

            decimal percent = maxout > 0 ? Math.Round(receid / maxout * 100, 2) : 0;

            TargetBonus target = new TargetBonus
            {
                Bonus = 0,
                Target = 0,
                MaxOut = maxout,
                MaxoutPercent = percent == 0 ? 0 : percent < 1 ? 1 : percent,
                Receied = receid
            };
            //decimal amount = 0;

            //if (left > right)
            //{
            //    amount = right;
            //    target.Node = "right";
            //}
            //else
            //{
            //    amount = left;
            //    target.Node = "left";
            //}
            //decimal TotalPercentIncomeByLevel = amount;
            //if (amount >= 50000 && amount < 300000)
            //{
            //    target.Bonus = 1000;
            //    target.Target = 300000;
            //    TotalPercentIncomeByLevel = amount-50000;
            //}
            //else if (amount >= 300000 && amount < 600000)
            //{
            //    target.Bonus = 5000;
            //    target.Target = 600000;
            //    TotalPercentIncomeByLevel = amount- 600000;
            //}
            //else if (amount >= 600000 && amount < 1000000)
            //{
            //    target.Bonus = 5000;
            //    target.Target = 1000000;
            //    TotalPercentIncomeByLevel = amount - 1000000;
            //}
            //else if (amount >= 1000000 && amount < 1500000)
            //{
            //    target.Bonus = 5000;
            //    target.Target = 1500000;
            //    TotalPercentIncomeByLevel = amount - 1500000;
            //}
            //else if (amount >= 1500000)
            //{
            //    target.Bonus = 0;
            //    target.Target = 0;
            //    TotalPercentIncomeByLevel = amount;
            //}
            //target.TotalIncomeCurrent = amount;
            ////target.Percent = (int)Math.Ceiling(amount / target.Target * 100);
            //target.Percent = (int)Math.Ceiling(amount / target.Target * 100);

            return target;
        }
        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "-ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool ValidateCapchar(string response)
        {
            string secretKey = ConfigurationManager.AppSettings["CapcharSecretKey"];
            var client = new WebClient();
            var resultToken = client.DownloadString(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", secretKey, response));
            var obj = JObject.Parse(resultToken);
            return (bool)obj.SelectToken("success");
        }
    }
}