using System;
using System.Security.Cryptography;
using System.Text;
using Newtonsoft.Json;

namespace SurveyApplication.Utility
{
    public static  class StringUltils
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
    }
}