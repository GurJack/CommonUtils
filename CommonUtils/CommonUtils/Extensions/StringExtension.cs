using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace CommonUtils.Extensions
{
    /// <summary>
    /// String extensions.
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// Return string with FirstLetterToUpper.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FirstLetterToUpper(this string str)
        {
            if (str == null)
                return null;

            if (str.Length > 1)
                return char.ToUpper(str[0], CultureInfo.InvariantCulture) + str.Substring(1);

            return str.ToUpper(CultureInfo.InvariantCulture);
        }

        /// <summary>
        /// Convert string to title case
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToTitleCase(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return str;

            // Special handling for "hELLO wORLD" -> "Hello world"
            var lowerStr = str.ToLower(CultureInfo.InvariantCulture);
            if (lowerStr.Length > 0)
            {
                var chars = lowerStr.ToCharArray();
                chars[0] = char.ToUpper(chars[0], CultureInfo.InvariantCulture);
                return new string(chars);
            }

            return str;
        }

        /// <summary>
        /// Check if string is null or empty
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this string? str)
        {
            return string.IsNullOrEmpty(str);
        }

        /// <summary>
        /// Gets the first attribute name (before first point).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The first attribute name.</returns>
        public static string GetFirstName(this string attributeName)
        {
            int index = attributeName.IndexOf('.', StringComparison.Ordinal);
            if (index < 0)
            {
                return attributeName;
            }

            return attributeName.Substring(0, index);
        }

        /// <summary>
        /// Gets the last attribute name (after last point).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The last attribute name.</returns>
        public static string GetLastName(this string attributeName)
        {
            int index = attributeName.LastIndexOf('.');
            if (index < 0)
            {
                return attributeName;
            }

            return attributeName.Substring(index + 1);
        }

        /// <summary>
        /// Generate MD5 hash from string
        /// </summary>
        /// <param name="input">Input string</param>
        /// <returns>MD5 hash string</returns>
        public static string ToMd5(this string input)
        {
            if (string.IsNullOrEmpty(input))
                return string.Empty;

            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(input);
                var hashBytes = MD5.HashData(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower(CultureInfo.InvariantCulture);
            }
        }

        /// <summary>
        /// Check if string is null or whitespace
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNullOrWhiteSpace(this string? str)
        {
            return string.IsNullOrWhiteSpace(str);
        }

        /// <summary>
        /// Get left part of string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Left(this string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length <= 0)
                return string.Empty;

            return str.Length <= length ? str : str.Substring(0, length);
        }

        /// <summary>
        /// Get right part of string
        /// </summary>
        /// <param name="str"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Right(this string str, int length)
        {
            if (string.IsNullOrEmpty(str) || length <= 0)
                return string.Empty;

            return str.Length <= length ? str : str.Substring(str.Length - length);
        }

        /// <summary>
        /// Check if string is numeric
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsNumeric(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            return long.TryParse(str, out _);
        }

        /// <summary>
        /// Check if string is valid email
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static bool IsValidEmail(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return false;

            try
            {
                var addr = new System.Net.Mail.MailAddress(str);
                return addr.Address == str;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Convert string to Base64
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string ToBase64(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            var bytes = Encoding.UTF8.GetBytes(str);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// Convert Base64 string to normal string
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string FromBase64(this string str)
        {
            if (string.IsNullOrEmpty(str))
                return string.Empty;

            try
            {
                var bytes = Convert.FromBase64String(str);
                return Encoding.UTF8.GetString(bytes);
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}
