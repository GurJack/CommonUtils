﻿﻿﻿﻿﻿﻿using System;
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
                return char.ToUpper(str[0]) + str.Substring(1);

            return str.ToUpper();
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

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(str.ToLower());
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
                var hashBytes = md5.ComputeHash(inputBytes);
                return Convert.ToHexString(hashBytes).ToLower();
            }
        }
    }
}
