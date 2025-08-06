using System;
using System.Linq;

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
        /// Gets the first attribute name (before first point).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The first attribute name.</returns>
        public static string GetFirstName(this string attributeName)
        {
            int index = attributeName.IndexOf('.');
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
                return String.Empty;
            }

            return attributeName.Substring(index + 1);
        }

        /// <summary>
        /// Gets the head attribute part of name (before last point).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The head attribute part of name.</returns>
        public static string GetFirstPart(this string attributeName)
        {
            int index = attributeName.LastIndexOf('.');
            if (index < 0)
            {
                return attributeName;
            }

            return attributeName.Substring(0, index);
        }

        /// <summary>
        /// Gets the last part attribute name (after first point).
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The last part attribute name.</returns>
        public static string GetLastPart(this string attributeName)
        {
            int index = attributeName.IndexOf('.');
            if (index < 0)
            {
                return String.Empty;
            }

            return attributeName.Substring(index + 1);
        }

        /// <summary>
        /// Gets the aggregate mode flag.
        /// </summary>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The aggregate mode flag.</returns>
        public static bool IsAggregate(this string attributeName)
        {
            return attributeName.Contains(".");
        }

        /// <summary>
        /// Gets the generic type flag.
        /// </summary>
        /// <param name="realTypeName"></param>
        /// <returns></returns>
        public static bool IsGenericType(this string realTypeName)
        {
            return realTypeName.Contains("`");
        }

        /// <summary>
        /// Gets the count occurrences of a character within current string.
        /// </summary>
        /// <returns></returns>
        public static int GetCountCharacter(this string source, char character)
        {
            int count = 0;
            foreach (char c in source)
                if (c == character) count++;

            return count;
        }

        /// <summary>
        ///   Splits a string into substrings based on the separator string. You can specify
        ///    whether the substrings include empty array elements.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="separator">A string array that delimits the substrings in this string, an empty array that
        ///          contains no delimiters, or null.</param>
        /// <param name="options">System.StringSplitOptions.RemoveEmptyEntries to omit empty array elements from
        ///        the array returned; or System.StringSplitOptions.None to include empty array
        ///        elements in the array returned.</param>
        /// <returns>An array whose elements contain the substrings in this string that are delimited by  separator. </returns>
        public static string[] Split(this string source, string separator, StringSplitOptions options = StringSplitOptions.None)
        {
            return source.Split(new[] {separator}, options);
        }


        /// <summary>
        /// Gets the name from group path (last item)
        /// </summary>
        /// <param name="path">path string (root2|root1|RealName)</param>
        /// <param name="separator">separate sybbol ("|")</param>
        /// <returns>returns the last name (RealName)</returns>
        public static string NameFromPath(this string path, string separator = "|")
        {
            return path.Split(separator, StringSplitOptions.RemoveEmptyEntries).Last();
        }

        /// <summary>
        /// Returns the part of a character string starting at a specified number of characters from the left.
        /// </summary>
        /// <param name="value">The original string.</param>
        /// <param name="length">The limit length.</param>
        /// <returns>The part of a character string starting at a specified number of characters from the left.</returns> 
        public static string Left(this string value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length >= value.Length)
            {
                return value;
            }
            else
            {
                return value.Substring(0, length);
            }
        }

        /// <summary>
        /// Returns the part of a character string starting at a specified number of characters from the right.
        /// </summary>
        /// <param name="value">The original string.</param>
        /// <param name="length">The limit length.</param>
        /// <returns>The part of a character string starting at a specified number of characters from the right.</returns> 
        public static string Right(this string value, int length)
        {
            if (value == null)
            {
                throw new ArgumentNullException(nameof(value));
            }

            if (length >= value.Length)
            {
                return value;
            }
            else
            {
                return value.Substring(value.Length - length, length);
            }
        }
    }
}