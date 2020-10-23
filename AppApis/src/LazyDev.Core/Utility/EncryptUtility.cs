using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LazyDev.Core.Utility
{
    public static class EncryptUtility
    {
        /// <summary>
        /// md5加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string Md5(string input)
        {
            using var md5 = MD5.Create();
            var data = md5.ComputeHash(Encoding.UTF8.GetBytes(input));
            var builder = new StringBuilder();
            foreach (var t in data)
            {
                builder.Append(t.ToString("x2"));
            }
            return builder.ToString();
        }

        #region Aes 加密解密

        private const string DefaultKey = @"MY2GB73L0qgP5oOzSF@kmoCaRRF$8GvK";
        private const string DefaultIv = "iTkL$lnAH$A8a3el";

        /// <summary>
        /// 加密明文（使用默认的加密Key，偏移量Iv）
        /// </summary>
        /// <param name="plainText"></param>
        /// <returns></returns>
        public static string AesEncrypt(this string plainText)
        {
            var key = Encoding.UTF8.GetBytes(DefaultKey);
            var iv = Encoding.UTF8.GetBytes(DefaultIv);
            return AesEncrypt(plainText, key, iv);
        }

        /// <summary>
        /// Aes解密(使用默认的加密Key，偏移量Iv)
        /// </summary>
        /// <param name="base64Str"></param>
        /// <returns></returns>
        public static string AesDecrypt(this string base64Str)
        {
            var key = Encoding.UTF8.GetBytes(DefaultKey);
            var iv = Encoding.UTF8.GetBytes(DefaultIv);
            return AesDecrypt(base64Str, key, iv);
        }

        /// <summary>
        /// Aes加密 
        /// </summary>
        /// <param name="plainText">明文</param>
        /// <param name="key">32位密钥</param>
        /// <param name="iv">16位偏移量</param>
        /// <returns>加密后的base64字符串</returns>
        public static string AesEncrypt(string plainText, byte[] key, byte[] iv)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException(nameof(plainText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            using var aesAlg = Aes.Create();
            // ReSharper disable once PossibleNullReferenceException
            aesAlg.Key = key;
            aesAlg.IV = iv;
            var cryptoTransform = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using var msEncrypt = new MemoryStream();
            using var csEncrypt = new CryptoStream(msEncrypt, cryptoTransform, CryptoStreamMode.Write);
            using var swEncrypt = new StreamWriter(csEncrypt);
            swEncrypt.Write(plainText);

            var encrypted = msEncrypt.ToArray();
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Aes解密
        /// </summary>
        /// <param name="base64Str">base64密文</param>
        /// <param name="key">32位密钥</param>
        /// <param name="iv">16位偏移量</param>
        /// <returns>明文</returns>
        public static string AesDecrypt(string base64Str, byte[] key, byte[] iv)
        {
            var cipherText = Convert.FromBase64String(base64Str);
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException(nameof(cipherText));
            if (key == null || key.Length <= 0)
                throw new ArgumentNullException(nameof(key));
            if (iv == null || iv.Length <= 0)
                throw new ArgumentNullException(nameof(iv));

            using var aesAlg = Aes.Create();
            // ReSharper disable once PossibleNullReferenceException
            aesAlg.Key = key;
            aesAlg.IV = iv;

            var cryptoTransform = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
            using var msDecrypt = new MemoryStream(cipherText);
            using var csDecrypt = new CryptoStream(msDecrypt, cryptoTransform, CryptoStreamMode.Read);
            using var srDecrypt = new StreamReader(csDecrypt);
            return srDecrypt.ReadToEnd();
        }

        #endregion
    }
}
