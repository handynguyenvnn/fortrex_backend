
using Lib.Domain;
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

namespace Web.SourceCoin.Common
{
    public class HelperCommon
    {
     
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

            string encryptionPrivateKey = "FORBITMAKV2SPBNI9921221";

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

        public static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "-ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}