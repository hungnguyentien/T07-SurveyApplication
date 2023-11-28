using DocumentFormat.OpenXml.Bibliography;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace SurveyApplication.Utility
{
    public static class StringUltils
    {
        /// <summary>
        ///     Mã hóa chuỗi có mật khẩu (có các ký tự đặc biệt)
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa</returns>
        public static string EncryptWithKey(string toEncrypt, string key)
        {
            byte[] keyArray;
            var toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            var cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length).Replace('+', '-').Replace('/', '_');
        }

        /// <summary>
        ///     Giản mã  (có các ký tự đặc biệt)
        /// </summary>
        /// <param name="toDecrypt">Chuỗi đã mã hóa</param>
        /// <returns>Chuỗi giản mã</returns>
        public static string DecryptWithKey(string toDecrypt, string key)
        {
            byte[] keyArray;
            var toEncryptArray = Convert.FromBase64String(toDecrypt.Replace('-', '+').Replace('_', '/'));
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            var cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }

        /// <summary>
        ///     Decode url formatted
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string DecodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.UnescapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        /// <summary>
        ///     Encode url formatted
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string EncodeUrlString(string url)
        {
            string newUrl;
            while ((newUrl = Uri.EscapeDataString(url)) != url)
                url = newUrl;
            return newUrl;
        }

        /// <summary>
        /// Có dấu sang không dâu
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ConvertToCamelString(this string str)
        {
            if (str == null)
            {
                return null;
            }

            // Creates a TextInfo based on the "en-US" culture.
            var textInfo = new CultureInfo("en-US", false).TextInfo;

            var newStr = textInfo.ToTitleCase(str.ConvertToUnSign()).Replace(" ", string.Empty);

            return char.ToUpper(newStr[0]) + newStr[1..];
        }

        /// <summary>
        /// Có dấu sang không dâu
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private static string ConvertToUnSign(this string str)
        {
            if (str == null)
            {
                return null;
            }

            var vietnameseSigns = new[]
            {

                "aAeEoOuUiIdDyY",

                "áàạảãâấầậẩẫăắằặẳẵ",

                "ÁÀẠẢÃÂẤẦẬẨẪĂẮẰẶẲẴ",

                "éèẹẻẽêếềệểễ",

                "ÉÈẸẺẼÊẾỀỆỂỄ",

                "óòọỏõôốồộổỗơớờợởỡ",

                "ÓÒỌỎÕÔỐỒỘỔỖƠỚỜỢỞỠ",

                "úùụủũưứừựửữ",

                "ÚÙỤỦŨƯỨỪỰỬỮ",

                "íìịỉĩ",

                "ÍÌỊỈĨ",

                "đ",

                "Đ",

                "ýỳỵỷỹ",

                "ÝỲỴỶỸ"

            };
            //Tiến hành thay thế , lọc bỏ dấu cho chuỗi
            for (int i = 1; i < vietnameseSigns.Length; i++)
            {
                for (int j = 0; j < vietnameseSigns[i].Length; j++)
                {
                    str = str.Replace(vietnameseSigns[i][j], vietnameseSigns[0][i - 1]);
                }
            }

            var reg = new Regex("[/*'\",_&#^@]");
            return reg.Replace(str, " ");

        }

        public static string ConvertDayOfWeekToTcvn(this DayOfWeek day)
        {
            if (day == DayOfWeek.Sunday)
                return "Chủ Nhật";
            return $"Thứ {(int)day + 1}";
        }
    }
}