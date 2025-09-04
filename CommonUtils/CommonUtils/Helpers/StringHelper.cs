//using System;
//using System.Globalization;
//using System.Linq;
//using System.Security.Cryptography;
//using System.Text;
//using CommonUtils.Extensions;

//namespace CommonUtils.Helpers
//{
//    /// <summary>
//    /// 
//    /// </summary>
//    public static class StringHelper
//    {
//        const string dictRu = "йцукенгшщзхъфывапролджэячсмитьбюёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮЁ";
//        const string dictEu = "qwertyuiop[]asdfghjkl;'zxcvbnm,.`QWERTYUIOP[]ASDFGHJKL;'ZXCVBNM,.`";

//        private const int HashSaltLength = 2;

//        private static SHA1CryptoServiceProvider DefaultHashProvider => new SHA1CryptoServiceProvider();
//        private static Encoding DefaultUnicode => Encoding.Unicode;

//        /// <summary>
//        /// Gets the SHA256CryptoServiceProvider.
//        /// </summary>
//        public static SHA256CryptoServiceProvider GetSHA256CryptoServiceProvider() => new SHA256CryptoServiceProvider();

//        /// <summary>
//        /// convert string of ru chars to eu chars
//        /// ONLY FOR ЙЦУКЕН-QWERTY KEYBOARDS
//        /// TODO: multilanguage and other keyboards
//        /// </summary>
//        /// <param name="input"></param>
//        /// <param name="isToUpper"></param>
//        /// <returns></returns>
//        public static string ConvertRuToEu(this string input, bool isToUpper=false)
//        {
//            if(string.IsNullOrEmpty(input))
//                return string.Empty;

//            string result = "";

//            foreach(var ch in input)
//            {
//                var ind = dictRu.IndexOf(ch);

//                if(ind>=0)
//                {                    
//                    result += dictEu[ind];
//                }
//                else
//                {
//                    result += ch;
//                }
//            }

//            if(isToUpper)
//            {
//                result = result.ToUpper();
//            }

//            return result; 
//        }

//        public static string GetComonPart(string[] strings)
//        {
//            string commonPart = "";
//            for (int i = 0; i < strings.Max().Length; i++)
//            {
//                bool equal = true;
//                char cur = strings[0][i];

//                foreach (var str in strings)
//                    equal &= cur == str[i];


//                if (equal)
//                    commonPart += cur;
//                else
//                    break;
//            }

//            return commonPart;
//        }

//        private static string GetHash(string stringToHash, Encoding enc, HashAlgorithm hashProvider, string salt)
//        {
//            var bytes = enc.GetBytes(stringToHash + salt);
//            var hash = Convert.ToBase64String(hashProvider.ComputeHash(bytes));
//            hash = hash.Left(hash.Length - 2);

//            return salt + hash;
//        }

//        /// <summary>
//        /// Get hash for string.
//        /// </summary>
//        /// <param name="stringToHash">Original string.</param>
//        /// <param name="enc">Character encoding.</param>
//        /// <param name="hashProvider">Hash algorithm.</param>
//        /// <returns>Hash for string.</returns>
//        public static string GetHash(string stringToHash, Encoding enc, HashAlgorithm hashProvider)
//        {
//            var salt = RandomGenerator.NextString(HashSaltLength);

//            return GetHash(stringToHash, enc, hashProvider, salt);
//        }

//        /// <summary>
//        /// Get hash for string (encoding Unicode).
//        /// </summary>
//        /// <param name="stringToHash">Original string.</param>
//        /// <param name="hashProvider">Hash algorithm.</param>
//        /// <returns>Hash for string.</returns>
//        public static string GetHash(string stringToHash, HashAlgorithm hashProvider)
//        {
//            return GetHash(stringToHash, Encoding.Unicode, hashProvider);
//        }

//        /// <summary>
//        /// Get SHA1 hash for string.
//        /// </summary>
//        /// <param name="stringToHash">Original string.</param>
//        /// <param name="enc">Character encoding.</param>
//        /// <returns>SHA1 hash for string.</returns>
//        public static string GetHash(string stringToHash, Encoding enc)
//        {
//            return GetHash(stringToHash, enc, DefaultHashProvider);
//        }

//        /// <summary>
//        /// Get SHA1 hash for string (encoding Unicode).
//        /// </summary>
//        /// <param name="stringToHash">Original string.</param>
//        /// <returns>Hash for string.</returns>
//        public static string GetHash(string stringToHash)
//        {
//            return GetHash(stringToHash, DefaultUnicode);
//        }

//        /// <summary>
//        /// Compares original value and hash string.
//        /// </summary>
//        /// <param name="hash">The hash string.</param>
//        /// <param name="value">The original string value.</param>
//        /// <returns></returns>
//        public static bool Compare(string value, string hash)
//        {
//            if (hash == null)
//            {
//                throw new ArgumentNullException(nameof(hash));
//            }
//            if (hash == null)
//            {
//                throw new ArgumentNullException(nameof(hash));
//            }

//            var salt = hash.Left(HashSaltLength);

//            if (salt.Length != HashSaltLength) return false;

//            var newHash = GetHash(value, DefaultUnicode, DefaultHashProvider, salt);

//            return hash == newHash;
//        }

//        /// <summary>
//        /// Compares original value and hash string.
//        /// </summary>
//        /// <param name="value">The original string value.</param>
//        /// <param name="hash">The hash string.</param>
//        /// <param name="hashProvider">The specified hash provider.</param>
//        /// <returns></returns>
//        public static bool Compare(string value, string hash, HashAlgorithm hashProvider)
//        {
//            if (hash == null)
//            {
//                throw new ArgumentNullException(nameof(hash));
//            }

//            if (hash == null)
//            {
//                throw new ArgumentNullException(nameof(hash));
//            }

//            var salt = hash.Left(HashSaltLength);

//            if (salt.Length != HashSaltLength) return false;

//            var newHash = GetHash(value, DefaultUnicode, hashProvider, salt);

//            return hash == newHash;
//        }

//        /// <summary>
//        /// Gets the localization message.
//        /// Text parameters will be replaced with a specified arguments.
//        /// </summary>
//        /// <param name="format">A composite format string.</param>
//        /// <param name="args">Specified arguments.</param>
//        /// <returns>Localization message.</returns>
//        public static string FormatMessage(string format, params object[] args)
//        {
//            if (format == null)
//            {
//                return null;
//            }

//            return String.Format(CultureInfo.CurrentCulture, format, args);
//        }

//    }
//}
