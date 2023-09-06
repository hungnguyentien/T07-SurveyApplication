using System;
using System.Security.Cryptography;
using System.Text;

namespace SurveyApplication.Utility
{
    public class StringUltils
    {
        /// <summary>
        /// Mã hóa chuỗi có mật khẩu
        /// </summary>
        /// <param name="toEncrypt">Chuỗi cần mã hóa</param>
        /// <returns>Chuỗi đã mã hóa</returns>
        public static string EncryptWithKey(string toEncrypt, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Encoding.UTF8.GetBytes(toEncrypt);
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateEncryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }

        /// <summary>
        /// Giản mã
        /// </summary>
        /// <param name="toDecrypt">Chuỗi đã mã hóa</param>
        /// <returns>Chuỗi giản mã</returns>
        public static string DecryptWithKey(string toDecrypt, string key)
        {
            byte[] keyArray;
            byte[] toEncryptArray = Convert.FromBase64String(toDecrypt);
            var hashmd5 = new MD5CryptoServiceProvider();
            keyArray = hashmd5.ComputeHash(Encoding.UTF8.GetBytes(key));
            var tdes = new TripleDESCryptoServiceProvider();
            tdes.Key = keyArray;
            tdes.Mode = CipherMode.ECB;
            tdes.Padding = PaddingMode.PKCS7;
            ICryptoTransform cTransform = tdes.CreateDecryptor();
            var resultArray = cTransform.TransformFinalBlock(toEncryptArray, 0, toEncryptArray.Length);
            return Encoding.UTF8.GetString(resultArray);
        }
    }
}
