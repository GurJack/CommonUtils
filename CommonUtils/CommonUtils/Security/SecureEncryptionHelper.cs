﻿﻿﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Security
{
    /// <summary>
    /// Помощник для безопасного шифрования данных с использованием AES
    /// </summary>
    public static class SecureEncryptionHelper
    {
        /// <summary>
        /// Шифрует текст с использованием AES алгоритма
        /// </summary>
        /// <param name="plainText">Открытый текст для шифрования</param>
        /// <param name="key">Ключ шифрования</param>
        /// <returns>Зашифрованный текст в формате Base64</returns>
        /// <exception cref="ArgumentNullException">Возникает, если ключ пустой</exception>
        public static string Encrypt(string plainText, string key)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            using Aes aes = Aes.Create();
            using var rfc = new Rfc2898DeriveBytes(key, GenerateSalt(key), 10000, HashAlgorithmName.SHA256);
            aes.Key = rfc.GetBytes(32);
            aes.IV = rfc.GetBytes(16);

            using var encryptor = aes.CreateEncryptor(aes.Key, aes.IV);
            using var ms = new MemoryStream();
            using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                cs.Write(plainBytes, 0, plainBytes.Length);
            }
            return Convert.ToBase64String(ms.ToArray());
        }

        /// <summary>
        /// Расшифровывает текст, зашифрованный методом Encrypt
        /// </summary>
        /// <param name="cipherText">Зашифрованный текст в формате Base64</param>
        /// <param name="key">Ключ шифрования</param>
        /// <returns>Расшифрованный текст</returns>
        /// <exception cref="ArgumentNullException">Возникает, если ключ пустой</exception>
        public static string Decrypt(string cipherText, string key)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            if (string.IsNullOrEmpty(key)) throw new ArgumentNullException(nameof(key));

            using Aes aes = Aes.Create();
            using var rfc = new Rfc2898DeriveBytes(key, GenerateSalt(key), 10000, HashAlgorithmName.SHA256);
            aes.Key = rfc.GetBytes(32);
            aes.IV = rfc.GetBytes(16);

            using var decryptor = aes.CreateDecryptor(aes.Key, aes.IV);
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using var ms = new MemoryStream(cipherBytes);
            using var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read);
            using var reader = new StreamReader(cs);
            return reader.ReadToEnd();
        }

        private static byte[] GenerateSalt(string seed)
        {
            return SHA256.HashData(Encoding.UTF8.GetBytes(seed))[..16];
        }
    }
}
