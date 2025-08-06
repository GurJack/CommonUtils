using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtils.CryptoProviders
{
    /// <summary>
    /// Class for encoding and decoding any data.
    /// Algorithm is SHA256.
    /// </summary>
    public class SHA256CryproProvider : ICryptoProvider
    {
        private readonly SHA256 _cryptoProvider = SHA256.Create();
        private readonly string _password = "СIS2?.0!2016Г";

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SHA256CryproProvider()
        {
        }

        /// <summary>
        /// Constructor with parameter.
        /// </summary>
        /// <param name="password">Specified password.</param>
        public SHA256CryproProvider(string password)
        {
            _password = password;
        }

        /// <summary>
        /// Decodes data.
        /// </summary>
        /// <param name="stream"></param>
        public Stream DecodeData(Stream stream)
        {
            if (stream == null)
                return null;

            if (_password == null)
                throw new ArgumentNullException(nameof(_password));

            // Get the bytes of the string
            var passwordBytes = Encoding.UTF8.GetBytes(_password);
            passwordBytes = _cryptoProvider.ComputeHash(passwordBytes);

            byte[] bytes;
            using (var output = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(output);

                bytes = AES_Decrypt(output.ToArray(), passwordBytes);
            }

            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Encodes data.
        /// </summary>
        /// <param name="stream"></param>
        public Stream EncodeData(Stream stream)
        {
            if (stream == null)
                return null;

            if (_password == null)
                throw new ArgumentNullException(nameof(_password));

            // Get the bytes of the string
            var passwordBytes = Encoding.UTF8.GetBytes(_password);

            // Hash the password with SHA256
            passwordBytes = _cryptoProvider.ComputeHash(passwordBytes);

            byte[] bytes;
            using (var output = new MemoryStream())
            {
                stream.Position = 0;
                stream.CopyTo(output);

                bytes = AES_Encrypt(output.ToArray(), passwordBytes);
            }

            return new MemoryStream(bytes);
        }

        /// <summary>
        /// Encodes string.
        /// </summary>
        /// <param name="string">The origin string.</param>
        /// <returns>The encoded string.</returns>
        public String EncodeString(String @string)
        {
            if (@string == null)
                return null;

            if (_password == null)
                throw new ArgumentNullException(nameof(_password));

            // Get the bytes of the string
            var bytesToBeEncrypted = Encoding.UTF8.GetBytes(@string);
            var passwordBytes = Encoding.UTF8.GetBytes(_password);

            // Hash the password with SHA256
            passwordBytes = _cryptoProvider.ComputeHash(passwordBytes);

            var bytesEncrypted = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            var result = Convert.ToBase64String(bytesEncrypted);

            return result;
        }

        /// <summary>
        /// Decodes string.
        /// </summary>
        /// <param name="string">The encoded string.</param>
        /// <returns>The decoded string.</returns>
        public String DecodeString(String @string)
        {
            if (@string == null)
                return null;

            if (_password == null)
                throw new ArgumentNullException(nameof(_password));

            // Get the bytes of the string
            var bytesToBeDecrypted = Convert.FromBase64String(@string);
            var passwordBytes = Encoding.UTF8.GetBytes(_password);
            passwordBytes = _cryptoProvider.ComputeHash(passwordBytes);

            var bytesDecrypted = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            var result = Encoding.UTF8.GetString(bytesDecrypted);

            return result;
        }

        private static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            byte[] decryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = { 18, 50, 18, 0, 8, 2, 1, 6 };

            // Create the streams used for decryption.
            using (var ms = new MemoryStream())
            {
                // Create an Aes object
                // with the specified key and IV.
                using (var aesAlg = new AesManaged())
                {
                    aesAlg.KeySize = 256;
                    aesAlg.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    aesAlg.Mode = CipherMode.ECB;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Create a decrytor to perform the stream transform.
                    var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

                    using (var cs = new CryptoStream(ms, decryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                        cs.Close();
                    }
                    decryptedBytes = ms.ToArray();
                }
            }

            return decryptedBytes;
        }

        private static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:
            // The salt bytes must be at least 8 bytes.
            byte[] saltBytes = { 18, 50, 18, 0, 8, 2, 1, 6 };

            // Create the streams used for encryption.
            using (var ms = new MemoryStream())
            {
                // Create an Aes object
                // with the specified key and IV.
                using (var aesAlg = new AesManaged())
                {
                    aesAlg.KeySize = 256;
                    aesAlg.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);

                    aesAlg.Key = key.GetBytes(aesAlg.KeySize / 8);
                    aesAlg.IV = key.GetBytes(aesAlg.BlockSize / 8);

                    aesAlg.Mode = CipherMode.ECB;
                    aesAlg.Padding = PaddingMode.PKCS7;

                    // Create a decrytor to perform the stream transform.
                    var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

                    // Create a decrytor to perform the stream transform.
                    using (var cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            // Return the encrypted bytes from the memory stream.
            return encryptedBytes;
        }
    }
}