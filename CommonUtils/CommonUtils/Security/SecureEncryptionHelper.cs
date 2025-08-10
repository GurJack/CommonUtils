using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CommonUtils.Security
{
    public static class SecureEncryptionHelper
    {
        public static string Encrypt(string plainText, string password)
        {
            if (string.IsNullOrEmpty(plainText)) return plainText;

            // Генерируем соль на основе пароля
            byte[] salt = GenerateSalt(password);

            using (Aes aes = Aes.Create())
            {
                // Генерируем ключ из пароля
                using (var rfc = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    10000,
                    HashAlgorithmName.SHA256))
                {
                    aes.Key = rfc.GetBytes(aes.KeySize / 8);
                    aes.IV = rfc.GetBytes(aes.BlockSize / 8);
                }

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        byte[] plainBytes = Encoding.UTF8.GetBytes(plainText);
                        cs.Write(plainBytes, 0, plainBytes.Length);
                    }

                    return Convert.ToBase64String(salt) + "|" +
                           Convert.ToBase64String(ms.ToArray());
                }
            }
        }

        public static string Decrypt(string cipherText, string password)
        {
            if (string.IsNullOrEmpty(cipherText)) return cipherText;
            if (!cipherText.Contains("|")) return cipherText;

            var parts = cipherText.Split('|');
            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] cipherBytes = Convert.FromBase64String(parts[1]);

            using (Aes aes = Aes.Create())
            {
                // Генерируем ключ из пароля
                using (var rfc = new Rfc2898DeriveBytes(
                    password,
                    salt,
                    10000,
                    HashAlgorithmName.SHA256))
                {
                    aes.Key = rfc.GetBytes(aes.KeySize / 8);
                    aes.IV = rfc.GetBytes(aes.BlockSize / 8);
                }

                ICryptoTransform decryptor = aes.CreateDecryptor(aes.Key, aes.IV);

                using (var ms = new System.IO.MemoryStream(cipherBytes))
                using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                using (var reader = new System.IO.StreamReader(cs))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        private static byte[] GenerateSalt(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
                byte[] hash = sha256.ComputeHash(passwordBytes);

                // Используем первые 16 байт хеша как соль
                byte[] salt = new byte[16];
                Array.Copy(hash, salt, 16);
                return salt;
            }
        }
    }
}
